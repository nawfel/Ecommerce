import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BasketComponent } from './basket.component';
import { BaskingRoutingModule } from './basking-routing.module';


@NgModule({
  declarations: [
    BasketComponent
  ],
  imports: [
    CommonModule,
    BaskingRoutingModule,

  ]
})
export class BasketModule { }
