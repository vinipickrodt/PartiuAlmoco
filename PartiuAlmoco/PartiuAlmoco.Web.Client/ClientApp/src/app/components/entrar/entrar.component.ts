import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from 'src/app/services/api.service';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { environment } from 'src/environments/environment';

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
    this.authentication.login(this.email, this.password).subscribe((result) => {
      this.router.navigate(["/votacao"]);
    });
  }

  logout() {
    this.authentication.logout();
  }
}
