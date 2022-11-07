import { HttpClient, HttpParams, HttpResponse, HttpResponseBase } from '@angular/common/http';
import { Component } from '@angular/core';
import { ToastrModule, ToastrService } from 'ngx-toastr';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'MicProject';
}
