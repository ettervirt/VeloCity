export interface Line {
  id: number;
  name: string;
  isActive: boolean;
}

export interface LineCommand {
  name: string;
  number: string;
  description?: string;
}

export interface RouteStop {
  stopId: number;
  stopName: string;
  sequence: number;
  direction: string;
}

export interface Stop {
  stopId: number;
  stopName: string;
  sequence: number;
  direction: number;
}

export interface LineDetailsDto {
  id: number;
  name: string;
  stops: Stop[];
}

export interface PaginatedList<T> {
  items: T[];
  pageNumber: number;
  pageSize: number;
  totalCount: number;
  totalPages: number;
  hasPreviousPage: boolean;
  hasNextPage: boolean;
}