import { Component } from '@angular/core';
import { MatIconRegistry } from "@angular/material/icon";
import { DomSanitizer } from "@angular/platform-browser";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'freelance-platform';

  constructor(
    private matIconRegistry: MatIconRegistry,
    private domSanitizer: DomSanitizer) {
    this.matIconRegistry.addSvgIcon(
      'google-logo',
      this.domSanitizer.bypassSecurityTrustResourceUrl("../assets/google-logo.svg")
    );
    this.matIconRegistry.addSvgIcon(
      'facebook-logo',
      this.domSanitizer.bypassSecurityTrustResourceUrl("../assets/facebook-logo.svg")
    );
  }

}
