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
  model: IKmStand = { id: '', stand: null, datum: new Date() };
  inputDate: Date;
  inputTime: Date;

  constructor() { }

  ngOnInit() {
    this.inputDate = this.model.datum;
    this.inputTime = this.model.datum;
  }

  datumChanged(event) {
    console.log(JSON.stringify(event));
  }

  onDateValueChange(event) {
    this.model.datum = event;
    console.log(`model date ${event} changed`);
  }

  combineDateAndTime(date: Date, time: Date) {
    let timeString = time.toTimeString();

    let year = date.getFullYear();
    let month = date.getMonth() + 1; // Jan is 0, dec is 11
    let day = date.getDate();
    let dateString = '' + year + '-' + month + '-' + day;
    let combined = new Date(dateString + ' ' + timeString);

    return combined;
  }

  onSubmit() {
    this.model.datum = this.combineDateAndTime(this.inputDate, this.inputTime);
    console.log(`onsubmit`);
  }
}
