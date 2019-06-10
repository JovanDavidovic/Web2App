import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ModifyPricelistComponent } from './modify-pricelist.component';

describe('ModifyPricelistComponent', () => {
  let component: ModifyPricelistComponent;
  let fixture: ComponentFixture<ModifyPricelistComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ModifyPricelistComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ModifyPricelistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
