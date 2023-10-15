import { Component, NgModule, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title ='Dating App';
  users: any;
  
  constructor(private http: HttpClient){}

  ngOnInit(): void {
    // http.get is an observable need a subscribe otherwise it is lazy
    this.http.get('https://localhost:5001/api/users').subscribe({
      next: res=> this.users = res,
      error: err => console.log(err),
      complete: ()=> console.log("task completed!")
    });
    //throw new Error('Method not implemented.');
  }
  
}
