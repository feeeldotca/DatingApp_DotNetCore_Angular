import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit{
  constructor(private accountService: AccountService){}
  model: any = {}
  loggedIn: boolean = false;
  ngOnInit(): void {
      
  }
  login(){
    this.accountService.login(this.model).subscribe({
      next: response => {
        console.log(response),
        this.loggedIn = true
      },
      error: error=>console.log(error)
    });
  }

  logout() {
    this.loggedIn = !this.loggedIn;
  }

}
