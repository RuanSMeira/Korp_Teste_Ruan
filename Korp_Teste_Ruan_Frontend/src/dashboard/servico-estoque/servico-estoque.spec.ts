import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ServicoEstoque } from './servico-estoque';

describe('ServicoEstoque', () => {
  let component: ServicoEstoque;
  let fixture: ComponentFixture<ServicoEstoque>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ServicoEstoque],
    }).compileComponents();

    fixture = TestBed.createComponent(ServicoEstoque);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
