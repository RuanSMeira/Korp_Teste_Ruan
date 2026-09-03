import { ComponentFixture, TestBed } from '@angular/core/testing';
import { LoginEmpresa } from './login-empresa';

describe('LoginEmpresa', () => {
  let component: LoginEmpresa;
  let fixture: ComponentFixture<LoginEmpresa>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LoginEmpresa],
    }).compileComponents();

    fixture = TestBed.createComponent(LoginEmpresa);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
