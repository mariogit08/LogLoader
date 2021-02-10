import { Component, OnInit } from '@angular/core';
import { LogService } from '../log.service';
import { Log } from '../log';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.css']
})
export class IndexComponent implements OnInit {

  logs: Log[] = [];
  form: FormGroup;

  constructor(public logService: LogService) { }

  ngOnInit(): void {
    this.loadData()
    this.form = new FormGroup({
      ip: new FormControl(''),
      startDate: new FormControl(''),
      finalDate: new FormControl(''),
      userAgent: new FormControl(''),
    });
  }

  loadData(): void {
    this.logService.getAll().subscribe((data: any) => {
      this.logs = data.value;
    })
  }

  deleteLog(id) {
    this.logService.delete(id).subscribe(res => {
      this.loadData()
      console.log(res);
    })
  }

  concatUserAgents(userAgents) {
    var products = userAgents.map(function (u) {
      return u.product;
    });
    var products = products.join(" | ")
    console.log(userAgents)
    return products;

  }

  submit() {
    // console.log(this.form.value);
    // this.logService.create(this.form.value).subscribe(res => {
    //      console.log('Log created successfully!');
    //      this.router.navigateByUrl('log/index');
    // })
  }

}
