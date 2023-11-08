import { CanActivateFn } from '@angular/router';
import { AccountService } from '../_services/account.service';
import { inject } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { map } from 'rxjs';

export const authGuard: CanActivateFn = (route, state) => {
  const accountService = inject(AccountService);
  const toastr = inject(ToastrService);
  // check if the current user is authorized to visit the link
  return accountService.currentUser$.pipe(map(user=>{
    if(user) return true;
    else{
      // if not authorized then give error message
      toastr.error("You shall not pass!");
      return false;
    }
  }));
};
