import { Component, OnInit } from '@angular/core'
import { ClockService } from "../clock.service";
import { TimezoneForDisplay } from "../models/timezoneForDisplay";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  public timezones: string[] = []
  public timezonesForDisplay: TimezoneForDisplay[] = []

  constructor(private service: ClockService) { }

  ngOnInit() {

    this.service.getTimezoneList()
      .subscribe({
        next: result=> this.timezones = result,
        error: (error: unknown) => console.error(error)
      })

    this.service.getTimezoneListForDisplay()
      .subscribe({
        next: result=> this.timezonesForDisplay = result,
        error: (error: unknown) => console.error(error)
      })
  }

}
