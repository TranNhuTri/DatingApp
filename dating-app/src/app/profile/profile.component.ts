import { Component, OnInit } from '@angular/core';
import { Member } from '../_models/member';
import { AccountsService } from '../_services/accounts.service';
import { MembersService } from '../_services/members.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {
  profile: Member = new Member();
  constructor(private membersService: MembersService, private accountsService: AccountsService) { }

  ngOnInit(): void {
    let username = '';
    this.accountsService.currentUser$.subscribe(
      (account) => {
        if(account != null)
          username = account.username;
      },
      (error) => console.log(error)
    )
    this.membersService.getMemberByUsername(username).subscribe(
      (member) => this.profile = member,
      (error) => console.log(error)
    )
  }

  submit() {
    this.membersService.updateProfile(this.profile).subscribe();
  }
}
