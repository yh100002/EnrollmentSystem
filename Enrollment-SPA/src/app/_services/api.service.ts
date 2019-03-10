import { Injectable } from '@angular/core';
import { Observable, of, throwError } from 'rxjs';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { catchError, tap, map } from 'rxjs/operators';
import { StudentDto, Envelope } from '../_models/dto';

const httpOptions = {
  headers: new HttpHeaders({'Content-Type': 'application/json'})
};
const apiUrl = "https://localhost:44339/api/students";

@Injectable({
  providedIn: 'root'
})
export class ApiService {    
  constructor(private http: HttpClient) { }

  getStudents (enrolled): Observable<Envelope<StudentDto[]>> {    
    let results: Envelope<StudentDto[]> = new Envelope<StudentDto[]>();
    const url = `${apiUrl}/getStudents?enrolled=${enrolled}`;
    return this.http.get<Envelope<StudentDto[]>>(url)
      .pipe(
        map(response => {        
          console.log(response);
          return response;
        })     
      );
  }

  registerStudent (student): Observable<any> {
    const url = `${apiUrl}/register/`;
    return this.http.post<StudentDto>(url, student, httpOptions).pipe(      
      tap((studentDto: StudentDto) => console.log(`register student w/ id=${studentDto.name}`)),
      catchError(this.handleError<StudentDto>('registerStudent'))
    );
  }

/*
  getProduct(id: string): Observable<ProductData> {
    const url = `${apiUrl}/getProduct/${id}`;
    return this.http.get<ProductData>(url).pipe(
      tap(_ => console.log(`fetched product id=${id}`)),
      catchError(this.handleError<ProductData>(`getProduct id=${id}`))
    );
  }

  addProduct (product): Observable<ProductData> {
    const url = `${apiUrl}/addProduct/`;
    return this.http.post<ProductData>(url, product, httpOptions).pipe(
      // tslint:disable-next-line:no-shadowed-variable
      tap((product: ProductData) => console.log(`added product w/ id=${product.zamroID}`)),
      catchError(this.handleError<ProductData>('addProduct'))
    );
  }

  updateProduct (id, product): Observable<any> {    
    const url = `${apiUrl}/update/`;
    product.zamroID = id;
    return this.http.put<ProductData>(url, product, httpOptions).pipe(
      tap(_ => console.log(`updated product id=${product.description}`)),
      catchError(this.handleError<ProductData>('updateBook'))
    );
  }

  deleteProduct (id: string): Observable<ProductData> {
    const url = `${apiUrl}/${id}`;

    return this.http.delete<ProductData>(url, httpOptions).pipe(
      tap(_ => console.log(`deleted product id=${id}`)),
      catchError(this.handleError<ProductData>('deleteProduct'))
    );
  }
*/

  private handleError<T> (operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }
}
