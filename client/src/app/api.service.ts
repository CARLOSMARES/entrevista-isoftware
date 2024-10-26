import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Users } from './users';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(
    private http: HttpClient
  ) {
  }

  url = 'http://localhost:5055/entrevista'

  getUsers(): Observable<Users[]> {
    return this.http.get<Users[]>(this.url);
  }

  deleteUser(id: number) {
    console.log(id)
    return this.http.delete(`${this.url}/${id}`);
  }

  updateUser(id: number, userData: Users) {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.put(`${this.url}/${id}`, userData, { headers });
  }


  newUser(data: Users) {
    return this.http.post(this.url, data);
  }

}
