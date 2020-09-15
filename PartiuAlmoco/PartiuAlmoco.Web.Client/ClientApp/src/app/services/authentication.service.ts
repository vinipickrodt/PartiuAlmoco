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

        this.authenticationChanged.emit(result["user_info"]);
      }));
  }

  public logout() {
    environment.jwt_token = null;
    environment.current_user = null;
    this.authenticationChanged.emit(null);
  }

  getCurrentUser(): Observable<{ user: User, token: string }> {
    return environment.current_user == null ? of(null) : of({ user: environment.current_user, token: environment.jwt_token });
  }
}
