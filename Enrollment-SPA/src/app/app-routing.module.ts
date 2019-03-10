import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { EnrollmentsComponent } from './enrollments/enrollments.component';
import { EnrollmentDetailComponent } from './enrollment-detail/enrollment-detail.component';
import { StudentRegisterComponent } from './student-register/student-register.component';

const routes: Routes = [  
  {
    path: 'enrollments',
    component: EnrollmentsComponent,
    data: { title: 'List of Enrollments' }    
  },
  {
    path: 'enrollment-detail/:id',
    component: EnrollmentDetailComponent,
    data: { title: 'Enrollment Details' }     
  },
  {
    path: 'student-register',
    component: StudentRegisterComponent,
    data: { title: 'Student Register' }    
  },  
  {path: '**', redirectTo: 'enrollments', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
