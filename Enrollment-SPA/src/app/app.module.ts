import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AddHeaderInterceptor } from './_intercepter/addheader';
import { AppRoutingModule } from './app-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatCheckboxModule } from '@angular/material';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { MatInputModule,
  MatPaginatorModule,
  MatProgressSpinnerModule,
  MatSortModule,
  MatTableModule,
  MatIconModule,
  MatButtonModule,
  MatCardModule,
  MatFormFieldModule } from '@angular/material';
import { PaginationModule } from 'ngx-bootstrap';
import { NgxJsonViewerModule } from 'ngx-json-viewer';
import { AlertComponent } from './_components';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppComponent } from './app.component';
import { EnrollmentsComponent } from './enrollments/enrollments.component';
import { EnrollmentDetailComponent } from './enrollment-detail/enrollment-detail.component';
import { StudentRegisterComponent } from './student-register/student-register.component';
import { ErrorInterceptor } from './_intercepter';
import { ExcelService } from './_services/excelservice';
import { AlertService } from './_services/alert.service';

@NgModule({
  declarations: [
    AppComponent,
    EnrollmentsComponent,
    EnrollmentDetailComponent,
    StudentRegisterComponent,    
    AlertComponent,       
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    AppRoutingModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    MatInputModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatProgressSpinnerModule,
    MatIconModule,
    MatButtonModule,
    MatCardModule,
    MatFormFieldModule,
    MatCheckboxModule,
    PaginationModule.forRoot(),
    NgxJsonViewerModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AddHeaderInterceptor, multi: true },    
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    
    ExcelService,
    AlertService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

