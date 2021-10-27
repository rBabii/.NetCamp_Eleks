import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {HomeComponent} from './components/pages/home/home.component';
import {AuthorComponent} from './components/pages/author/author.component';
import {PostComponent} from './components/pages/post/post.component';
import {NotFoundComponent} from './components/pages/not-found/not-found.component';
import {RegisterComponent} from './components/global/register/register.component';
import {LoginComponent} from './components/global/login/login.component';
import {LoginRegisterGuardGuard} from './guards/login-register-guard.guard';
import {VerifyComponent} from './components/pages/verify/verify.component';
import {UpdateProfileComponent} from './components/pages/update-profile/update-profile.component';
import {ProfileComponent} from './components/pages/profile/profile.component';

const routes: Routes = [
  { path: '', component: HomeComponent},
  { path: 'author', component: AuthorComponent},
  { path: 'post', component: PostComponent},
  { path: 'register', component: RegisterComponent, canActivate: [LoginRegisterGuardGuard]},
  { path: 'login', component: LoginComponent, canActivate: [LoginRegisterGuardGuard]},
  { path: 'update-profile', component: UpdateProfileComponent},
  { path: 'verify', component: VerifyComponent},
  { path: 'profile', component: ProfileComponent},
  { path: '**', component: NotFoundComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
