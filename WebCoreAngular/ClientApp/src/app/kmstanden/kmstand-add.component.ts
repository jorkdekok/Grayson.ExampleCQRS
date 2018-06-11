import { Component, OnInit } from '@angular/core';
import { IKmStand } from './kmstand.model';
import { BsDatepickerModule } from 'ngx-bootstrap';

@Component({
  selector: 'app-kmstand-add',
  templateUrl: './kmstand-add.component.html',
  styleUrls: ['./kmstand-add.component.css']
})
export class KmstandAddComponent implements OnInit {
  pageTitle = 'Nieuwe kmstand';
  model: IKmStand = { id: '', stand: 0, datum: new Date() };

  constructor() { }

  ngOnInit() {
  }

  datumChanged(event) {
    console.log(JSON.stringify(event));
  }
}
