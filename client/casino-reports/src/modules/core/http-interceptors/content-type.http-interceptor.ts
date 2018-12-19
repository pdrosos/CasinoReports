import { Injectable } from '@angular/core';
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class ContentTypeHttpInterceptor implements HttpInterceptor {
  public constructor() {
  }

  public intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    request = request.clone({
      headers: request.headers.set('Accept', 'application/json'),
    });

    if (['POST', 'PUT', 'PATCH'].includes(request.method.toUpperCase()) &&
      !request.headers.has('Content-Type') &&
      !(request.body instanceof FormData)) {
      request = request.clone({
        headers: request.headers.set('Content-Type', 'application/json'),
      });
    }

    return next.handle(request);
  }
}
