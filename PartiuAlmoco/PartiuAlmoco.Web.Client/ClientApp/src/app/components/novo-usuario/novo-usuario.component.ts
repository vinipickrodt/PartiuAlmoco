import { Component, Input, OnInit } from '@angular/core';
import { ApiService } from 'src/app/services/api.service';
import { catchError } from "rxjs/operators";
import { throwError } from 'rxjs';
import { Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import Swal from 'sweetalert2';

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
        catchError(function (err, b) {
          const errMsg = (typeof err.error === 'object' && err.error !== null) ? JSON.stringify(err.error) : err.error;
          Swal.fire("Update Failed", errMsg, "error");
          return throwError(err);
        }))
      .subscribe(response => {
        this.router.navigate(['/entrar']);
      });
  }
}
