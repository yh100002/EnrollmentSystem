export interface StudentDto {
  id: number;
  name: string;
  email: string;
  age: number;
  courseName: string; 
}


export class Envelope<T> {
  result: T;    
  errorMessage:string;
  timeGenerated: Date;  
}
