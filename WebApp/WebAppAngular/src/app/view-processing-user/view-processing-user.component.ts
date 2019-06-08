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

  constructor(private vali: ValidateUsersService, private router: Router, private jwt: JwtService) { }

  ngOnInit() {
      this.vali.getUserByUsername(localStorage.getItem("name")).subscribe(data => {
      
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
        }
        else
        {
          this.proccessingPassenger.acctype = "Pensioner";
        }
    });

    localStorage.name = undefined;
  }

}
