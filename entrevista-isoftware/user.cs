using System.ComponentModel.DataAnnotations;

namespace entrevista_isoftware;

public class User
{

    [Key]
    public int UserId { get; set; }

    [Required]
    [StringLength(50)]
    [MinLength(3)]
    public string? Nombre { get; set; }

    [Required]
    public string? Edad { get; set; }

    [Required]
    public string? Email { get; set; }

}
