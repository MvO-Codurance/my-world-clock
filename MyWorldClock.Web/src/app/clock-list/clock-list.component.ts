import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';

@Component({
  selector: 'app-clock-list',
  templateUrl: './clock-list.component.html'
})
export class ClockListComponent {
  public clocks: ClockData[] = [];

  constructor(http: HttpClient) {
    http.get<ClockData[]>(environment.apiBaseUrl + 'worldclocks')
      .subscribe({
        next: result=> this.clocks = result,
        error: error => console.error(error)
      });
  }
}

interface ClockData {
  instant: Date;
  localDateTime: Date;
  timezone: string;
  zonedDateTime: Date;
}
