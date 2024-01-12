import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  private isLoggedIn = false;

  login(username: string, password: string): boolean {
    console.log('Attempted login with username:', username, 'and password:', password);

    if (username === 'a' && password === 'a') {
      this.isLoggedIn = true;
      console.log('Login successful!');
      return true;
    } else {
      console.log('Login failed!');
      return false;
    }
  }


  logout(): void {
    this.isLoggedIn = false;
  }

  isLoggedInUser(): boolean {
    return this.isLoggedIn;
  }
}
