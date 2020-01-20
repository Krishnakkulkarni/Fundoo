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
    console.log(value.note);
    
    return value.note.item1.filter(Array => Array.title.toLowerCase().indexOf(args.toLowerCase()) !== -1 || Array.description.toLowerCase().toLowerCase().indexOf(args.toLowerCase()) !== -1)
  }

}
