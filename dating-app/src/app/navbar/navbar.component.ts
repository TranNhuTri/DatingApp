import { Component, OnInit } from '@angular/core';
import { UserLogin } from '../_models/userLogin';
import { AccountsService } from '../_services/accounts.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {
  user: UserLogin = { username: 'npurches1', password: 'Password' }
  username: string = '';

  constructor(public accountsService: AccountsService) { }

  ngOnInit(): void {
  }

  login() {
    this.accountsService.login(this.user).subscribe(
      (response) => console.log(response),
      (error) => console.log(error)
    );
  }

}
