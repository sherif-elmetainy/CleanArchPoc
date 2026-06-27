import { inject, Injectable } from '@angular/core';
import { firstValueFrom, Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class CustomersService {
  readonly #url = import.meta.env.POCWEBAPI_HTTPS;
  readonly #httpClient = inject(HttpClient);


  public getCustomers() :Observable<Array<{ id: string, name: string }>> {
    return this.#httpClient.get<Array<{ id: string, name: string }>>(`${this.#url}/api/v1/customers`);
  }

  public addCustomer(customer: { id: string, firstName: string, lastName:string, email: string }) :Promise<void> {
    return firstValueFrom(this.#httpClient.post<void>(`${this.#url}/api/v1/customers`, customer));
  }
}
