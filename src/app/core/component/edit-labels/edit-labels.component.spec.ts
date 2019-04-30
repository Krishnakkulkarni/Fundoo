import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditLabelsComponent } from './edit-labels.component';

describe('EditLabelsComponent', () => {
  let component: EditLabelsComponent;
  let fixture: ComponentFixture<EditLabelsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditLabelsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditLabelsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
