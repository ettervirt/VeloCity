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
