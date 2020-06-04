import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  isFalse: boolean;
  constructor(private Auth: AuthService, private router: Router) {}

  ngOnInit(): void {
    this.isFalse = false;
  }

  async loginUser(event) {
    event.preventDefault();
    const target = event.target;
    const user = target.querySelector('#user').value;
    const password = target.querySelector('#password').value;
    this.Auth.loginUser(user, password).subscribe(
      (data) => {
        if (data.status === 200) {
          this.router.navigate(['home']);
          this.Auth.setLoggedIn(true);
        }
      },
      (error) => {
        this.isFalse = true;
      }
    );
  }
}
