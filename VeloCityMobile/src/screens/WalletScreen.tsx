import React, { useState, useCallback } from 'react';
import {
  StyleSheet,
  View,
  Text,
  TextInput,
  TouchableOpacity,
  ScrollView,
  Alert,
} from 'react-native';
import { useFocusEffect } from '@react-navigation/native';

import apiService from '../api/apiService';
import LoadingOverlay from '../components/LoadingOverlay';
import { translateApiError } from '../utils/errorTranslator';
import type { PaymentMethod, TopUpBalanceCommand } from '../types';

const PAYMENT_METHODS: { id: PaymentMethod; name: string; icon: string }[] = [
  { id: 'Card', name: 'Karta płatnicza', icon: '💳' },
  { id: 'PayPal', name: 'PayPal', icon: '🅿️' },
];

const PRESET_AMOUNTS = [10, 20, 50, 100];

const WalletScreen = () => {
  const [balance, setBalance] = useState<number>(0);
  const [amountInput, setAmountInput] = useState<string>('');

  const [selectedMethod, setSelectedMethod] = useState<PaymentMethod>('Card');
  
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  useFocusEffect(
    useCallback(() => {
      fetchBalance();
    }, [])
  );

  const fetchBalance = async () => {
    try {
      setError(null);
      const response = await apiService.getBalance();
      setBalance(response.balance);
    } catch (err: any) {
      setError(translateApiError(err.message));
    }
  };

  const handleTopUp = async () => {
    const amountToTopUp = parseFloat(amountInput.replace(',', '.'));

    if (isNaN(amountToTopUp) || amountToTopUp <= 0) {
      setError('Wpisz poprawną kwotę doładowania.');
      return;
    }

    setIsLoading(true);
    setError(null);

    try {
      const command: TopUpBalanceCommand = {
        amount: amountToTopUp,
        paymentMethod: selectedMethod,
        currency: 'PLN',
      };

      await apiService.topUp(command);
      
      Alert.alert('Sukces!', `Konto zostało zasilone kwotą ${amountToTopUp.toFixed(2)} PLN.`);
      setAmountInput('');
      await fetchBalance();

    } catch (err: any) {
      setError(translateApiError(err.message));
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <View style={styles.container}>
      <ScrollView contentContainerStyle={styles.scrollContent} showsVerticalScrollIndicator={false}>
        
        <View style={styles.balanceCard}>
          <Text style={styles.balanceLabel}>Dostępne środki</Text>
          <Text style={styles.balanceValue}>{balance.toFixed(2)} PLN</Text>
        </View>

        {error && (
          <View style={styles.errorContainer}>
            <Text style={styles.errorText}>{error}</Text>
          </View>
        )}

        <Text style={styles.sectionTitle}>Doładuj konto</Text>
        
        <View style={styles.amountContainer}>
          <TextInput
            style={styles.amountInput}
            placeholder="Wpisz kwotę (np. 15.50)"
            placeholderTextColor="#999"
            keyboardType="numeric"
            value={amountInput}
            onChangeText={text => {
              setAmountInput(text);
              setError(null);
            }}
          />
          <Text style={styles.currencyLabel}>PLN</Text>
        </View>

        <View style={styles.presetContainer}>
          {PRESET_AMOUNTS.map(preset => (
            <TouchableOpacity
              key={preset}
              style={styles.presetButton}
              onPress={() => {
                setAmountInput(preset.toString());
                setError(null);
              }}>
              <Text style={styles.presetText}>+{preset}</Text>
            </TouchableOpacity>
          ))}
        </View>

        <Text style={styles.sectionTitle}>Metoda płatności</Text>
        <View style={styles.methodsContainer}>
          {PAYMENT_METHODS.map(method => (
            <TouchableOpacity
              key={method.id}
              style={[
                styles.methodCard,
                selectedMethod === method.id && styles.methodCardActive,
              ]}
              onPress={() => setSelectedMethod(method.id)}>
              <Text style={styles.methodIcon}>{method.icon}</Text>
              <Text
                style={[
                  styles.methodText,
                  selectedMethod === method.id && styles.methodTextActive,
                ]}>
                {method.name}
              </Text>
            </TouchableOpacity>
          ))}
        </View>

        <TouchableOpacity style={styles.submitButton} onPress={handleTopUp}>
          <Text style={styles.submitButtonText}>Sfinalizuj płatność</Text>
        </TouchableOpacity>
      </ScrollView>
      <LoadingOverlay visible={isLoading} />
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#F5F7FA',
  },
  scrollContent: {
    padding: 24,
    paddingTop: 40,
    paddingBottom: 60,
  },
  balanceCard: {
    backgroundColor: '#346699',
    padding: 24,
    borderRadius: 16,
    alignItems: 'center',
    marginBottom: 32,
    elevation: 4,
    shadowColor: '#346699',
    shadowOffset: { width: 0, height: 4 },
    shadowOpacity: 0.3,
    shadowRadius: 8,
  },
  balanceLabel: {
    color: '#E1E8EF',
    fontSize: 16,
    fontWeight: '500',
    marginBottom: 8,
  },
  balanceValue: {
    color: '#FFF',
    fontSize: 40,
    fontWeight: '900',
    letterSpacing: 1,
  },
  errorContainer: {
    backgroundColor: '#FFE5E5',
    padding: 12,
    borderRadius: 8,
    borderLeftWidth: 4,
    borderLeftColor: '#FF3B30',
    marginBottom: 20,
  },
  errorText: {
    color: '#D92D20',
    fontSize: 14,
    fontWeight: '500',
  },
  sectionTitle: {
    fontSize: 18,
    fontWeight: '700',
    color: '#1A2A3A',
    marginBottom: 16,
  },
  amountContainer: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FFF',
    borderRadius: 12,
    borderWidth: 1,
    borderColor: '#E1E8EF',
    marginBottom: 16,
    paddingHorizontal: 16,
  },
  amountInput: {
    flex: 1,
    fontSize: 20,
    color: '#1A2A3A',
    paddingVertical: 16,
  },
  currencyLabel: {
    fontSize: 18,
    fontWeight: 'bold',
    color: '#666',
  },
  presetContainer: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    marginBottom: 32,
  },
  presetButton: {
    backgroundColor: '#E1E8EF',
    paddingVertical: 10,
    paddingHorizontal: 16,
    borderRadius: 8,
    flex: 1,
    marginHorizontal: 4,
    alignItems: 'center',
  },
  presetText: {
    color: '#346699',
    fontWeight: '700',
    fontSize: 16,
  },
  methodsContainer: {
    flexDirection: 'row',
    gap: 12,
    marginBottom: 40,
  },
  methodCard: {
    flex: 1,
    backgroundColor: '#FFF',
    padding: 16,
    borderRadius: 12,
    borderWidth: 2,
    borderColor: '#E1E8EF',
    alignItems: 'center',
    gap: 8,
  },
  methodCardActive: {
    borderColor: '#346699',
    backgroundColor: '#F0F5FA',
  },
  methodIcon: {
    fontSize: 24,
  },
  methodText: {
    color: '#666',
    fontWeight: '600',
    fontSize: 14,
  },
  methodTextActive: {
    color: '#346699',
  },
  submitButton: {
    backgroundColor: '#346699',
    padding: 18,
    borderRadius: 12,
    alignItems: 'center',
    elevation: 3,
  },
  submitButtonText: {
    color: '#FFF',
    fontSize: 18,
    fontWeight: 'bold',
  },
});

export default WalletScreen;
