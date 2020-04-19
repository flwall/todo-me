import { Component, OnInit } from '@angular/core';
import { AuthService} from '../../services/auth.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  constructor(private Auth: AuthService, private router: Router) { }

  ngOnInit(): void {
  }

  loginUser(event) {
    event.preventDefault();
    const target = event.target;
    const user = target.querySelector('#user').value;
    const password = target.querySelector('#password').value;
    this.Auth.loginUser(user, password).subscribe(response => {
      if (response === 'Logged in user ' + user + ' successfully') {
        console.log('funkt');
        this.router.navigate(['home']);
        this.Auth.setLoggedIn(true);
      }
    }, error => {
      window.alert('invalid credentials');
    });
    console.log('Input from user:' + user, password);
  }
}
