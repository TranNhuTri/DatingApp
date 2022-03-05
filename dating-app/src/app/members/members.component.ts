import { Component, OnInit } from '@angular/core';
import { Member } from '../_models/member';
import { MembersService } from '../_services/members.service';

@Component({
  selector: 'app-members',
  templateUrl: './members.component.html',
  styleUrls: ['./members.component.scss']
})
export class MembersComponent implements OnInit {
  members: Member[] = [];

  constructor(private membersService: MembersService) { }

  ngOnInit(): void {
    this.fetchMembers();
  }

  fetchMembers() {
    this.membersService.getMembers().subscribe(
      res => this.members = res,
      err => console.log(err)
    )
  }

}
