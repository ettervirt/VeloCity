import { API_BASE_URL } from './config';
import type {
  LoginCommand,
  LoginResponse,
  TopUpBalanceCommand,
  BalanceDto,
  PaymentDtoPaginatedList,
  PaymentDto,
} from '../types';

class ApiService {
  private baseUrl: string;
  private token: string | null = null;

  constructor() {
    this.baseUrl = API_BASE_URL;
  }

  public setToken(token: string | null) {
    this.token = token;
  }

  private async request<T>(
    endpoint: string,
    options: RequestInit = {},
  ): Promise<T> {
    const url = `${this.baseUrl}${endpoint}`;

    const HeadersCtor = (globalThis as any).Headers;
    let headers: any;
    if (HeadersCtor) {
      headers = new HeadersCtor(options.headers as any);
      if (!headers.has('Content-Type')) {
        headers.set('Content-Type', 'application/json');
        if (this.token && !headers.has('Authorization')) {
          headers.set('Authorization', `Bearer ${this.token}`);
        }
      }
    } else {
      headers = {
        'Content-Type': 'application/json',
        ...(this.token ? { Authorization: `Bearer ${this.token}` } : {}),
        ...(options.headers as any),
      };
    }

    try {
      console.log(`API Request: ${options.method || 'GET'} ${url}`);

      const response = await fetch(url, {
        ...options,
        headers,
      });

      if (!response.ok) {
        const errorText = await response.text();
        let extractedMessage = errorText || response.statusText;

        try {
          const errorJson = JSON.parse(errorText);
          extractedMessage =
            errorJson.message ||
            errorJson.detail ||
            errorJson.title ||
            extractedMessage;
        } catch (e) {}

        throw new Error(extractedMessage);
      }

      if (response.status === 204) {
        return {} as T;
      }

      const data = await response.json();
      console.log(`API Response:`, data);
      return data;
    } catch (error) {
      console.error('API Error:', error);
      throw error;
    }
  }

  async login(data: LoginCommand): Promise<LoginResponse> {
    const response = await this.request<LoginResponse>('/users/login', {
      method: 'POST',
      body: JSON.stringify(data),
    });

    if (response.token) {
      this.setToken(response.token);
    }

    return response;
  }

  async getBalance(): Promise<BalanceDto> {
    return await this.request<BalanceDto>('/payments/balance', {
      method: 'GET',
    });
  }

  async topUp(data: TopUpBalanceCommand): Promise<TopUpBalanceCommand> {
    return await this.request<TopUpBalanceCommand>('/payments/top-up', {
      method: 'POST',
      body: JSON.stringify(data),
    });
  }

  async getPayments(
    pageNumber: number = 1,
    pageSize: number = 10,
  ): Promise<PaymentDtoPaginatedList> {
    return await this.request<PaymentDtoPaginatedList>(
      `/payments?PageNumber=${pageNumber}&PageSize=${pageSize}`,
      { method: 'GET' },
    );
  }

  async getPayment(id: number): Promise<PaymentDto> {
    return await this.request<PaymentDto>(`/payments/${id}`, {
      method: 'GET',
    });
  }
}

export default new ApiService();
