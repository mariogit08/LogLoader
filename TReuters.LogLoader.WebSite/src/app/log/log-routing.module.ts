import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { IndexComponent } from './index/index.component';
import { ViewComponent } from './view/view.component';
import { CreateComponent } from './create/create.component';
import { BatchCreateComponent } from './batchCreate/batchCreate.component';
import { EditComponent } from './edit/edit.component';

const routes: Routes = [
  { path: 'log', redirectTo: 'log/index', pathMatch: 'full'},
  { path: 'log/index', component: IndexComponent },
  { path: 'log/:logId/view', component: ViewComponent },
  { path: 'log/create', component: CreateComponent },
  { path: 'log/:logId/edit', component: EditComponent },
  { path: 'log/batch', component: BatchCreateComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LogRoutingModule { }
