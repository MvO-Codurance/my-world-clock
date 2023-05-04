import { Component } from '@angular/core'
import { HttpClient } from '@angular/common/http'
import { environment } from '../../environments/environment'
import { ClockData } from '../models/clock.data'

@Component({
  selector: 'app-clock-list',
  templateUrl: './clock-list.component.html'
})
export class ClockListComponent {
  public clocks: ClockData[] = []

  constructor(http: HttpClient) {
    http.get<ClockData[]>(environment.apiBaseUrl + 'worldclocks')
      .subscribe({
        next: result=> this.clocks = result,
        error: (error: unknown) => console.error(error)
      })
  }
}

