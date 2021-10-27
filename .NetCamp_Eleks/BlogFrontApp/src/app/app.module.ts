import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './components/pages/home/home.component';
import { PostComponent } from './components/pages/post/post.component';
import { AuthorComponent } from './components/pages/author/author.component';
import { HeaderComponent } from './components/global/header/header.component';
import { FooterComponent } from './components/global/footer/footer.component';
import { NotFoundComponent } from './components/pages/not-found/not-found.component';
import { FeaturedPostComponent } from './components/pages/home/featured-post/featured-post.component';
import { StoryPostComponent } from './components/pages/home/storie-post/story-post.component';
import { LoginComponent } from './components/global/login/login.component';
import { RegisterComponent } from './components/global/register/register.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from './modules/material-module.module';
import { MobxAngularModule } from 'mobx-angular';
import {ReactiveFormsModule} from '@angular/forms';
import {HttpClientModule} from '@angular/common/http';
import {JwtConfigModule} from './modules/jwt-config.module';
import {httpInterceptorProviders} from './http-interceptors/interceptors.provider';
import { VerifyComponent } from './components/pages/verify/verify.component';
import { NotificationComponent } from './components/global/notification/notification.component';
import { UpdateProfileComponent } from './components/pages/update-profile/update-profile.component';
import {EnumToArrayPipe} from './pipes/EnumToArrayPipe';
import { ProfileComponent } from './components/pages/profile/profile.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    PostComponent,
    AuthorComponent,
    HeaderComponent,
    FooterComponent,
    NotFoundComponent,
    FeaturedPostComponent,
    StoryPostComponent,
    LoginComponent,
    RegisterComponent,
    VerifyComponent,
    NotificationComponent,
    UpdateProfileComponent,
    EnumToArrayPipe,
    ProfileComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    JwtConfigModule,
    MaterialModule,
    MobxAngularModule,
    ReactiveFormsModule
  ],
  providers: [httpInterceptorProviders],
  bootstrap: [AppComponent]
})
export class AppModule { }
