import { Component } from '@angular/core';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  model: any = {};
  username:string = "";
  password:string = "";
  
  register(){
    // this.username = model.username;
    // this.password = model.password;
    console.log(this.model);
  }

  cancel() {
    console.log("cancelled");
  }
}
