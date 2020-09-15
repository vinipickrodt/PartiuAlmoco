import { Component, OnInit } from '@angular/core';
import { Restaurant } from 'src/app/interfaces/restaurant';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-restaurantes',
  templateUrl: './restaurantes.component.html',
  styleUrls: ['./restaurantes.component.css']
})
export class RestaurantesComponent implements OnInit {
  constructor(private api: ApiService) { }

  ngOnInit() {
    this.refresh();
  }

  restaurants: Restaurant[];

  refresh() {
    this.api.getRestaurants().subscribe(result => this.restaurants = result);
  }
}
