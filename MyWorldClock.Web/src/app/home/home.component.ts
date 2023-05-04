import { Component, OnInit } from '@angular/core'
import { ClockService } from "../clock.service";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  public timezones: string[] = []

  constructor(private service: ClockService) { }

  ngOnInit() {
    this.service.getTimezoneList()
      .subscribe({
        next: result=> this.timezones = result,
        error: (error: unknown) => console.error(error)
      })
  }

}
