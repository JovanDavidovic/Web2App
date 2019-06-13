import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteDepartureTimeComponent } from './delete-departure-time.component';

describe('DeleteDepartureTimeComponent', () => {
  let component: DeleteDepartureTimeComponent;
  let fixture: ComponentFixture<DeleteDepartureTimeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DeleteDepartureTimeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DeleteDepartureTimeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
