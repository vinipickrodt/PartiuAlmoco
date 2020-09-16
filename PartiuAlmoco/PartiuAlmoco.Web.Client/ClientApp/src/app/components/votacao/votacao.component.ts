import { Component, OnInit, ɵɵcontainerRefreshEnd } from '@angular/core';
import { of, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Ranking } from 'src/app/interfaces/ranking';
import { Restaurant } from 'src/app/interfaces/restaurant';
import { ApiService } from 'src/app/services/api.service';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { buildTable } from 'src/environments/utils';

@Component({
  selector: 'app-votacao',
  templateUrl: './votacao.component.html',
  styleUrls: ['./votacao.component.css']
})
export class VotacaoComponent implements OnInit {
  constructor(private api: ApiService, private auth: AuthenticationService) { }

  ngOnInit() {
    this.auth.getCurrentUser().subscribe((user) => {
      this.isAuthenticated = user != null;
      this.refresh();
    });
  }

  cols = 3;
  selected = "";
  votedRestaurantId = "";
  restaurants: Restaurant[] = [];
  isAuthenticated = false;
  ranking: Ranking[] = [];

  refresh() {
    this.api.getRestaurantsValidForPoll().subscribe(result => this.restaurants = result);
    this.api.getRanking().subscribe(ranking => this.ranking = ranking);
    if (this.isAuthenticated) {
      this.api.getMyVote().subscribe(vote => this.votedRestaurantId = vote);
    }
  }

  getRestaurantRows() {
    var restaurantsWithVotes = this.restaurants.map(restaurant => ({ ...restaurant, votos: this.getTotalVotesRestaurant(restaurant.id) }));
    restaurantsWithVotes.sort((r1, r2) => r2.votos - r1.votos);

    return buildTable(this.cols, restaurantsWithVotes);
  }

  getRestaurantClassName(restaurant: Restaurant) {
    return `col-lg-${(12 / this.cols)} ${(this.isSelected(restaurant) || this.isVoted(restaurant) ? 'selected-restaurant' : '')} restaurant`;
  }

  select(restaurant: Restaurant) {
    if (this.isAuthenticated && !this.votedRestaurantId) {
      this.selected = restaurant.id;
    }
  }

  isSelected(restaurant: Restaurant) {
    return this.selected == restaurant['id'];
  }

  isVoted(restaurant: Restaurant) {
    return restaurant.id == this.votedRestaurantId;
  }

  confirmarVoto(restaurant: Restaurant) {
    return this.api.votar(restaurant.id)
      .pipe(catchError((err, _) => { alert(JSON.stringify(err)); return throwError(err); }))
      .subscribe(result => {
        this.votedRestaurantId = restaurant.id;
        this.refresh();
      });
  }

  getVotedRestaurant() {
    return (this.restaurants || []).find(r => r.id == this.votedRestaurantId);
  }

  getTotalVotesRestaurant(restaurantId: string) {
    var restaurantRanking = this.ranking.find(ranking => ranking.restaurant.id == restaurantId);
    return restaurantRanking ? restaurantRanking.votes : 0;
  }
}
