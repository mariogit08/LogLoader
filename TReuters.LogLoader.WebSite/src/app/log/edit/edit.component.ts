import { Component, OnInit } from '@angular/core';
import { LogService } from '../log.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Log } from '../log';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { DatePipe } from '@angular/common';
import { UserAgent } from '../userAgent';
@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {


  id: number;
  log: Log;
  form: FormGroup;
  date: string;
  userAgents: UserAgent[] = [];
  userAgentAdd: UserAgent = <UserAgent>({});

  constructor(
    public logService: LogService,
    private route: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.id = this.route.snapshot.params['logId'];

    this.logService.find(this.id).subscribe((data: any) => {
      this.log = data.value;
      this.loadRequestDate();
      this.userAgents = this.log.userAgents;
    });

    

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

  loadRequestDate(): void {
    let requestDate = new Date(this.log.requestDate)
    this.date = requestDate.toISOString().slice(0, 16);
  }

  get f() {
    return this.form.controls;
  }

  isLogValid() {
    return this.log.ip !== "" &&  
           this.log.requestDate !== null && 
           this.log.method !== "" && this.log.requestURL !== "" && 
           this.log.protocol !== "" && this.log.protocolVersion !== "" && this.log.statusCodeResponse && this.log.timezone          
  }

  get timezones() {
    let timezones = [{ key: "GMT", selected: false }
      , { key: "GMT+1:00", selected: false }
      , { key: "GMT+2:00", selected: false }
      , { key: "GMT+2:00", selected: false }
      , { key: "GMT+3:00", selected: false }
      , { key: "GMT+3:30", selected: false }
      , { key: "GMT+4:00", selected: false }
      , { key: "GMT+5:00", selected: false }
      , { key: "GMT+5:30", selected: false }
      , { key: "GMT+6:00", selected: false }
      , { key: "GMT+7:00", selected: false }
      , { key: "GMT+8:00", selected: false }
      , { key: "GMT+9:00", selected: false }
      , { key: "GMT+9:30", selected: false }
      , { key: "GMT+10:00", selected: false }
      , { key: "GMT+11:00", selected: false }
      , { key: "GMT+12:00", selected: false }
      , { key: "GMT-11:00", selected: false }
      , { key: "GMT-10:00", selected: false }
      , { key: "GMT-9:00", selected: false }
      , { key: "GMT-8:00", selected: false }
      , { key: "GMT-7:00", selected: false }
      , { key: "GMT-7:00", selected: false }
      , { key: "GMT-6:00", selected: false }
      , { key: "GMT-5:00", selected: false }
      , { key: "GMT-5:00", selected: false }
      , { key: "GMT-4:00", selected: false }
      , { key: "GMT-3:30", selected: false }
      , { key: "GMT-3:00", selected: false }
      , { key: "GMT-3:00", selected: false }
      , { key: "GMT-1:00", selected: false }]

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
    this.log.userAgents = this.userAgents;
    this.logService.update(this.id, this.log).subscribe(res => {
      console.log('Log updated successfully!');
      this.router.navigateByUrl('log/index');
    })
  }

}
