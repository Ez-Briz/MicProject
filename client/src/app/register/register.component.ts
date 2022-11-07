import { HttpClient, HttpParams } from '@angular/common/http';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  baseUrl = environment.apiUrl;
  model: any;
  constructor(
    private toast: ToastrService,
    private http: HttpClient) {}

  ngOnInit(): void {
  }

  register(unp: string, email: string) {
    this.model = {unp, email};
    this.postUser(this.model).subscribe((response: any) => {
      if (response) {
        this.toast.success("Successful registration!");
      }
      else {
        this.toast.error("Something went wrong!");
      }
      },
      error => {
        this.toast.error(error.error, error.status);
      });
  }

  postUser (model: any) {
    let params = new HttpParams();
    params = params.append('unp', model.unp);
    params = params.append('email', model.email);
    return this.http.post(this.baseUrl + "user", params);
  }
}
