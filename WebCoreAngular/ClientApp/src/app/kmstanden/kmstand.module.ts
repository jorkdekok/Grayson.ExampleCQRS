import { KmstandService } from './kmstand.service';
import { KmstandListComponent } from './kmstand-list.component';
import { KmstandAddComponent } from './kmstand-add.component';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { SharedModule } from '../shared/shared.module';

@NgModule({
  imports: [
    SharedModule,
    RouterModule.forChild([
      { path: 'kmstanden', component: KmstandListComponent },
      { path: 'add-kmstand', component: KmstandAddComponent },
    ]),
  ],
  declarations: [
    KmstandAddComponent,
    KmstandListComponent
  ],
  providers: [
    KmstandService
  ]
})
export class KmstandModule { }
