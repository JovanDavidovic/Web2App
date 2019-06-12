import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateDepartureTimeComponent } from './create-departure-time.component';

describe('CreateDepartureTimeComponent', () => {
  let component: CreateDepartureTimeComponent;
  let fixture: ComponentFixture<CreateDepartureTimeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateDepartureTimeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateDepartureTimeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
