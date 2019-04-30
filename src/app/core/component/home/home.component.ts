import { MediaMatcher } from '@angular/cdk/layout';
import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  mobileQuery: MediaQueryList;
  private _mobileQueryListener: () => void;

  constructor(private router: Router, changeDetectorRef: ChangeDetectorRef, media: MediaMatcher, public snackBar: MatSnackBar) {
    this.mobileQuery = media.matchMedia('(max-width: 600px)');
    this._mobileQueryListener = () => changeDetectorRef.detectChanges();
    this.mobileQuery.addListener(this._mobileQueryListener);

  }
  ngOnInit() {

  }
  onLogout() {
    localStorage.removeItem('token');
    this.router.navigate(['user/login']);
    this.snackBar.open("logout successful", "close", { duration: 1500 });
  }
  Note() {
    this.router.navigate(['home/MainNotes'])
  }

  Reminder(){
    this.router.navigate(['home/Reminder'])
  }

  EditLables(){
    this.router.navigate(['home/EditLables'])
  }

  Archive(){
    this.router.navigate(['home/Archive'])
  }

  Trash(){
    this.router.navigate(['home/Trash'])
  }
}