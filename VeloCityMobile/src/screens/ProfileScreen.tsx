import React, { useState, useEffect } from 'react';
import { View, Text, StyleSheet, TouchableOpacity } from 'react-native';
import { useIsFocused } from '@react-navigation/native';
import { SafeAreaView } from 'react-native-safe-area-context';
import { NativeStackNavigationProp } from '@react-navigation/native-stack';
import { RootStackParamList } from '../navigation/types';
import { useAuthStore } from '../store/useAuthStore';
import apiService from '../api/apiService';

interface ProfileScreenProps {
  navigation: NativeStackNavigationProp<RootStackParamList>;
}

export default function ProfileScreen({ navigation }: ProfileScreenProps) {
  const { user, signOut } = useAuthStore();
  const [balance, setBalance] = useState<string>('0.00');
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const isFocused = useIsFocused();
  
  const handleLogout = () => {
    apiService.setToken(null);
    signOut();
  };

  useEffect(() => {
      const fetchData = async () => {
        setIsLoading(true);
        try {
          const walletResponse = await apiService.getBalance();
          setBalance(Number(walletResponse.balance).toFixed(2));
          }
            catch (error) {
          console.error('Błąd podczas pobierania danych pulpitu:', error);
        } finally {
          setIsLoading(false);
        }
      };
  
      if(isFocused) fetchData();
    }, [isFocused]);

  if (!user) return null;

  return (
    <SafeAreaView style={styles.container} edges={['top', 'left', 'right']}>
      <View style={styles.profileHeader}>
        <View style={styles.avatarCircle}>
          <Text style={styles.avatarLetter}>
            {user.name ? user.name.charAt(0).toUpperCase() : 'U'}
          </Text>
        </View>
        <Text style={styles.userName}>{user.name}</Text>
        <Text style={styles.userRole}>
          Rola: {user.role === 'User' ? 'Pasażer' : user.role}
        </Text>
      </View>

      <View style={styles.walletCard}>
        <Text style={styles.walletLabel}>Stan Twojego konta</Text>
        <Text style={styles.walletBalance}>{balance} PLN</Text>

        <TouchableOpacity
          style={styles.depositButton}
          onPress={() => navigation.navigate('Main', { screen: 'PaymentHistory', params: {screen: 'WalletTab'} })}
        >
          <Text style={styles.depositButtonText}>Doładuj konto</Text>
        </TouchableOpacity>
      </View>

      <View style={styles.actionsContainer}>
        <TouchableOpacity style={styles.actionRow} onPress={() => navigation.navigate('Main', { screen: 'Dashboard' })}>
          <Text style={styles.actionText}>Moje Bilety</Text>
          <Text style={styles.arrowIcon}>→</Text>
        </TouchableOpacity>

        <TouchableOpacity style={styles.actionRow} onPress={() => navigation.navigate('Main', {screen: 'PaymentHistory', params: { screen: 'HistoryTab' }})}>
          <Text style={styles.actionText}>Historia Transakcji</Text>
          <Text style={styles.arrowIcon}>→</Text>
        </TouchableOpacity>
      </View>

      <TouchableOpacity style={styles.logoutButton} onPress={handleLogout}>
        <Text style={styles.logoutButtonText}>Wyloguj się</Text>
      </TouchableOpacity>
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#F5F7FA',
    paddingHorizontal: 20,
    paddingTop: 12,
  },
  profileHeader: {
    alignItems: 'center',
    marginTop: 20,
    marginBottom: 24,
  },
  avatarCircle: {
    width: 80,
    height: 80,
    borderRadius: 40,
    backgroundColor: '#346699',
    justifyContent: 'center',
    alignItems: 'center',
    marginBottom: 12,
  },
  avatarLetter: { 
    color: '#FFF', 
    fontSize: 36, 
    fontWeight: 'bold',
  },
  userName: { 
    fontSize: 22, 
    fontWeight: 'bold', 
    color: '#1A1C1E',
  },
  userRole: { 
    fontSize: 14, 
    color: '#666', 
    marginTop: 4,
  },
  walletCard: { 
    backgroundColor: '#FFF', 
    padding: 20, 
    borderRadius: 16, 
    alignItems: 'center', 
    marginBottom: 24, 
    elevation: 2,
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.05,
    shadowRadius: 4,
  },
  walletLabel: { 
    fontSize: 14, 
    color: '#666', 
    marginBottom: 4,
  },
  walletBalance: { 
    fontSize: 32, 
    fontWeight: 'bold', 
    color: '#1A1C1E', 
    marginBottom: 12,
  },
  depositButton: { 
    backgroundColor: '#E1EBF5', 
    paddingVertical: 10, 
    paddingHorizontal: 20, 
    borderRadius: 20,
  },
  depositButtonText: { 
    color: '#346699', 
    fontWeight: '600',
  },
  actionsContainer: { 
    backgroundColor: '#FFF', 
    borderRadius: 16, 
    overflow: 'hidden', 
    marginBottom: 'auto',
    borderWidth: 1,
    borderColor: '#E1E8EF',
  },
  actionRow: { 
    padding: 18, 
    borderBottomWidth: 1, 
    borderBottomColor: '#F0F0F0', 
    flexDirection: 'row', 
    justifyContent: 'space-between',
    alignItems: 'center',
  },
  actionText: { 
    fontSize: 16, 
    color: '#1A1C1E', 
    fontWeight: '500',
  },
  arrowIcon: {
    fontSize: 18,
    color: '#346699',
    fontWeight: 'bold',
  },
  logoutButton: { 
    backgroundColor: '#FFE5E5', 
    padding: 16, 
    borderRadius: 12, 
    alignItems: 'center', 
    marginBottom: 24, 
  },
  logoutButtonText: { 
    color: '#FF3B30', 
    fontSize: 16, 
    fontWeight: 'bold',
  },
});