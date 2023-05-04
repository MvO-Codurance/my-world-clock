import { TestBed } from '@angular/core/testing'

import { ClockService } from './clock.service'
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing'

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
})
