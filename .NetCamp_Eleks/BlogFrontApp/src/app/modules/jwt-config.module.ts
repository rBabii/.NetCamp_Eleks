import {JwtModule} from '@auth0/angular-jwt';
import {NgModule} from '@angular/core';

export function _tokenGetter(): string {
  return localStorage.getItem('accessToken');
}

@NgModule({
  imports: [
    JwtModule.forRoot({
      config: {
        tokenGetter: _tokenGetter,
        allowedDomains: ['localhost:5000', 'localhost:5001'],
        authScheme: 'Bearer ',
        skipWhenExpired: true
      }
    })
  ],
  exports: [
    JwtModule
  ]
})



export class JwtConfigModule {}
