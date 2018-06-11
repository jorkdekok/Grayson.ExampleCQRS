import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { KmstandListComponent } from './kmstand-list.component';

describe('KmstandListComponent', () => {
  let component: KmstandListComponent;
  let fixture: ComponentFixture<KmstandListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ KmstandListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(KmstandListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
