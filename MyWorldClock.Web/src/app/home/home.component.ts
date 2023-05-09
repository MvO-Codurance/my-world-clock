import { Component, OnInit } from '@angular/core'
import { ClockService } from "../clock.service";
import { TimezoneForDisplay } from "../models/timezoneForDisplay";
import { Language } from "../models/language";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  public timezones: string[] = []
  public timezonesForDisplay: TimezoneForDisplay[] = []
  public languages: Language[] = []

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

    this.service.getLanguages()
      .subscribe({
        next: result=> this.languages = result,
        error: (error: unknown) => console.error(error)
      })
  }

}
