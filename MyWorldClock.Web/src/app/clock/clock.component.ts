import { AfterViewInit, Component, ElementRef, Input, OnDestroy, ViewChild } from '@angular/core'
import { ClockData } from '../models/clock.data'
import { v4 as uuid } from 'uuid'

@Component({
  selector: 'app-clock',
  templateUrl: './clock.component.html',
  styleUrls: ['./clock.component.css']
})
export class ClockComponent implements AfterViewInit, OnDestroy {

  @Input() clock: ClockData | undefined

  @ViewChild('clockHourHand') clockHourHand!: ElementRef
  @ViewChild('clockMinuteHand') clockMinuteHand!: ElementRef
  @ViewChild('clockSecondHand') clockSecondHand!: ElementRef
  @ViewChild('clockDatetimeString') clockDatetimeString!: ElementRef

  public hourHandId: string | undefined
  public minuteHandId: string | undefined
  public secondHandId: string | undefined

  public hourHandIdReference: string | undefined
  public minuteHandIdReference: string | undefined
  public secondHandIdReference: string | undefined

  private date: Date | undefined
  private interval: number | undefined

  constructor() {
    const idSuffix = uuid()

    this.hourHandId = `clockHourHand-${idSuffix}`
    this.minuteHandId = `clockMinuteHand-${idSuffix}`
    this.secondHandId = `clockSecondHand-${idSuffix}`

    this.hourHandIdReference = `#${this.hourHandId}`
    this.minuteHandIdReference = `#${this.minuteHandId}`
    this.secondHandIdReference = `#${this.secondHandId}`
  }

  ngAfterViewInit() {
    this.initClock()
    this.interval = window.setInterval(() => this.updateClock(), 1000)
  }

  ngOnDestroy() {
    if (this.interval){
      window.clearInterval(this.interval)
    }
  }

  private initClock(){
    if (this.clock) {
      this.date = new Date(Date.parse(this.clock.localDateTime))
      const seconds = this.date.getSeconds()
      let minutes = this.date.getMinutes()
      let hours = this.date.getHours()
      hours = (hours > 12) ? hours - 12 : hours
      minutes = (minutes * 60) + seconds
      hours = (hours * 3600) + minutes

      this.clockSecondHand.nativeElement.setAttribute('transform', 'rotate(' + 360 * (seconds / 60) + ',192,192)')
      this.clockMinuteHand.nativeElement.setAttribute('transform', 'rotate(' + 360 * (minutes / 3600) + ',192,192)')
      this.clockHourHand.nativeElement.setAttribute('transform', 'rotate(' + 360 * (hours / 43200) + ',192,192)')
      this.clockDatetimeString.nativeElement.textContent = this.date.toLocaleString()
    }
  }

  private updateClock() {
    if (this.date) {
      this.date.setSeconds(this.date.getSeconds() + 1)
      this.clockDatetimeString.nativeElement.textContent = this.date.toLocaleString()
    }
  }

}
