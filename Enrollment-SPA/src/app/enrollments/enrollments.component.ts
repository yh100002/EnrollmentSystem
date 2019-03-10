import {Component, NgModule, VERSION, Pipe, PipeTransform, OnInit} from '@angular/core';
import {BrowserModule, DomSanitizer} from '@angular/platform-browser';
import { ApiService } from '../_services/api.service';
import { StudentDto } from '../_models/dto';

@Component({
  selector: 'app-enrollments',
  templateUrl: './enrollments.component.html',
  styleUrls: ['./enrollments.component.scss']
})
export class EnrollmentsComponent implements OnInit {  
  displayedColumns: string[] = ['name', 'email', 'course'];
  data: StudentDto[] = [];
  isLoadingResults = true;  

  constructor(private api: ApiService) { }

  ngOnInit() {
    this.api.getStudents('') 
      .subscribe(res => {
        this.data = res.result;
        this.isLoadingResults = false;
      }, err => {
        console.log(err);
        this.isLoadingResults = false;
      });
  }

}
