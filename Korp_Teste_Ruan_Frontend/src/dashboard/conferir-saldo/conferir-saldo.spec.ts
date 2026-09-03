import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ConferirSaldoComponent } from './conferir-saldo';

describe('ConferirSaldoComponent', () => {
  let component: ConferirSaldoComponent;
  let fixture: ComponentFixture<ConferirSaldoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ConferirSaldoComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(ConferirSaldoComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });
});