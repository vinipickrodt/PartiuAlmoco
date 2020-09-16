import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Restaurant } from 'src/app/interfaces/restaurant';
import { ApiService } from 'src/app/services/api.service';
import { catchError } from 'rxjs/operators';
import Swal from 'sweetalert2';
import { throwError } from 'rxjs';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-restaurante',
  templateUrl: './restaurante.component.html',
  styleUrls: ['./restaurante.component.css']
})
export class RestauranteComponent implements OnInit {

  constructor(private api: ApiService, private route: ActivatedRoute, private router: Router, private fb: FormBuilder) { }

  id = "";

  form: FormGroup;

  ngOnInit() {
    this.form = this.fb.group({
      name: ['', Validators.required],
      website: [''],
      phone: ['']
    });

    this.route.params.subscribe((params) => {
      this.id = params["id"];

      if (this.id) {
        this.api.getRestaurantById(this.id).subscribe((restaurant => {
          this.id = restaurant.id;
          this.form.setValue({
            name: restaurant.name,
            website: restaurant.website,
            phone: restaurant.phone
          })
        }));
      }
    });
  }

  save() {
    if (this.id) {
      var data = {
        ...this.form.value,
        id: this.id
      };
      this.api.updateRestaurant(data)
        .pipe(
          catchError(err => {
            const errMsg = (typeof err.error === 'object' && err.error !== null) ? JSON.stringify(err.error) : err.error;
            Swal.fire("Update Failed", errMsg, "error");
            return throwError(err);
          }))
        .subscribe(result => {
          this.router.navigate(["/restaurantes"]);
        });
    } else {
      this.api.createRestaurant(this.form.value)
        .pipe(
          catchError(err => {
            const errMsg = (typeof err.error === 'object' && err.error !== null) ? JSON.stringify(err.error) : err.error;
            Swal.fire("Insert Failed", errMsg, "error");
            return throwError(err);
          }))
        .subscribe(result => {
          this.router.navigate(["/restaurantes"]);
        });
    }
  }
}
