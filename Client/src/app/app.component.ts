import { Component, NgModule, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AccountService } from './_services/account.service';
import { User } from './_models/user';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title ='Dating App';
  users: any;
  
  constructor(private http: HttpClient, private accountService: AccountService){}

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

  setCurrentUser() {
    const userString = localStorage.getItem('user');
    if(!userString) return;
    const user: User = JSON.parse(userString);
    this.accountService.setCurrentUser(user);
  }
  
}
