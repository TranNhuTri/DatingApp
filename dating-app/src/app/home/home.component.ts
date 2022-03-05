import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  isRegister: boolean = false;

  constructor() { }

  ngOnInit(): void {
  }

  cancelRegisterMode(status: boolean) {
    this.isRegister = status;
  }

}
