import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Member } from '../_models/member';

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  }
  userUrl = "https://localhost:5001/api/Users";
  constructor(private httpClient: HttpClient) { }
  
  public getMembers(): Observable<Member[]> {
    return this.httpClient.get<Member[]>(this.userUrl);
  }

  public getMemberByUsername(username: string): Observable<Member> {
    return this.httpClient.get<Member>(`${this.userUrl}/${username}`);
  }

  public updateProfile(member: Member): Observable<any> {
    return this.httpClient.put(this.userUrl, member, this.httpOptions);
  }
}
