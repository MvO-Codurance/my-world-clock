import { Component, OnInit } from '@angular/core'
import { ClockService } from "../clock.service";
import { TimezoneForDisplay } from "../models/timezone-for-display";
import { Language } from "../models/language";
import { User } from "../models/user";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  public timezones: string[] = []
  public timezonesForDisplay: TimezoneForDisplay[] = []
  public languages: Language[] = []

  constructor(private service: ClockService, public user: User) { }

  ngOnInit() {

    this.service.getTimezoneList()
      .subscribe({
        next: result=> this.timezones = result,
        error: (error: unknown) => console.error(error)
      })

    this.getTimezonesForDisplay(this.user.selectedLanguage);

    this.service.getLanguages()
      .subscribe({
        next: result=> this.languages = result,
        error: (error: unknown) => console.error(error)
      })
  }

  getTimezonesForDisplay(language: string) {
    this.service.getTimezoneListForDisplay(language)
      .subscribe({
        next: result=> this.timezonesForDisplay = result,
        error: (error: unknown) => console.error(error)
      })
  }

  onLanguageChange(language: string){
    this.getTimezonesForDisplay(language)
  }

}
