import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BasketComponent } from './basket.component';
import { BaskingRoutingModule } from './basking-routing.module';
import { SharedModule } from '../shared/shared.module';



@NgModule({
  declarations: [
    BasketComponent
  ],
  imports: [
    CommonModule,
    BaskingRoutingModule,
    SharedModule
  ]
})
export class BasketModule { }
