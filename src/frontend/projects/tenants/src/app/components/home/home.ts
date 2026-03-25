import { Component, inject } from '@angular/core';
import { Tenants } from '../../services/tenants';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'tenants-app-home',
  imports: [AsyncPipe],
  templateUrl: './home.html',
  styleUrl: './home.scss',
})
export class Home {
  #tenantsService = inject(Tenants);

  readonly tenants = this.#tenantsService.getTenants();
}
