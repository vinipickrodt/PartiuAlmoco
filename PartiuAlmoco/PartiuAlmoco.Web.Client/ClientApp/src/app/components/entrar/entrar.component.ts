import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ApiService } from 'src/app/services/api.service';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { environment } from 'src/environments/environment';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-entrar',
  templateUrl: './entrar.component.html',
  styleUrls: ['./entrar.component.css']
})
export class EntrarComponent implements OnInit {
  constructor(private authentication: AuthenticationService, private router: Router) { }

  ngOnInit() { }

  email = "";
  password = "";

  login() {
    this.authentication.login(this.email, this.password)
      .pipe(catchError(err => {
        const errMsg = (typeof err.error === 'object' && err.error !== null) ? JSON.stringify(err.error) : err.error;
        Swal.fire("Login failed", errMsg, "error");
        return throwError(err);
      }))
      .subscribe((result) => {
        this.router.navigate(["/votacao"]);
      });
  }

  logout() {
    this.authentication.logout().subscribe(() => {
      this.router.navigate(["/"]);
    });
  }
}
