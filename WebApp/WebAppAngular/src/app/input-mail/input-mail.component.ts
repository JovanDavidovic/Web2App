import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { MapService } from '../services/http/map.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-input-mail',
  templateUrl: './input-mail.component.html',
  styleUrls: ['./input-mail.component.css'],
  providers: [MapService]
})
export class InputMailComponent implements OnInit {

  emailForm = this.fb.group({
    email: ['',
    [Validators.required,
    Validators.email]]
  });

  get mailForm() { return this.emailForm.controls; }

  constructor(private fb: FormBuilder, private mapService: MapService, private router: Router) { }

  ngOnInit() {
  }

  send(){
    this.mapService.sendMail(this.emailForm.get("email").value).subscribe(data => {
      this.router.navigate(["home"]);
    })
  }

}
