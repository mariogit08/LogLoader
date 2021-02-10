import { Component, OnInit } from '@angular/core';
import { LogService } from '../log.service';
import { Router } from '@angular/router';
import { FormGroup, FormControl, ReactiveFormsModule } from '@angular/forms';
import { AppComponent } from 'src/app/app.component';


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
    private router: Router
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
      console.log('Log created successfully!');
      this.router.navigateByUrl('log/index');
      
    }, error => {
      console.log(error);
    });
  }
}
