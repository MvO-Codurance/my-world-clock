export const DefaultLanguageCode: string = "en-GB"
export const DefaultTimezone: string = "Europe/London"

export class User
{
  defaultLanguage: string
  selectedLanguage: string

  defaultTimezone: string
  selectedTimezone: string

  constructor() {
    this.defaultLanguage = DefaultLanguageCode
    this.selectedLanguage = navigator.language ?? this.defaultLanguage

    this.defaultTimezone = DefaultTimezone
    this.selectedTimezone = Intl.DateTimeFormat().resolvedOptions().timeZone ?? this.defaultTimezone
  }
}
