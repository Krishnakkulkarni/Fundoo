import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'search'
})
export class SearchPipe implements PipeTransform {

  transform(value: any, args?: any): any {

    if (args == undefined) {
      return value;
    }
    if (!value) return null;
    if (!args) return value.note;

    console.log('in pipe', value.note, args);

    return value.note.filter(Array => Array.title.toLowerCase().indexOf(args.toLowerCase()) !== -1 || Array.description.toLowerCase().toLowerCase().indexOf(args.toLowerCase()) !== -1)
  }

}
