import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ServicoFaturamento } from './servico-faturamento';

describe('ServicoFaturamento', () => {
  let component: ServicoFaturamento;
  let fixture: ComponentFixture<ServicoFaturamento>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ServicoFaturamento],
    }).compileComponents();

    fixture = TestBed.createComponent(ServicoFaturamento);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
