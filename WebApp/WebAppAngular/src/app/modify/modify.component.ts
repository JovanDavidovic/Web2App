import { Component, OnInit } from '@angular/core';
import { AuthHttpService } from '../services/http/auth.service';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { ModifyHttpService } from '../services/http/modify.service';
import { ConfirmPasswordValidator } from '../validators/confirm-password-validator';
import { JwtService } from '../services/jwt.service';
import { identifierModuleUrl } from '@angular/compiler';

@Component({
  selector: 'app-modify',
  templateUrl: './modify.component.html',
  styleUrls: ['./modify.component.css'],
  providers: [ModifyHttpService, JwtService]
})
export class ModifyComponent implements OnInit {


  modifyForm = this.fb.group({
    name: ['',
      [Validators.required]],
    lastname: ['',
      [Validators.required]],
    username: ['',
      [Validators.required]],
    email: ['',
      [Validators.required,
      Validators.email]],
    password: ['',
      [Validators.required,
      Validators.minLength(6),
      Validators.pattern(/(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[\W])/)]],
    confirmPassword: [''],
    birthday: ['',
      Validators.required],
    address: ['',
      Validators.required],
    acctype: [''],
  });

  get modiForm() { return this.modifyForm.controls; }

  constructor(private fb: FormBuilder, private modi: ModifyHttpService, private router: Router, private jwt: JwtService) { }

  ngOnInit() {
    this.modi.beforeModify(this.jwt.getMail()).subscribe(data => {
      console.log(data);

      this.modifyForm.patchValue({name: data.Name});
      this.modifyForm.patchValue({lastname: data.LastName});
      this.modifyForm.patchValue({username: data.UserName});
      this.modifyForm.patchValue({birthday: data.DateOfBirth});
      this.modifyForm.patchValue({address: data.Address});
      this.modifyForm.patchValue({email: this.jwt.getMail()})

      
      if(data.TypeId == 1)
      {
        this.modifyForm.patchValue({acctype: "Regular"});
      }
      else if(data.TypeId == 2)
      {
        this.modifyForm.patchValue({acctype: "Student"});
      }
      else
      {
        this.modifyForm.patchValue({acctype: "Pensioner"});
      }
    },
      err => {
        console.log(err);
      })
  }

  modify(){
    this.modifyForm.patchValue({email: this.jwt.getMail()})
    this.modifyForm.patchValue({confirmPassword: this.modifyForm.get("password").value})
    this.modi.modify(this.modifyForm.value).subscribe(data => {
      this.router.navigate(["login"]);
    },
      err => {
        console.log(err);
      })
  }
}
