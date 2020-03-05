import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DevicesDebugComponent } from './devices-debug.component';

describe('DevicesDebugComponent', () => {
  let component: DevicesDebugComponent;
  let fixture: ComponentFixture<DevicesDebugComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DevicesDebugComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DevicesDebugComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
