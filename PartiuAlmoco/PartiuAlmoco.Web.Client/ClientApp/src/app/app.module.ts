import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { VotacaoComponent } from './components/votacao/votacao.component';
import { EntrarComponent } from './components/entrar/entrar.component';
import { NovoUsuarioComponent } from './components/novo-usuario/novo-usuario.component';
import { RestaurantesComponent } from './components/restaurantes/restaurantes.component';
import { RestauranteComponent } from './components/restaurante/restaurante.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    VotacaoComponent,
    EntrarComponent,
    NovoUsuarioComponent,
    RestaurantesComponent,
    RestauranteComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', redirectTo: '/votacao', pathMatch: 'full' },
      { path: 'votacao', component: VotacaoComponent },
      { path: 'restaurantes', component: RestaurantesComponent },
      { path: 'restaurante/:id', component: RestauranteComponent },
      { path: 'entrar', component: EntrarComponent },
      { path: 'novo-usuario', component: NovoUsuarioComponent },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
