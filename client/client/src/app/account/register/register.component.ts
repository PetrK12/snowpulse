import { Component } from '@angular/core';
import {FormBuilder, Validators} from "@angular/forms";
import {AccountService} from "../account.service";
import {Router} from "@angular/router";

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
    email: [null, [Validators.required, Validators.email]],
    password: [null, [Validators.required, Validators.pattern(this.complextPassRegex)]]
  })

  onSubmit() {
    this.accountService.register(this.registerForm.value).subscribe({
      next: () => this.router.navigateByUrl('/shop'),
      error: error => this.errors = error
    });
  }
}
