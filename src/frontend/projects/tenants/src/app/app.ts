import { Component, inject, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Tenants } from './services/tenants';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'tenants-app-root',
  imports: [RouterOutlet, AsyncPipe],
  templateUrl: './app.html',
  styleUrl: './app.scss',
})
export class App {
  protected readonly title = signal('tenants');

}
