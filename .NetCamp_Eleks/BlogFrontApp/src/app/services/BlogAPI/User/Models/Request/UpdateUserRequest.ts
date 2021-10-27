import {Gender} from '../Enums/Gender';

export interface UpdateUserRequest {
  firstName: string;
  lastName: string;
  gender: Gender;
  birthDate: string;
  phoneNumber: string;
}
