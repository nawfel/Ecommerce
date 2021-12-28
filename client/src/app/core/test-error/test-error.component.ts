import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-test-error',
  templateUrl: './test-error.component.html',
  styleUrls: ['./test-error.component.scss']
})
export class TestErrorComponent implements OnInit {
baseUrl =environment.apiUrl;
  constructor(private httpclient : HttpClient) { }

  ngOnInit(): void {
  }

  get404Error(){
    this.httpclient.get(this.baseUrl+'products/42').subscribe(response=>{
      console.log(response);
    },error=>{
      console.log(console.log(error));      
    })
  }
  get500Error(){
    this.httpclient.get(this.baseUrl+'/buggy/servererror').subscribe(response=>{
      console.log(response);
    },error=>{
      console.log(console.log(error));      
    })
  }
  get400Error(){
    this.httpclient.get(this.baseUrl+'buggy/badrequest').subscribe(response=>{
      console.log(response);
    },error=>{
      console.log(console.log(error));      
    })
  }
  get400ValidationError(){
    this.httpclient.get(this.baseUrl+'products/fortytwo').subscribe(response=>{
      console.log(response);
    },error=>{
      console.log(console.log(error));      
    })
  }

}
