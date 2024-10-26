using entrevista_isoftware;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configure the DbContext
builder.Services.AddDbContext<MyDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => "Prueba Tecnica");

app.MapGet("/entrevista", async (MyDbContext dbContext) =>
{
    var result = await dbContext.Users.ToListAsync();
    return result;
});

app.MapPost("/entrevista", async (MyDbContext dbContext, User user) =>
{
    await dbContext.Users.AddAsync(user);
    await dbContext.SaveChangesAsync();
    return user;

});

app.MapDelete("/entrevista/{id}", async (MyDbContext dbContext, int id) =>
{
    var user = await dbContext.Users.FirstOrDefaultAsync(u => u.UserId == id);
    if (user == null)
    {
        return Results.NotFound();
    }
    dbContext.Users.Remove(user);
    await dbContext.SaveChangesAsync();
    return Results.NoContent();
});

app.MapPut("/entrevista/{id}", async (MyDbContext dbContext, int id, User user) =>
{
    if (user.UserId != id)
    {
        return Results.BadRequest();
    }
    var existingUser = await dbContext.Users.FindAsync(id);
    if (existingUser == null)
    {
        return Results.NotFound();
    }

    // Update properties here, or you can use a mapper to map all properties
    existingUser.Nombre = user.Nombre; // Assuming the User model has a Name property
    existingUser.Email = user.Email; // Assuming the User model has an Email property

    await dbContext.SaveChangesAsync();
    return Results.Ok(existingUser);
});

app.UseHttpsRedirection();

app.Run();