import { Component, OnInit } from '@angular/core';
import { LogService } from '../log.service';
import { Router } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { UserAgent } from '../userAgent';
import { Log } from '../log';
import { NotificationService } from 'src/app/notification.service';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})
export class CreateComponent implements OnInit {
  form: FormGroup;
  batchFile: File = null;
  userAgents: UserAgent[] = [];
  userAgentAdd: UserAgent = <UserAgent>({});

  constructor(
    public logService: LogService,
    private router: Router,
    private notifyService: NotificationService
  ) { }

  ngOnInit(): void {
    this.form = new FormGroup({
      ip: new FormControl('', [Validators.required]),
      userIdentifier: new FormControl(''),
      requestDate: new FormControl('', [Validators.required]),
      timezone: new FormControl(''),
      method: new FormControl('', [Validators.required]),
      requestURL: new FormControl('', [Validators.required]),
      protocol: new FormControl('', [Validators.required]),
      protocolVersion: new FormControl('', [Validators.required]),
      statusCodeResponse: new FormControl('', [Validators.required]),
      port: new FormControl(''),
      originUrl: new FormControl(''),
      userAgent: new FormControl('')
    });

  }

  get f() {
    return this.form.controls;
  }

  get timezones() {
    let timezones = [
      { key:"0+100",value: "GMT+1:00", selected: false }
      , { key:"0+200",value: "GMT+2:00", selected: false } 
      , { key:"0+300",value: "GMT+3:00", selected: false } 
      , { key:"0+400",value: "GMT+4:00", selected: false } 
      , { key:"0+500",value: "GMT+5:00", selected: false } 
      , { key:"0+600",value: "GMT+6:00", selected: false }
      , { key:"0+700",value: "GMT+7:00", selected: false } 
      , { key:"0+800",value: "GMT+8:00", selected: false }
      , { key:"0+900",value: "GMT+9:00", selected: false }
      , { key:"+1000",value: "GMT+10:00", selected: false } 
      , { key:"+1100",value: "GMT+11:00", selected: false } 
      , { key:"+1200",value: "GMT+12:00", selected: false } 
      , { key:"-0100",value: "GMT-1:00", selected: false }
      , { key:"-0200",value: "GMT-2:00", selected: false } 
      , { key:"-0300",value: "GMT-3:00", selected: false } 
      , { key:"-0400",value: "GMT-4:00", selected: false } 
      , { key:"-0500",value: "GMT-5:00", selected: false } 
      , { key:"-0600",value: "GMT-6:00", selected: false }
      , { key:"-0700",value: "GMT-7:00", selected: false } 
      , { key:"-0800",value: "GMT-8:00", selected: false }
      , { key:"-0900",value: "GMT-9:00", selected: false }
      , { key:"-1000",value: "GMT-10:00", selected: false } 
      , { key:"-1100",value: "GMT-11:00", selected: false } 
      , { key:"-1200",value: "GMT-12:00", selected: false } ]

    return timezones;
  }

  deleteAgent(agent) {
    this.userAgents = this.userAgents.filter(function (value, index, arr) {
      return value != agent
    });
  }

  addUserAgent() {
    this.userAgents.push(this.userAgentAdd)
  }

  canAddAgent() {
    let agent = this.userAgentAdd;
    return agent.product && agent.productVersion && agent.systemInformation;
  }

  submit() {
    let log = <Log>({ ...this.form.value });
    log.userAgents = this.userAgents;

    this.logService.create(log).subscribe(res => {
      
      this.showToasterSuccess();
      this.router.navigateByUrl('log/index');
    })
  }

  uploadFileToActivity() {
    this.logService.createByFile(this.batchFile).subscribe(data => {
      // do something, if upload success
    }, error => {
      console.log(error);
    });
  }

  showToasterSuccess() {
    this.notifyService.showSuccess("Successful operation !!", "LogLoader")
  }

  showToasterError() {
    this.notifyService.showError("Something is wrong", "LogLoader")
  }

  showToasterInfo(info) {
    this.notifyService.showInfo(info, "LogLoader")
  }

  showToasterWarning(warning) {
    this.notifyService.showWarning(warning, "LogLoader")
  }

}
