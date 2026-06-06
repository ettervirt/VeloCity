export interface TimetableDto {
  id: number;
  tripId: number;
  stopId: number;
  stopName: string;
  sequence: number;
  departureTime: string;
}

export interface TimetableResponse {
  items: TimetableDto[];
  pageNumber: number;
  pageSize: number;
  totalCount: number;
  totalPages: number;
  hasPreviousPage: boolean;
  hasNextPage: boolean;
}