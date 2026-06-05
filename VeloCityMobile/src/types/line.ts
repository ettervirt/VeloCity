export interface Line {
  id: number;
  name: string;
  number: string;
  description?: string;
}

export interface LineCommand {
  name: string;
  number: string;
  description?: string;
}