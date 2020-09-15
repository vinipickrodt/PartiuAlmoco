import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from '../interfaces/user';
import { AuthenticationService } from '../services/authentication.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  constructor(private authentication: AuthenticationService, private router: Router) { }

  ngOnInit() {
    this.authentication.authenticationChanged.subscribe((user) => {
      this.currentUser = user;
      this.isAuthenticated = user != null;
    });
  }

  isExpanded = false;

  isAuthenticated = false;
  currentUser: User = null;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  logout() {
    this.authentication.logout();
    this.router.navigate(["/"]);
    return false;
  }
}
