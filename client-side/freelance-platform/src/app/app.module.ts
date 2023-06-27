import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthModule } from './modules/auth/auth.module';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { JwtInterceptor, JwtModule } from "@auth0/angular-jwt";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FreelancerModule } from './modules/freelancer/freelancer.module';
import { SharedModule } from './modules/shared/shared.module';
import { LayoutModule } from './modules/layout/layout.module';
import { JsonDateInterceptor } from './modules/shared/utils/json-date-interceptor';
import { ClientModule } from './modules/client/client.module';
import { JobModule } from './modules/job/job.module';
import { NotificationModule } from './modules/notification/notification.module';
import { ContractModule } from './modules/contract/contract.module';
import { FeedbackModule } from './modules/feedback/feedback.module';

@NgModule({
    declarations: [
        AppComponent,
    ],
    bootstrap: [AppComponent],
    imports: [
        BrowserModule,
        BrowserAnimationsModule,
        AppRoutingModule,
        HttpClientModule,
        SharedModule,
        AuthModule,
        FreelancerModule,
        ClientModule,
        JobModule,
        ContractModule,
        NotificationModule,
        FeedbackModule,
        LayoutModule,
        JwtModule.forRoot({
            config: {
              tokenGetter: () => JSON.parse(localStorage.getItem('user') as string)?.jwt
            }
        })
    ],
    providers: [
        { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true},
        { provide: HTTP_INTERCEPTORS, useClass: JsonDateInterceptor, multi: true}
    ],
})
export class AppModule { }
