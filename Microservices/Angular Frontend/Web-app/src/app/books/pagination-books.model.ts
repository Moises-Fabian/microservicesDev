import { Books } from "./book.model";

export interface paginationBooks{
   pageSize: number;
   page: string;
   sort: string;
   sortDirection: string;
   pagesQuantity: number;
   data: Books[];
   filterValue: {};
   totalRows: number;
}
