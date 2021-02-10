import { Component, OnInit } from '@angular/core';
import { LogService } from '../log.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Log } from '../log';

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
    private router: Router
    ) { }

  ngOnInit(): void {
    this.id = this.route.snapshot.params['logId'];
    
    this.logService.find(this.id).subscribe((data: Log)=>{
      this.log = data;
    });
  }

}
