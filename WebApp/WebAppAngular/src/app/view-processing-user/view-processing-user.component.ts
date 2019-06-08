import { Component, OnInit } from '@angular/core';
import { ValidateUsersService } from '../services/http/validate-users.service';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { JwtService } from '../services/jwt.service';
import { WholePassengerModel } from '../models/whole-passenger-model';

@Component({
  selector: 'app-view-processing-user',
  templateUrl: './view-processing-user.component.html',
  styleUrls: ['./view-processing-user.component.css'],
  providers: [ValidateUsersService]
})
export class ViewProcessingUserComponent implements OnInit {

  proccessingPassenger: WholePassengerModel;
  mySrc: string;

  constructor(private vali: ValidateUsersService, private router: Router, private jwt: JwtService) { }

  ngOnInit() {
      this.vali.getUserByUsername(localStorage.getItem("name")).subscribe(data => {
      
        this.proccessingPassenger = new WholePassengerModel();

        this.proccessingPassenger.name = data.Name;
        this.proccessingPassenger.lastname = data.LastName;
        this.proccessingPassenger.username = data.UserName;
        this.proccessingPassenger.email = data.Email;
        this.proccessingPassenger.address = data.Address;
        this.proccessingPassenger.birthday = data.DateOfBirth;
        
        if(data.TypeId == 1)
        {
          this.proccessingPassenger.acctype = "Regular";
        }
        else if(data.TypeId == 2)
        {
          this.proccessingPassenger.acctype = "Student";
          this.vali.getPhoto(this.proccessingPassenger.username).subscribe(data2 => {
            this.mySrc = 'data:image/jpeg;base64,' + data2;
            console.log(data2);
          },
          err => {
            console.log(err);
          });
        }
        else
        {
          this.proccessingPassenger.acctype = "Pensioner";
          this.vali.getPhoto(this.proccessingPassenger.username).subscribe(data2 => {
            this.mySrc = 'data:image/jpeg;base64,' + data2;
          },
          err => {
            console.log(err);
          });
        }
    });

    localStorage.name = undefined;
  }

  DenyValidate() {
    console.log("user denied");
    this.vali.DenyValidateToBackend(this.proccessingPassenger.username).subscribe(data => {
      this.router.navigate(["validateUsers"]);
    });
  }

  AcceptValidate() {
    console.log("user accepted");
    this.vali.AcceptValidateToBackend(this.proccessingPassenger.username).subscribe(data => {
      this.router.navigate(["validateUsers"]);
    });
  }
}
