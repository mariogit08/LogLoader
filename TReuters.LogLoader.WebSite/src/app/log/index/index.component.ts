import { Component, OnInit } from '@angular/core';
import { LogService } from '../log.service';
import { Log } from '../log';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { NotificationService } from 'src/app/notification.service';
import { LogFilter } from '../logFilter';
import { Result } from '../result';

@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.css']
})
export class IndexComponent implements OnInit {

  logs: Log[] = [];
  form: FormGroup;
  logFilter = <LogFilter>({});
  

  constructor(public logService: LogService, public notificationService: NotificationService) { }

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
      this.notificationService.showSuccess("Log deleted successfuly!", "TReuters LogLoader")
      this.loadData()
      console.log(res);
    })
  }

  concatUserAgents(userAgents) {
    var products = userAgents.map(function (u) {
      return u.product;
    });
    var products = products.join(" | ")    
    return products;

  }

  submit() {
    console.log(this.logFilter);
    this.logService.getByFilter(this.logFilter).subscribe((res: Result<Log[]>) => {
        if(res.success){
          this.logs = res.value
        }
        else{
          this.notificationService.showError("An error has ocurred, try again", "TReuters LogLoader")
        }         
    })
  }

  getAll() {
    this.loadData()
  }
}
