import { Component, inject, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CustomersService } from './services/customers.service';
import { AsyncPipe, JsonPipe } from '@angular/common';

@Component({
  selector: 'booking-app-root',
  imports: [RouterOutlet, AsyncPipe, JsonPipe],
  templateUrl: './app.html',
  styleUrl: './app.scss',
})
export class App {
  protected readonly title = signal('booking');

  readonly #customers = inject(CustomersService);
  readonly customers = this.#customers.getCustomers();

  async addCustomer() {
    await this.#customers.addCustomer({ id: '1', firstName: 'John', lastName: 'Doe', email: 'john.doe-example.com' });
  }
}
