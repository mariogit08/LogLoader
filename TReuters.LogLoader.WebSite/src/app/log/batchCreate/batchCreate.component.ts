import { Component, OnInit } from '@angular/core';
import { LogService } from '../log.service';
import { Router } from '@angular/router';
import { FormGroup, FormControl, ReactiveFormsModule } from '@angular/forms';
import { AppComponent } from 'src/app/app.component';
import { NotificationService } from 'src/app/notification.service';


@Component({
  selector: 'app-batchCreate',
  templateUrl: './batchCreate.component.html',
  styleUrls: ['./batchCreate.component.css']
})
export class BatchCreateComponent implements OnInit {
  form: FormGroup;
  _batchFile: File = null;  
  
  constructor(
    public logService: LogService,
    private router: Router,
    private notificationService: NotificationService
  ) { }
  title: string;

  ngOnInit(): void {
    this.form = new FormGroup({
      batchFile: new FormControl(''),     
    });
  }

  get f() {
    return this.form.controls;
  }

  get batchFile(){
    return this._batchFile;
  }

  handleFileInput(files: FileList) {
    this._batchFile = files.item(0);
  }

  submit() {
    if(!this._batchFile){
      alert("Select a file to upload!")
      return;
    }
    
    this.logService.createByFile(this._batchFile).subscribe(res => {
      this.notificationService.showSuccess("Log created successfully!", "TReuters.LogLoader")      
      // this.router.navigateByUrl('log/index');      
    }, error => {
      this.notificationService.showError("Error cannot create logs", "TReuters.LogLoader")      
      console.log(error);
    });
  }
}
