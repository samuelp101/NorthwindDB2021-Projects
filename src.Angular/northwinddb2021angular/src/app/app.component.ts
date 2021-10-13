import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'northwinddb2021angular';
  customers: any;

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.getCustomers();
  }

  getCustomers() {
    this.http.get('https://localhost:44341/api/customers').subscribe(response => {
      this.customers = response;
    }, error => {
      console.log(error);
    })
  }


}
