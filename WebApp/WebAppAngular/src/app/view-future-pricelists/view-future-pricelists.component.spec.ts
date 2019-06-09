import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewFuturePricelistsComponent } from './view-future-pricelists.component';

describe('ViewFuturePricelistsComponent', () => {
  let component: ViewFuturePricelistsComponent;
  let fixture: ComponentFixture<ViewFuturePricelistsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ViewFuturePricelistsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewFuturePricelistsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
