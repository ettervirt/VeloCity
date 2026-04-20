import { API_BASE_URL } from './config';
import type { LoginCommand, LoginResponse } from '../types/models';

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
        ...(this.token ? { 'Authorization': `Bearer ${this.token}` } : {}),
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
        throw new Error(
          `HTTP ${response.status}: ${errorText || response.statusText}`,
        );
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
}

export default new ApiService();
