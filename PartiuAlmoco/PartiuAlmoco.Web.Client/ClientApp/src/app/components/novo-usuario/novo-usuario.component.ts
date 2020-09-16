import { Component, Input, OnInit } from '@angular/core';
import { ApiService } from 'src/app/services/api.service';
import { catchError } from "rxjs/operators";
import { throwError } from 'rxjs';
import { Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-novo-usuario',
  templateUrl: './novo-usuario.component.html',
  styleUrls: ['./novo-usuario.component.css']
})
export class NovoUsuarioComponent implements OnInit {
  constructor(private api: ApiService, private router: Router, private fb: FormBuilder) { }

  form: FormGroup;

  ngOnInit() {
    this.form = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['email', Validators.required],
      password: ['password', Validators.required]
    });
  }



  createUser() {
    const firstName = this.form.get("firstName").value;
    const lastName = this.form.get("lastName").value;
    const email = this.form.get("email").value;
    const password = this.form.get("password").value;
    const fullName = firstName + " " + lastName;
    const friendlyName = firstName;

    this.api.createUser(fullName, friendlyName, email, password)
      .pipe(
        catchError(function (a, b) {
          return throwError(a);
        }))
      .subscribe(response => {
        this.router.navigate(['/entrar']);
      });
  }
}
