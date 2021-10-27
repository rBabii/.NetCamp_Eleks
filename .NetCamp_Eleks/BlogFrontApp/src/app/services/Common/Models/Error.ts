import {ErrorType} from '../Enums/ErrorType';

export interface Error {
  errorMessages: string[];
  errorType: ErrorType;
}
