import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { JwtService } from '../services/jwt.service';
import { ModifyHttpService } from '../services/http/modify.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
  providers: [ModifyHttpService, JwtService]
})
export class HomeComponent implements OnInit {

  constructor(private fb: FormBuilder, private modi: ModifyHttpService, private router: Router, private jwt: JwtService) { }

  ngOnInit() {
    if(!this.jwt.isTokenValid()){
      localStorage.jwt = undefined;
      localStorage.name = undefined;
    }
    else if (this.jwt.getRole() == "AppUser") {
      this.modi.beforeModify(this.jwt.getMail()).subscribe(data => {

        localStorage.name = data.UserName;

        if (data.TypeId != 1 && data.Image == null) {
          localStorage.name = data.UserName;
          this.router.navigate(["uploadPhoto"]);
        }
      })
    }
  }

}
