import React from 'react';
import { useAuthStore } from '../store/useAuthStore';

import PassengerDashboard from '../screens/PassengerDashboard';
import DriverDashboard from '../screens/DriverDashboard';
import AdminDashboard from '../screens/AdminDashboard';

export default function DashboardScreen({ navigation }: any) {
  const { isLoggedIn, user } = useAuthStore();

  if (!isLoggedIn || !user) {
    return <PassengerDashboard navigation={navigation} />;
  }

  switch (user.role) {
    case 'Admin':
      return <AdminDashboard navigation={navigation} />;
    case 'Driver':
      return <DriverDashboard navigation={navigation} />;
    case 'Passenger':
    default:
      return <PassengerDashboard navigation={navigation} />;
  }
}