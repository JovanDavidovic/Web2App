import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewProcessingUserComponent } from './view-processing-user.component';

describe('ViewProcessingUserComponent', () => {
  let component: ViewProcessingUserComponent;
  let fixture: ComponentFixture<ViewProcessingUserComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ViewProcessingUserComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewProcessingUserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
