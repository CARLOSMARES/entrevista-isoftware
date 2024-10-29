import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterOutlet } from '@angular/router';
import { ApiService } from './api.service';
import { Users } from './users';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, FormsModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'User';
  userData: Users[] = [];
  newData: Users = {} as Users;
  nombre: string = '';
  age: number = 1;
  email: string = '';
  isEditing: boolean = false;
  isAdding: boolean = false;
  selectedUserId!: number;
  constructor(private apiService: ApiService) {
    this.getUsers();
  }

  validarEmail(email: string): boolean {
    const regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return regex.test(email);
  }

  newUserForm() {
    this.isAdding = true;
    this.resetForm();
  }

  getUsers() {
    this.apiService.getUsers().subscribe((data) => {
      data.map((item) => {
        console.log(item);
        this.userData.push(item);
      })
    });
  }

  deleteUser(id: number) {
    this.apiService.deleteUser(id).subscribe((data) => {
      this.userData = this.userData.filter(user => user.userId !== id);
    });
  }

  editUser(id: number) {
    this.selectedUserId = id;
    this.isEditing = true;
    this.isAdding = false;

    const userToEdit = this.userData.find(item => item.userId === id);
    if (userToEdit) {
      this.nombre = userToEdit.nombre;
      this.age = userToEdit.edad;
      this.email = userToEdit.email;
      this.newData = { nombre: this.nombre, edad: this.age, email: this.email };
    }
  }

  async submitEdit() {
    if (this.validarEmail(this.email) === false) {
      alert('Email no valido');
      return
    }
    this.newData = {
      userId: this.selectedUserId,
      nombre: this.nombre,
      edad: this.age,
      email: this.email
    };

    try {
      const data = await this.apiService.updateUser(this.selectedUserId, this.newData).toPromise();
      // Actualiza los datos locales
      this.userData = this.userData.map(user =>
        user.userId === this.selectedUserId ? { ...user, ...this.newData } : user
      );
      this.resetForm();
      this.isEditing = false;
    } catch (error: any) {
      console.error('Error al actualizar el usuario:', error);
      alert(`Error al actualizar el usuario: ${error.message || 'Ocurrió un error'}`);
    }
  }

  newUser() {
    if (this.validarEmail(this.email) === false) {
      alert('Email no valido');
      return
    } else {
      this.newData = {
        nombre: this.nombre,
        edad: this.age,
        email: this.email
      }
      console.log(this.nombre, this.age, this.email);
      console.log(this.newData);
      this.apiService.newUser(this.newData).subscribe((data) => {
        this.userData.push(this.newData);
        this.resetForm();
        this.isAdding = false;
      },
        (error) => {
          console.error(error);
        }
      );
    }
  }

  cancelEdit() {
    this.isEditing = false;
    this.isAdding = false;
    this.resetForm();
  }

  resetForm() {
    this.nombre = '';
    this.age = 0;
    this.email = '';
  }

}
