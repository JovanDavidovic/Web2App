import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewPricelistsComponent } from './view-pricelists.component';

describe('ViewPricelistsComponent', () => {
  let component: ViewPricelistsComponent;
  let fixture: ComponentFixture<ViewPricelistsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ViewPricelistsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewPricelistsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
