import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { flatMap } from 'rxjs/operators';
import { Ranking } from '../interfaces/ranking';
import { Restaurant } from '../interfaces/restaurant';
import { AuthenticationService } from './authentication.service';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  constructor(private authentication: AuthenticationService, private http: HttpClient) { }

  createUser(fullName: string, friendlyName: string, email: string, password: string) {
    return this.http.post(`${environment.api_endpoint}api/Login/CreateUser`, {
      fullName,
      friendlyName,
      email,
      password
    });
  }

  createRestaurant(restaurant: Restaurant) {
    return this.authentication.getCurrentUser()
      .pipe(flatMap((user) => {
        if (!user) throw new Error("Not Authenticated.");
        return this.http.post(`${environment.api_endpoint}api/Restaurant/Create`, restaurant, {
          headers: {
            Authorization: `Bearer ${user.token}`
          }
        });
      }));
  }

  updateRestaurant(restaurant: Restaurant): Observable<any> {
    return this.authentication.getCurrentUser()
      .pipe(flatMap((user) => {
        if (!user) throw new Error("Not Authenticated.");
        return this.http.post(`${environment.api_endpoint}api/Restaurant/Update`, restaurant, {
          headers: {
            Authorization: `Bearer ${user.token}`
          }
        });
      }));
  }

  getRestaurants(): Observable<Restaurant[]> {
    return this.http.get<Restaurant[]>(`${environment.api_endpoint}api/Restaurant`);
  }

  getRestaurantsValidForPoll(): Observable<Restaurant[]> {
    return this.http.get<Restaurant[]>(`${environment.api_endpoint}api/RestaurantPoll/GetRestaurantsValidForPoll`);
  }

  getRestaurantById(id: string): Observable<Restaurant> {
    return this.authentication.getCurrentUser()
      .pipe(flatMap((user) => {
        if (!user) throw new Error("Not Authenticated.");
        return this.http.get<Restaurant>(`${environment.api_endpoint}api/Restaurant/GetById?id=${encodeURIComponent(id)}`, {
          headers: {
            Authorization: `Bearer ${user.token}`
          }
        });
      }));
  }

  votar(restaurantId: string) {
    return this.authentication.getCurrentUser()
      .pipe(flatMap((user) => {
        if (!user) throw new Error("Not Authenticated.");
        return this.http.get<Restaurant>(`${environment.api_endpoint}api/RestaurantPoll/Vote?restaurantId=${encodeURIComponent(restaurantId)}`, {
          headers: {
            Authorization: `Bearer ${user.token}`
          }
        });
      }));
  }

  getMyVote(): Observable<string> {
    return this.authentication.getCurrentUser()
      .pipe(flatMap((user) => {
        if (!user) throw new Error("Not Authenticated.");
        return this.http.get<string>(`${environment.api_endpoint}api/RestaurantPoll/GetMyVote`, {
          headers: {
            Authorization: `Bearer ${user.token}`
          }
        });
      }));
  }

  getRanking(): Observable<Ranking[]> {
    return this.http.get<Ranking[]>(`${environment.api_endpoint}api/RestaurantPoll/GetRanking`);
  }
}
