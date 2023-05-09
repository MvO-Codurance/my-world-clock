export const DefaultLanguageCode: string = "en-GB"

export class User
{
  defaultLanguage: string
  selectedLanguage: string

  constructor() {
    this.defaultLanguage = DefaultLanguageCode
    this.selectedLanguage = navigator.language ?? this.defaultLanguage
  }
}
