import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LogRoutingModule } from './log-routing.module';
import { IndexComponent } from './index/index.component';
import { ViewComponent } from './view/view.component';
import { CreateComponent } from './create/create.component';
import { BatchCreateComponent } from './batchCreate/batchCreate.component';
import { EditComponent } from './edit/edit.component';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';


@NgModule({
  declarations: [IndexComponent, ViewComponent, CreateComponent, EditComponent, BatchCreateComponent],
  imports: [
    CommonModule,
    LogRoutingModule,
    FormsModule,
    ReactiveFormsModule
  ],
  bootstrap: [IndexComponent, ViewComponent, CreateComponent, EditComponent, BatchCreateComponent],
})
export class LogModule { }
