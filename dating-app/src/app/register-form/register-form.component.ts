import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { UserRegister } from '../_models/user';
import { AccountsService } from '../_services/accounts.service';

@Component({
  selector: 'app-register-form',
  templateUrl: './register-form.component.html',
  styleUrls: ['./register-form.component.scss']
})
export class RegisterFormComponent implements OnInit {
  user: UserRegister = new UserRegister();
  @Output() cancelRegister = new EventEmitter();

  constructor(private accoutService: AccountsService) { }

  ngOnInit(): void {
  }

  register() {
    this.accoutService.register(this.user).subscribe(res => this.cancel());
  }

  cancel() {
    this.cancelRegister.emit(false);
  }

}
