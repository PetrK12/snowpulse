import { Component } from '@angular/core';
import {AbstractControl, AsyncValidator, AsyncValidatorFn, FormBuilder, Validators} from "@angular/forms";
import {AccountService} from "../account.service";
import {Router} from "@angular/router";
import {debounce, debounceTime, finalize, map, take, switchMap} from "rxjs";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent {
  errors: string[] | null = null;
  constructor(private fb: FormBuilder, private accountService: AccountService, private router: Router) {}
  complextPassRegex = "(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_" +
  "+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$"

  registerForm = this.fb.group({
    displayname: [null, Validators.required],
    email: [null, [Validators.required, Validators.email], [this.validateEmailNotTaken()]],
    password: [null, [Validators.required, Validators.pattern(this.complextPassRegex)]]
  })

  onSubmit() {
    this.accountService.register(this.registerForm.value).subscribe({
      next: () => this.router.navigateByUrl('/shop'),
      error: error => this.errors = error
    });
  }

  validateEmailNotTaken(): AsyncValidatorFn{
    return(control: AbstractControl) => {
      return control.valueChanges.pipe(
        debounceTime(2000),
        take(1),
        switchMap(() => {
          return this.accountService.checkEmailExists(control.value).pipe(
            map(result => result? {emailExists: true} : null),
            finalize(() => control.markAllAsTouched())
          )
        })
      )
    }
  }
}
