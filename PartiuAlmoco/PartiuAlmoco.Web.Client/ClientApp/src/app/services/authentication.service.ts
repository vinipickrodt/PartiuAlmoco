import { HttpClient } from '@angular/common/http';
import { EventEmitter, Injectable, Output } from '@angular/core';
import { Observable, of } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { User } from '../interfaces/user';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  constructor(private http: HttpClient) { }

  @Output()
  public authenticationChanged: EventEmitter<User> = new EventEmitter<User>();

  public login(email: string, password: string) {
    return this.http.get(`http://localhost:53233/api/Login/GetJwtToken?email=${encodeURIComponent(email)}&password=${encodeURIComponent(password)}`)
      .pipe(tap((result) => {
        environment.jwt_token = result["access_token"];
        environment.current_user = result["user_info"];
        localStorage.setItem("__userInfo", JSON.stringify(result));
        this.authenticationChanged.emit(result["user_info"]);
      }));
  }

  public logout() {
    environment.jwt_token = null;
    environment.current_user = null;
    localStorage.removeItem("__userInfo");
    this.authenticationChanged.emit(null);
    return of();
  }

  getCurrentUser(): Observable<{ user: User, token: string }> {
    var currentSession = localStorage.getItem("__userInfo");
    var user = null;
    var token = null;

    if (currentSession) {
      user = JSON.parse(currentSession)["user_info"];
      token = JSON.parse(currentSession)["access_token"];

      var authChanged = (!environment.jwt_token);
      environment.jwt_token = token;
      environment.current_user = user;
      if (authChanged) {
        this.authenticationChanged.emit(user);
      }
    }

    return currentSession == null ? of(null) : of({ user, token });
  }
}
