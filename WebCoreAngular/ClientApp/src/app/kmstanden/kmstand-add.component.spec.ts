import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { KmstandAddComponent } from './kmstand-add.component';

describe('KmstandAddComponent', () => {
  let component: KmstandAddComponent;
  let fixture: ComponentFixture<KmstandAddComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ KmstandAddComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(KmstandAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
