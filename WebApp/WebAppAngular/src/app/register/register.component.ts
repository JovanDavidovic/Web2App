import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { RegHttpService } from '../services/http/reg.service';
import { ConfirmPasswordValidator } from '../validators/confirm-password-validator';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
  providers: [RegHttpService]
})
export class RegisterComponent implements OnInit {

  registerForm = this.fb.group({
    name: ['',
      [Validators.required]],
    lastname: ['',
      [Validators.required]],
    email: ['',
      [Validators.required,
      Validators.email]],
    password: ['',
      [Validators.required,
      Validators.minLength(6),
      Validators.pattern(/(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[\W])/)]],
    confirmPassword: ['',
      Validators.required],
    birthday: ['',
      Validators.required],
    address: ['',
      Validators.required],
    acctype: [''],
  }, { validators: ConfirmPasswordValidator });

  get regForm() { return this.registerForm.controls; }

  constructor(private fb: FormBuilder, private reg: RegHttpService, private router: Router) { }

  ngOnInit() {
  }

  register() {

    this.reg.register(this.registerForm.value).subscribe(data => {
      this.router.navigate(["login"]);
    },
      err => {
        console.log(err);
      })

  }

}
