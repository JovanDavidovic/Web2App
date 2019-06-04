import { Component, OnInit } from '@angular/core';
import { AuthHttpService } from '../services/http/auth.service';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  providers: [AuthHttpService]
})
export class LoginComponent implements OnInit {

  registerForm = this.fb.group({
    mail: ['',
      [Validators.required,
      Validators.email]],
    pass: ['',
      [Validators.required,
      Validators.minLength(6),
      Validators.pattern(/(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[\W])/)]],
  });

  get regForm() { return this.registerForm.controls; }

  constructor(private fb: FormBuilder, private auth: AuthHttpService, private router: Router) { }

  ngOnInit() {
  }

  login() {
    this.auth.logIn(this.registerForm.get("mail").value, this.registerForm.get("pass").value).subscribe(data => {
      localStorage.jwt = data.access_token;
      this.router.navigate(["home"]);
    },
      err => {
        console.log(err);
      })

  }

}