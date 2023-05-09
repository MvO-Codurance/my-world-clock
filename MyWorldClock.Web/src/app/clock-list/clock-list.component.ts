import { Component, Input } from '@angular/core'
import { ClockData } from '../models/clock-data'
import { ClockService } from '../clock.service'

const NoLanguageSelected = ''

@Component({
  selector: 'app-clock-list',
  templateUrl: './clock-list.component.html'
})
export class ClockListComponent {


  @Input()
  get selectedLanguage(): string { return this._selectedLanguage; }
  set selectedLanguage(selectedLanguage: string) {
    this._selectedLanguage = (selectedLanguage && selectedLanguage.trim()) || NoLanguageSelected;
    if (this._selectedLanguage != NoLanguageSelected) {
      this.getWorldClockList(this.selectedLanguage)
    }
  }
  private _selectedLanguage = NoLanguageSelected

  public clocks: ClockData[] = []

  constructor(private service: ClockService) { }

  getWorldClockList(language: string) {
    this.service.getWorldClockList(language)
      .subscribe({
        next: result=> this.clocks = result,
        error: (error: unknown) => console.error(error)
      })
  }
}

