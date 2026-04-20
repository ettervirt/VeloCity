import React from 'react';
import { createBottomTabNavigator } from '@react-navigation/bottom-tabs';
import Icon, {IoniconsIconName}  from '@react-native-vector-icons/ionicons';
import DashboardScreen from '../screens/DashboardScreen';
import RoutesScreen from '../screens/DashboardScreen';
import ProfileScreen from '../screens/DashboardScreen';

const Tab = createBottomTabNavigator();

const MainTabNavigator = () => {
  return (
    <Tab.Navigator
      screenOptions={({ route }) => ({
        headerShown: false,
        tabBarActiveTintColor: "#346699",
        tabBarInactiveTintColor: 'gray',
        tabBarIcon: ({ color, size }) => {
          var iconName: IoniconsIconName = 'home-outline';

          if (route.name === 'Dashboard') iconName = 'home-outline';
          else if (route.name === 'Routes') iconName = 'map-outline';
          else if (route.name === 'Profile') iconName = 'person-outline';

          return <Icon name={iconName} size={size} color={color} />;
        },
      })}
    >
      <Tab.Screen
        name="Dashboard"
        component={DashboardScreen}
        options={{ title: 'Pulpit' }}
      />
      <Tab.Screen
        name="Routes"
        component={RoutesScreen}
        options={{ title: 'Trasy' }}
      />
      <Tab.Screen
        name="Profile"
        component={ProfileScreen}
        options={{ title: 'Profil' }}
      />
    </Tab.Navigator>
  );
};

export default MainTabNavigator;
