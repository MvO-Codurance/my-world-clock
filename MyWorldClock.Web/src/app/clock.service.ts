import {Injectable} from '@angular/core'
import {HttpClient} from '@angular/common/http'
import {ClockData} from './models/clock.data'
import {Observable} from 'rxjs'
import {environment} from '../environments/environment'
import {TimezoneForDisplay} from "./models/timezoneForDisplay";

export const GET_WORLD_CLOCK_LIST_URL = 'worldclocks'
export const GET_TIMEZONE_LIST_URL = 'timezones/all'
export const GET_TIMEZONE_LIST_FOR_DISPLAY_URL = 'timezones/for-display'

@Injectable({
  providedIn: 'root'
})
export class ClockService {

  constructor(private httpClient: HttpClient) { }

  getWorldClockList(): Observable<ClockData[]> {
    return this.httpClient.get<ClockData[]>(environment.apiBaseUrl + GET_WORLD_CLOCK_LIST_URL)
  }

  getTimezoneList(): Observable<string[]> {
    return this.httpClient.get<string[]>(environment.apiBaseUrl + GET_TIMEZONE_LIST_URL)
  }

  getTimezoneListForDisplay(): Observable<TimezoneForDisplay[]> {
    return this.httpClient.get<TimezoneForDisplay[]>(environment.apiBaseUrl + GET_TIMEZONE_LIST_FOR_DISPLAY_URL)
  }
}
