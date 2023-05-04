import { Injectable } from '@angular/core'
import { HttpClient } from '@angular/common/http'
import { ClockData } from './models/clock.data'
import { Observable } from 'rxjs'
import { environment } from '../environments/environment'

export const GET_WORLD_CLOCK_LIST_URL = 'worldclocks'

@Injectable({
  providedIn: 'root'
})
export class ClockService {

  constructor(private httpClient: HttpClient) { }

  getWorldClockList(): Observable<ClockData[]> {
    return this.httpClient.get<ClockData[]>(environment.apiBaseUrl + GET_WORLD_CLOCK_LIST_URL)
  }
}
