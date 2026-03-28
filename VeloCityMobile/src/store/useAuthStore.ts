import { create } from 'zustand';

interface AuthState {
  isLoggedIn: boolean;
  user: { name: string; role: string } | null;
  signIn: (name: string, role: string) => void;
  signOut: () => void;
}

export const useAuthStore = create<AuthState>((set) => ({
  isLoggedIn: false,
  user: null,
  signIn: (name, role) => set({ isLoggedIn: true, user: { name, role } }),
  signOut: () => set({ isLoggedIn: false, user: null }),
}));
