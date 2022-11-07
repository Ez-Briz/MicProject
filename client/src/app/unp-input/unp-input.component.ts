import { HttpClient, HttpRequest, HttpResponse, HttpResponseBase } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormArray, FormControl, FormGroup } from '@angular/forms';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-unp-input',
  templateUrl: './unp-input.component.html',
  styleUrls: ['./unp-input.component.css']
})
export class UnpInputComponent implements OnInit {
  form: FormGroup;
  nalogdbstatus: string = "";
  localdbstatus: string = "";
  baseUrl = environment.apiUrl;

  ngOnInit(): void {}
  constructor(private http: HttpClient) {
    this.form = new FormGroup({
      unps: new FormArray([
        new FormControl('')
      ])
    });
  }

  get unps() {
    return this.form.get('unps') as FormArray;
  }

  addUnp() {
    this.unps.push(new FormControl());
  }
  removeUnp() {
    if (this.unps.length > 1)
      this.unps.removeAt(this.unps.length-1);
  }

  checkStatus() {
    for (let unpIndex in this.unps.controls) {
      this.getInfo(this.unps.controls[unpIndex].value).subscribe((response: any) => {
        this.nalogdbstatus = "Found";
      },
      error => {
        console.log(error)
        this.nalogdbstatus = "Not found!";
      });
      this.getUser(this.unps.controls[unpIndex].value).subscribe((response: any) => {
        this.localdbstatus = response?.id ? "Found" : "Not found!";
      },
      error => {
        console.log(error)
        this.localdbstatus = "Not found!";
      });
    }
  }

  getInfo(unp: string) {
    return this.http.get(this.baseUrl + "info?unp=" + unp);
  }

  getUser(unp: string) {
    return this.http.get(this.baseUrl + "user/" + unp);
  }
}
