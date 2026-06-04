export type PaymentMethod = 'Card' | 'PayPal' | 'Giftcard';
export type Currency = 'PLN' | 'EUR' | 'USD';
export type PaymentStatus = 'Completed' | 'Pending' | 'Failed';

export interface TopUpBalanceCommand {
  amount: number;
  paymentMethod: PaymentMethod;
  currency: Currency;
}

export interface BalanceDto {
  balance: number;
}

export interface PaymentDto {
  id: number;
  amount: number;
  exchangeRate: number;
  amountInBaseCurrency: number;
  currency: Currency;
  paymentMethod: PaymentMethod;
  transactionId: string;
  status: PaymentStatus;
  createdAt: string;
}

export interface PaymentDtoPaginatedList {
  items: PaymentDto[];
  pageNumber: number;
  pageSize: number;
  totalCount: number;
  totalPages: number;
  hasPreviousPage: boolean;
  hasNextPage: boolean;
}
