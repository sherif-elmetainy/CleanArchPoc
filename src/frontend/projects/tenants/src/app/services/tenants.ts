import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class Tenants {
  readonly #url = import.meta.env.TENANTSWEBAPI_HTTPS;
  readonly #httpClient = inject(HttpClient);


  public getTenants() :Observable<Array<{ id: string, name: string }>> {
    return this.#httpClient.get<Array<{ id: string, name: string }>>(`${this.#url}/api/v1/tenants`);
  }
}
