import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { UserRegister, UserToken } from '../_models/user';
import { UserLogin } from '../_models/userLogin';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AccountsService {

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  }

  accountsUrl = "https://localhost:5001/api/Accounts";
  private currentUser = new BehaviorSubject<UserToken | null>(null);
  currentUser$ = this.currentUser.asObservable();

  constructor(private httpClient: HttpClient) { }

  login(userLogin: UserLogin): Observable<any> {
    return this.httpClient.post<UserToken>(`${this.accountsUrl}/login`, userLogin, this.httpOptions)
      .pipe(map((response: UserToken) => {
        const user = response;
        if (user) {
          localStorage.setItem('userToken', JSON.stringify(user));
          this.currentUser.next(user);
        }
      }));
  }

  logout() {
    localStorage.removeItem('userToken');
    this.currentUser.next(null);
  }

  register(user: UserRegister): Observable<any> {
    return this.httpClient.post<UserToken>(`${this.accountsUrl}/register`, user, this.httpOptions)
    .pipe(map((response: UserToken) => {
      const user = response;
      if (user) {
        localStorage.setItem('userToken', JSON.stringify(user));
        this.currentUser.next(user);
      }
    }));
  }

  refreshToken() {
    const userString = localStorage.getItem('userToken');
    if (!userString) return;
    const user = JSON.parse(userString);
    if (user) {
      this.currentUser.next(user);
      return;
    }
    this.logout();
  }
}
