import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { Observable, of } from 'rxjs';
import { User } from '../_models/user';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit{
  constructor(public accountService: AccountService, private toastr: ToastrService,
    private router: Router){}
  model: any = {};

  ngOnInit(): void {  }

  login(){
    this.accountService.login(this.model).subscribe({
      next: _ => this.router.navigateByUrl("/members"),
      error: error=>this.toastr.error(error.error)
    });
  }

  logout() {
    this.accountService.logout();
  }

}
