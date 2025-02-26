export interface QueryObject {
   symbole?: string | null;
   companyName?: string | null;
   sortBy?: string | null;
   isDescending: boolean;
   pageNumber: number;
   pageSize: number;
   toString():void;
}

// QueryObject.protoype.toString =
