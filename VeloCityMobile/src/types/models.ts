export type UserRole = 'Passenger' | 'Driver' | 'Admin';

export interface LoginCommand {
  email: string | null;
  password: string | null;
}

export interface LoginResponse {
    token: string | null;
    name: string | null;
    role: string | null;
}

export interface RegisterCommand {
  name: string | null;
  surname: string | null;
  email: string | null;
  password: string | null;
}

export interface RegisterResponse {
  id: number | null;
  message: string | null;
}