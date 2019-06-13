import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewDepartureTimeComponent } from './view-departure-time.component';

describe('ViewDepartureTimeComponent', () => {
  let component: ViewDepartureTimeComponent;
  let fixture: ComponentFixture<ViewDepartureTimeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ViewDepartureTimeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewDepartureTimeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
