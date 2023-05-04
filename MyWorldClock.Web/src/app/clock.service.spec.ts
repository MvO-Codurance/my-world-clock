import { TestBed } from '@angular/core/testing'
import {
  ClockService,
  GET_TIMEZONE_LIST_FOR_DISPLAY_URL,
  GET_TIMEZONE_LIST_URL,
  GET_WORLD_CLOCK_LIST_URL
} from './clock.service'
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing'
import { ClockData } from './models/clock.data'
import DoneCallback = jest.DoneCallback
import { environment } from '../environments/environment'
import { TimezoneForDisplay } from "./models/timezoneForDisplay";

const GET_WORLD_CLOCK_LIST_RESPONSE_EMPTY: ClockData[] = []

const GET_WORLD_CLOCK_LIST_1_CLOCK: ClockData[] = [
  {
    timezone: 'America/Los_Angeles',
    instant: '2023-05-04T10:04:19.9313127Z',
    zonedDateTime: '2023-05-04T03:04:19.9313144-07 America/Los_Angeles',
    localDateTime: '2023-05-04T03:04:19.9313209'
  }
]

const GET_WORLD_CLOCK_LIST_3_CLOCKS = [
  {
    timezone: 'America/Los_Angeles',
    instant: '2023-05-04T10:04:19.9313127Z',
    zonedDateTime: '2023-05-04T03:04:19.9313144-07 America/Los_Angeles',
    localDateTime: '2023-05-04T03:04:19.9313209'
  },
  {
    timezone: 'America/New_York',
    instant: '2023-05-04T10:04:19.9313226Z',
    zonedDateTime: '2023-05-04T06:04:19.9313227-04 America/New_York',
    localDateTime: '2023-05-04T06:04:19.9313233'
  },
  {
    timezone: 'Europe/London',
    instant: '2023-05-04T10:04:19.9313239Z',
    zonedDateTime: '2023-05-04T11:04:19.9313239+01 Europe/London',
    localDateTime: '2023-05-04T11:04:19.9313244'
  }
]

describe('ClockService', () => {
  let httpTestingController: HttpTestingController
  let service: ClockService

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule]
    })
    httpTestingController = TestBed.inject(HttpTestingController)
    service = TestBed.inject(ClockService)
  })

  afterEach(() => {
    httpTestingController.verify()
  })

  it('should be created', () => {
    expect(service).toBeTruthy()
  })

  describe('getWorldClockList', () => {

    it('given no clocks should return empty array', (done: DoneCallback) => {
      service.getWorldClockList().subscribe((result: ClockData[]) => {
        expect(result).toEqual([])
        done()
      })

      const testRequest = httpTestingController
        .expectOne(environment.apiBaseUrl + GET_WORLD_CLOCK_LIST_URL)

      testRequest.flush(GET_WORLD_CLOCK_LIST_RESPONSE_EMPTY)
    })

    it('given one clock should return array of one clock', (done: DoneCallback) => {
      const expected: ClockData[] = [
        {
          timezone: 'America/Los_Angeles',
          instant: '2023-05-04T10:04:19.9313127Z',
          zonedDateTime: '2023-05-04T03:04:19.9313144-07 America/Los_Angeles',
          localDateTime: '2023-05-04T03:04:19.9313209'
        }
      ]

      service.getWorldClockList().subscribe((result: ClockData[]) => {
        expect(result).toEqual(expected)
        done()
      })

      const testRequest = httpTestingController
        .expectOne(environment.apiBaseUrl + GET_WORLD_CLOCK_LIST_URL)
      expect(testRequest.request.method).toBe('GET')

      testRequest.flush(GET_WORLD_CLOCK_LIST_1_CLOCK)
    })

    it('given three clocks should return array of three clocks', (done: DoneCallback) => {
      const expected: ClockData[] = [
        {
          timezone: 'America/Los_Angeles',
          instant: '2023-05-04T10:04:19.9313127Z',
          zonedDateTime: '2023-05-04T03:04:19.9313144-07 America/Los_Angeles',
          localDateTime: '2023-05-04T03:04:19.9313209'
        },
        {
          timezone: 'America/New_York',
          instant: '2023-05-04T10:04:19.9313226Z',
          zonedDateTime: '2023-05-04T06:04:19.9313227-04 America/New_York',
          localDateTime: '2023-05-04T06:04:19.9313233'
        },
        {
          timezone: 'Europe/London',
          instant: '2023-05-04T10:04:19.9313239Z',
          zonedDateTime: '2023-05-04T11:04:19.9313239+01 Europe/London',
          localDateTime: '2023-05-04T11:04:19.9313244'
        }
      ]

      service.getWorldClockList().subscribe((result: ClockData[]) => {
        expect(result).toEqual(expected)
        done()
      })

      const testRequest = httpTestingController
        .expectOne(environment.apiBaseUrl + GET_WORLD_CLOCK_LIST_URL)
      expect(testRequest.request.method).toBe('GET')

      testRequest.flush(GET_WORLD_CLOCK_LIST_3_CLOCKS)
    })
  })

  describe('getTimezoneList', () => {

    it('should return array of timezones', (done: DoneCallback) => {
      let expectedCount = 596

      service.getTimezoneList().subscribe((result: string[]) => {
        expect(result.length).toEqual(expectedCount)
        expect(result[0]).toEqual('Africa/Abidjan')
        expect(result[expectedCount - 1]).toEqual('Zulu')
        done()
      })

      const testRequest = httpTestingController
        .expectOne(environment.apiBaseUrl + GET_TIMEZONE_LIST_URL)
      expect(testRequest.request.method).toBe('GET')

      let expectedTimezones = new Array<string>(expectedCount)
      expectedTimezones[0] = 'Africa/Abidjan'
      expectedTimezones[expectedCount - 1] = 'Zulu'

      testRequest.flush(expectedTimezones)
    })
  })

  describe('getTimezoneListForDisplay', () => {

    it('should return array of timezones', (done: DoneCallback) => {
      let expectedCount = 139

      service.getTimezoneListForDisplay().subscribe((result: TimezoneForDisplay[]) => {
        expect(result.length).toEqual(expectedCount)
        expect(result[0].id).toEqual('Etc/GMT+12')
        expect(result[expectedCount - 1].id).toEqual('Pacific/Kiritimati')
        done()
      })

      const testRequest = httpTestingController
        .expectOne(environment.apiBaseUrl + GET_TIMEZONE_LIST_FOR_DISPLAY_URL)
      expect(testRequest.request.method).toBe('GET')

      let expectedTimezones = new Array<TimezoneForDisplay>(expectedCount)
      expectedTimezones[0] = {
        id: 'Etc/GMT+12',
        name: ''
      }
      expectedTimezones[expectedCount - 1] = {
        id: 'Pacific/Kiritimati',
        name: ''
      }

      testRequest.flush(expectedTimezones)
    })
  })
})
