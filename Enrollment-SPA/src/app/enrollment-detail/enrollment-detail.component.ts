import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ApiService } from '../_services/api.service';
import { StudentDto } from '../_models/dto';


@Component({
  selector: 'app-enrollment-detail',
  templateUrl: './enrollment-detail.component.html',
  styleUrls: ['./enrollment-detail.component.scss']
})
export class EnrollmentDetailComponent implements OnInit {  
  student: StudentDto = { id: 0, name: '', email: '', age:0, courseName: ''};
  isLoadingResults = true;

  constructor(private route: ActivatedRoute, private api: ApiService, private router: Router) { }

  ngOnInit() {
    console.log(this.route.snapshot.params['id']);
    this.getEnrollmentDetails(this.route.snapshot.params['id']);
  }

  getEnrollmentDetails(id) {
    console.log(id);
    /*
    this.api.getProduct(id)
      .subscribe(data => {
        this.product = data;
        console.log(this.product);
        this.isLoadingResults = false;
      });*/
  }

  deleteEnrollment(id) {
    this.isLoadingResults = true;
    /*
    this.api.deleteProduct(id)
      .subscribe(res => {
          this.isLoadingResults = false;
          this.router.navigate(['/products']);
        }, (err) => {
          console.log(err);
          this.isLoadingResults = false;
        }
      );
      */
  }

}
