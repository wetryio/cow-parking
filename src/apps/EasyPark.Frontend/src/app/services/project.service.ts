import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {

  constructor(private http: HttpClient) { }

  projects$(): Observable<any> {
    return this.http.get(environment.baseApiAddress + 'project');
  }

  project$(id: string): Observable<any> {
    return this.http.get(environment.baseApiAddress + 'project/' + id);
  }

  projectToken$(id: string): Observable<any> {
    return this.http.get(environment.baseApiAddress + 'project/' + id + '/token');
  }

  projectCreateResponse$(name: string, allowOrigins: string[]) {
    return this.http.post(environment.baseApiAddress + 'project', {
      name: name,
      allowOrigins:allowOrigins
    });
  }
}
