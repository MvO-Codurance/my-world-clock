import { Component, OnInit } from '@angular/core'
import { ClockData } from '../models/clock-data'
import { ClockService } from '../clock.service'

@Component({
  selector: 'app-clock-list',
  templateUrl: './clock-list.component.html'
})
export class ClockListComponent implements OnInit {
  public clocks: ClockData[] = []

  constructor(private service: ClockService) { }

  ngOnInit() {
    this.service.getWorldClockList()
      .subscribe({
        next: result=> this.clocks = result,
        error: (error: unknown) => console.error(error)
      })
  }
}

