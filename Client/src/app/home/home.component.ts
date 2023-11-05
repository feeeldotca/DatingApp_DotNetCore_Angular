import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  registerMode = false;
  users: any;

  constructor(private http:HttpClient){};
  ngOnInit(): void {
      this.getUsers();
  }

  getUsers(){
    // http.get is an observable need a subscribe otherwise it is lazy
    this.http.get('https://localhost:5001/api/users').subscribe({
      next: res=> this.users = res,
      error: err => console.log(err),
      complete: ()=> console.log("Request has completed!")
    });
  }

  registerToggle() {
    this.registerMode = !this.registerMode;
  }

  cancelRegisterMode(event: boolean){
    this.registerMode = event;
  }
}
