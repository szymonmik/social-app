import { Component, OnInit } from '@angular/core';
import { User } from './_models/user';
import { AccountService } from './_services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title: string = 'Social App';

  constructor(private accountService: AccountService){

  }
  ngOnInit(): void {
    this.setCurrentUser();
  }

  /*getUsers() {
    this.http.get('https://localhost:7202/api/user').subscribe({
      next: response => this.users = response,
      error: error => console.log(error),
      complete: () => console.log('Requst has completed')
    })
  }*/

  setCurrentUser() {
    const userString = localStorage.getItem('user');
    if (!userString) return;
    const user: User = JSON.parse(userString);
    this.accountService.setCurrentUser(user);
  }
  
}
