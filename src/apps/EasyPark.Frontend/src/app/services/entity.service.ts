import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EntityService {

  private baseUrl = 'http://entity-service.services.185.136.234.221.xip.io/';
  // private baseUrl = 'https://localhost:5001/';

  constructor(private http: HttpClient) { }

  entities$(): Observable<any> {
    return this.http.get(this.baseUrl + 'api/entity');
  }

  entity$(id: string): Observable<any> {
    return this.http.get(this.baseUrl + 'api/entity/' + id);
  }

  entityCreateResponse$(name: string, description: string, deviceCount: number, postCode: string) {
    return this.http.post(this.baseUrl + 'api/entity', {
      name: name,
      description: description,
      deviceCount: deviceCount,
      postCode: postCode
    });
  }
}
