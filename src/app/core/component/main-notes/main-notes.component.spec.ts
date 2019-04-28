import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MainNotesComponent } from './main-notes.component';

describe('MainNotesComponent', () => {
  let component: MainNotesComponent;
  let fixture: ComponentFixture<MainNotesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MainNotesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MainNotesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
