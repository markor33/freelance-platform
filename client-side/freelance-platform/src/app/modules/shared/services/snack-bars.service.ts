import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
export class SnackBarsService {

  constructor(private snackBar: MatSnackBar) { }

  primary(message: string) {
    this.snackBar.open(message, 'Ok', {
      duration: 3000
    });
  }

  error(message: string) {
    this.snackBar.open(message, 'Ok', {
      duration: 3000,
      panelClass: ['error-snackbar']
    });
  }

}
