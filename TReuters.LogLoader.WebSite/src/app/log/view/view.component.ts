import { Component, OnInit } from '@angular/core';
import { LogService } from '../log.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Log } from '../log';
import { Result } from '../result';
import { NotificationService } from 'src/app/notification.service';

@Component({
  selector: 'app-view',
  templateUrl: './view.component.html',
  styleUrls: ['./view.component.css']
})
export class ViewComponent implements OnInit {

  id: number;
  log: Log;

  constructor(
    public logService: LogService,
    private route: ActivatedRoute,
    private router: Router,
    public notificationService: NotificationService
    ) { }

  ngOnInit(): void {
    this.id = this.route.snapshot.params['logId'];
    
    this.logService.find(this.id).subscribe((data: Result<Log>)=>{      
      if(data.success){        
        this.log = data.value;          
      }
      else{
        this.notificationService.showError("Cannot load log, try again", "TReuters.Log Loader")
      }        
    });
  }

  concatUserAgents(userAgents) {
    var products = userAgents.map(function (u) {
      let systemInformation = u.systemInformation ? "("+ u.systemInformation+")":"";
      return u.product + " " + u?.productVersion + " "+ systemInformation;
    });
    var products = products.join(" | ")    
    return products;

  }

}
