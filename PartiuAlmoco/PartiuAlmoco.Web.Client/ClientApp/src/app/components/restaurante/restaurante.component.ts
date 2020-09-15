import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Restaurant } from 'src/app/interfaces/restaurant';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-restaurante',
  templateUrl: './restaurante.component.html',
  styleUrls: ['./restaurante.component.css']
})
export class RestauranteComponent implements OnInit {

  constructor(private api: ApiService, private route: ActivatedRoute, private router: Router) { }

  id = "";
  restaurant: Restaurant = { id: null, name: "", website: "", phone: "" };

  ngOnInit() {
    this.route.params.subscribe((params) => {
      this.id = params["id"];

      if (this.id) {
        this.api.getRestaurantById(this.id).subscribe((restaurant => this.restaurant = restaurant));
      }
    });
  }

  save() {
    if (this.restaurant.id) {
      this.api.updateRestaurant(this.restaurant).subscribe(result => {
        this.router.navigate(["/restaurantes"]);
      });
    } else {
      var obj = { ...this.restaurant };
      delete obj["id"];
      this.api.createRestaurant(obj).subscribe(result => {
        this.router.navigate(["/restaurantes"]);
      });
    }
  }
}
