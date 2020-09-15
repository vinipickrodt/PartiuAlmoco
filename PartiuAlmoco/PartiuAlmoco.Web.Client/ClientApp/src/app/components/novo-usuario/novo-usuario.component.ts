import { Component, Input, OnInit } from '@angular/core';
import { ApiService } from 'src/app/services/api.service';
import { catchError } from "rxjs/operators";
import { throwError } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-novo-usuario',
  templateUrl: './novo-usuario.component.html',
  styleUrls: ['./novo-usuario.component.css']
})
export class NovoUsuarioComponent implements OnInit {
  constructor(private api: ApiService, private router: Router) { }

  ngOnInit() { }

  @Input()
  firstName: string = "";

  @Input()
  lastName: string = "";

  @Input()
  email: string = "";

  @Input()
  password: string = "";

  createUser() {
    const firstName = (this.firstName.trim() || "").trim();
    const lastName = (this.lastName.trim() || "").trim();
    const email = (this.email.trim() || "").trim();
    const password = (this.password.trim() || "").trim();

    const fullName = firstName + " " + lastName;
    const friendlyName = firstName;

    this.api.createUser(fullName, friendlyName, this.email, this.password)
      .pipe(
        catchError(function (a, b) {
          return throwError(a);
        }))
      .subscribe(response => {
        this.router.navigate(['/entrar']);
      });
  }
}
