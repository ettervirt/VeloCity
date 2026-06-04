export interface TicketDto { 
  id: number;
  ticketTypeName: string;
  price: number;
  purchasedAt: string;
  validFrom: string;
  validTo: string;
  vehicleId: number;
  isValidated: boolean;
}

export interface TicketTypeDto {
  id: number;
  name: string;
  price: number;
  durationInMinutes: number;
  zoneLimit: number;
}