import {Pipe, PipeTransform} from '@angular/core';
import {KeyValueEnumItem} from '../models/KeyValueEnumItem';

@Pipe({
  name: 'enumToArray'
})
export class EnumToArrayPipe implements PipeTransform {
  transform(data: any): KeyValueEnumItem[] {
    const keys = Object.keys(data);

    const enumKeys = keys.slice(keys.length / 2);
    const values = keys.slice(0, keys.length / 2);

    const mapped: KeyValueEnumItem[] = enumKeys.map((k, index) => {
      return {
        key: k,
        value: values[index]
      };
    });

    return mapped;
  }
}
