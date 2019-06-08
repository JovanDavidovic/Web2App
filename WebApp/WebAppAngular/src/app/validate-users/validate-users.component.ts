import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder } from '@angular/forms';
import { ValidateUsersService } from '../services/http/validate-users.service';
import { Passenger } from '../models/passenger-model';

@Component({
  selector: 'app-validate-users',
  templateUrl: './validate-users.component.html',
  styleUrls: ['./validate-users.component.css'],
  providers: [ValidateUsersService]
})
export class ValidateUsersComponent implements OnInit {

  passengers: Passenger[] = [];
  selectedPassenger: Passenger;

  constructor(private fb: FormBuilder, private val: ValidateUsersService, private router: Router) { }

  ngOnInit() {
    this.val.getAllUsers().subscribe(data => {
      console.log(data);

      data.forEach(element => {

        if(element.TypeId == 1)
        {
          this.passengers.push({ username: element.UserName, role: "Regular" });
        }
        else if(element.TypeId == 2)
        {
          this.passengers.push({ username: element.UserName, role: "Student" });
        }
        else
        {
          this.passengers.push({ username: element.UserName, role: "Pensioner" });
        }
        

      });
    });
  }


  onSelect(passenger: Passenger): void {
    console.log("selected");
    
    this.selectedPassenger = passenger;
    localStorage.name = this.selectedPassenger.username;
    this.router.navigate(["viewProcessingUser"]);
    
  }
}
