import { ComponentFixture, TestBed } from '@angular/core/testing';
import { CadastrarProduto } from './cadastrar-produto';

describe('CadastrarProduto', () => {
  let component: CadastrarProduto;
  let fixture: ComponentFixture<CadastrarProduto>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CadastrarProduto],
    }).compileComponents();

    fixture = TestBed.createComponent(CadastrarProduto);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
