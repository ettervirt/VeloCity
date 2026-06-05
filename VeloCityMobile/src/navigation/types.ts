export type RootStackParamList = {
  Login: undefined;
  Register: undefined;
  Main: {
    screen?: string;
    params?: {
      screen: string;
    };
  };
};

export type MainTabParamList = {
  Dashboard: { userName: string };
  Routes: undefined;
  Wallet: undefined;
  Profile: undefined;
};
