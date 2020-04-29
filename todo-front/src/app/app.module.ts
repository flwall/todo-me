import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpModule } from '@angular/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './pages/login/login.component';
import { RegisterComponent } from './pages/register/register.component';
import { HomeComponent } from './pages/home/home.component';
import {RouterModule} from '@angular/router';
import {HttpClientModule} from '@angular/common/http';
import { AuthService } from './services/auth.service';
import { TooltipModule } from 'ngx-bootstrap/tooltip';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    HomeComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    HttpModule,
    TooltipModule.forRoot(),
    RouterModule.forRoot([{
      path: '',
      component: LoginComponent
    },
      {
        path: 'home',
        component: HomeComponent,
        canActivate: []
      },
      {
        path: 'register',
        component: RegisterComponent
      }])
  ],
  providers: [AuthService],
  bootstrap: [AppComponent]
})
export class AppModule { }
