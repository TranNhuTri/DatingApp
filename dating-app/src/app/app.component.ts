import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { User } from './_models/user';
import { AccountsService } from './_services/accounts.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  constructor(public accountsService: AccountsService) {}

  ngOnInit(): void {
    this.accountsService.refreshToken();
  }
}
