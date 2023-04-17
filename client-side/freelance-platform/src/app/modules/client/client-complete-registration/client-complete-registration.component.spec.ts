import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClientCompleteRegistrationComponent } from './client-complete-registration.component';

describe('ClientCompleteRegistrationComponent', () => {
  let component: ClientCompleteRegistrationComponent;
  let fixture: ComponentFixture<ClientCompleteRegistrationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ClientCompleteRegistrationComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ClientCompleteRegistrationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
