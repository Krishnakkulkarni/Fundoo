import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ImagecropComponent } from './imagecrop.component';

describe('ImagecropComponent', () => {
  let component: ImagecropComponent;
  let fixture: ComponentFixture<ImagecropComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ImagecropComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ImagecropComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
