import React from 'react';
import { createMaterialTopTabNavigator } from '@react-navigation/material-top-tabs';
import { SafeAreaView } from 'react-native-safe-area-context';

import WalletScreen from '../screens/WalletScreen';
import PaymentHistoryScreen from '../screens/PaymentHistoryScreen';

const TopTab = createMaterialTopTabNavigator();

const WalletTabNavigator = () => {
  return (
    <SafeAreaView style={{ flex: 1, backgroundColor: '#F5F7FA' }} edges={['top']}>
      <TopTab.Navigator
        screenOptions={{
          tabBarLabelStyle: { fontSize: 14, fontWeight: 'bold', textTransform: 'none' },
          tabBarActiveTintColor: '#346699',
          tabBarInactiveTintColor: '#666',
          tabBarIndicatorStyle: { backgroundColor: '#346699', height: 3 },
          tabBarStyle: { backgroundColor: '#FFF', elevation: 2 },
        }}
      >
        <TopTab.Screen 
          name="WalletTab" 
          component={WalletScreen} 
          options={{ title: 'Doładuj konto' }} 
        />
        <TopTab.Screen 
          name="HistoryTab" 
          component={PaymentHistoryScreen} 
          options={{ title: 'Historia transakcji' }} 
        />
      </TopTab.Navigator>
    </SafeAreaView>
  );
};

export default WalletTabNavigator;