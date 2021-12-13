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
import {ProfileComponent} from './components/pages/profile/profile.component';
import {UnAuthorizedUserGuard} from './guards/unAuthorizedUser.guard';
import {SetupBlogComponent} from './components/pages/setup-blog/setup-blog.component';
import {SetupedBlogGuard} from './guards/setuped-blog.guard';
import {BlogListComponent} from './components/pages/blog-list/blog-list.component';
import {SingleBlogPageComponent} from './components/pages/single-blog-page/single-blog-page.component';
import {CreatePostComponent} from './components/global/create-post/create-post.component';
import {ResetPasswordComponent} from './components/pages/reset-password/reset-password.component';
import {EmailVerifyHelpComponent} from './components/global/email-verify-help/email-verify-help.component';
import {VerifiedUserGuard} from './guards/verified-user.guard';
import {SinglePostPageComponent} from './components/pages/single-post-page/single-post-page.component';

const routes: Routes = [
  { path: '', component: HomeComponent},
  { path: 'author', component: AuthorComponent},
  { path: 'post', component: PostComponent},
  { path: 'register', component: RegisterComponent, canActivate: [LoginRegisterGuardGuard]},
  { path: 'login', component: LoginComponent, canActivate: [LoginRegisterGuardGuard]},
  { path: 'verify', component: VerifyComponent},
  { path: 'blog', component: BlogListComponent},
  { path: 'post/create', component: CreatePostComponent},
  { path: 'blog/:blogUrl', component: SingleBlogPageComponent},
  { path: 'post/:postId', component: SinglePostPageComponent},
  { path: 'profile', component: ProfileComponent, canActivate: [UnAuthorizedUserGuard]},
  { path: 'setup-blog', component: SetupBlogComponent, canActivate: [UnAuthorizedUserGuard, SetupedBlogGuard]},
  { path: 'reset-password', component: ResetPasswordComponent, canActivate: [LoginRegisterGuardGuard]},
  { path: 'email-verify-help', component: EmailVerifyHelpComponent, canActivate: [UnAuthorizedUserGuard, VerifiedUserGuard]},
  { path: '**', component: NotFoundComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
