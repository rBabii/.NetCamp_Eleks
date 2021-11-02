import {Gender} from '../Enums/Gender';

export interface GetUserResponse {
  email: string;
  firstName: string;
  lastName: string;
  gender: Gender;
  birthDate: Date;
  phoneNumber: string;
  blogUrl: string;
  imageUrl: string;
}
