import { Component, OnInit } from '@angular/core';
import { KmstandService } from './kmstand.service';
import { IKmStand } from './kmstand.model';

@Component({
  selector: 'app-kmstand-list',
  templateUrl: './kmstand-list.component.html',
  styleUrls: ['./kmstand-list.component.css']
})
export class KmstandListComponent implements OnInit {
  pageTitle = 'Kmstanden lijst';
  errorMessage: string;
  kmstanden: IKmStand[];

  constructor(private _kmstandService: KmstandService) { }

  ngOnInit() {
    this._kmstandService.getKmstanden()
      .subscribe(kmstanden => {
        this.kmstanden = kmstanden;
      },
        error => this.errorMessage = <any>error);
  }

}
