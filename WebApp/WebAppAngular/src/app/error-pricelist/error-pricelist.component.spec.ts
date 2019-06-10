import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ErrorPricelistComponent } from './error-pricelist.component';

describe('ErrorPricelistComponent', () => {
  let component: ErrorPricelistComponent;
  let fixture: ComponentFixture<ErrorPricelistComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ErrorPricelistComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ErrorPricelistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
