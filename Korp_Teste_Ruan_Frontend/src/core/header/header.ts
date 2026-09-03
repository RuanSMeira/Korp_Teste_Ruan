import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-header',
  standalone: true,
  templateUrl: './header.html',
})
export class HeaderComponent {
  @Input({ required: true }) title!: string;
  @Input({ required: true }) subtitle!: string;
  @Input() userName = 'Ana Silva';
  @Input() avatarUrl = 'https://placehold.co/32x32';
}
