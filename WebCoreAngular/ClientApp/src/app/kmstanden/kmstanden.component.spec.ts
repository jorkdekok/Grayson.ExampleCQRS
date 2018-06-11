import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { KmstandenComponent } from './kmstanden.component';

describe('KmstandenComponent', () => {
  let component: KmstandenComponent;
  let fixture: ComponentFixture<KmstandenComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ KmstandenComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(KmstandenComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
