import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { RegHttpService } from '../services/http/reg.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-upload-photo',
  templateUrl: './upload-photo.component.html',
  styleUrls: ['./upload-photo.component.css'],
  providers: [RegHttpService]
})
export class UploadPhotoComponent implements OnInit {

  uploadPhotoForm = this.fb.group({
    photo: ['']
  });

  selectedFile: File = null;
  onFileSelected(event) {
    this.selectedFile = <File>event.target.files[0];
  }

  constructor(private fb: FormBuilder, private reg: RegHttpService, private router: Router) { }

  ngOnInit() {
  }

  uploadPhoto() {
    let formData: FormData = new FormData();

    let options = {
      sheaders:
      {
        "Content-type": "application/json"
      }
    }

    if (this.selectedFile != null) {
      formData.append('photo', this.selectedFile, this.selectedFile.name);

      this.reg.uploadPhotoToBackend(formData, localStorage.getItem("name"), options).subscribe(data => {
        this.router.navigate(["login"]);
      },
        err => {
          console.log(err);
        }
      )
    }
  }
}
