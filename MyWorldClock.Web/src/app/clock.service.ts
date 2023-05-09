import { Injectable } from '@angular/core'
import { HttpClient } from '@angular/common/http'
import { ClockData } from './models/clock-data'
import { Observable } from 'rxjs'
import { environment}  from '../environments/environment'
import { TimezoneForDisplay } from "./models/timezone-for-display";
import { Language } from "./models/language";

export const GET_WORLD_CLOCK_LIST_URL = 'worldclocks/'
export const GET_TIMEZONE_LIST_URL = 'timezones/all'
export const GET_TIMEZONE_LIST_FOR_DISPLAY_URL = 'timezones/for-display/'
export const GET_LANGUAGES_URL = 'languages'

@Injectable({
  providedIn: 'root'
})
export class ClockService {

  constructor(private httpClient: HttpClient) { }

  getWorldClockList(language: string): Observable<ClockData[]> {
    return this.httpClient.get<ClockData[]>(environment.apiBaseUrl + GET_WORLD_CLOCK_LIST_URL + language)
  }

  getTimezoneList(): Observable<string[]> {
    return this.httpClient.get<string[]>(environment.apiBaseUrl + GET_TIMEZONE_LIST_URL)
  }

  getTimezoneListForDisplay(language: string): Observable<TimezoneForDisplay[]> {
    return this.httpClient.get<TimezoneForDisplay[]>(environment.apiBaseUrl + GET_TIMEZONE_LIST_FOR_DISPLAY_URL + language)
  }

  getLanguages(): Observable<Language[]> {
    return this.httpClient.get<Language[]>(environment.apiBaseUrl + GET_LANGUAGES_URL)
  }
}
