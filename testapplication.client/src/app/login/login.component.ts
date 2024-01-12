import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from '../login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  username = '';
  password = '';

  constructor(private loginService: LoginService, private router: Router) { }

  login(): void {
    if (this.loginService.login(this.username, this.password)) {
      this.router.navigate(['/main-page']);
    } else {
      console.log('Login failed');
    }
  }
}
