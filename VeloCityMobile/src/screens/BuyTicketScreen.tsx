import React, { useState, useEffect } from 'react';
import { View, Text, StyleSheet, TouchableOpacity, ScrollView, Alert, ActivityIndicator, FlatList } from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';
import apiService from '../api/apiService';
import LoadingOverlay from '../components/LoadingOverlay';
import { translateApiError } from '../utils/errorTranslator';
import type { TicketTypeDto } from '../types/ticket';

interface PurchaseTicketCommand {
  ticketTypeId: number;
}

const BuyTicketScreen = () => {
  const [tickets, setTickets] = useState<TicketTypeDto[]>([]);
  const [isLoading, setIsLoading] = useState<boolean>(true);
  const [isPurchasing, setIsPurchasing] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    fetchTicketTypes();
  }, []);

  const fetchTicketTypes = async () => {
    try {
      setError(null);
      setIsLoading(true);
      const response = await apiService.getTicketTypes();
      setTickets(response);
    } catch (err: any) {
      setError(translateApiError(err.message));
      console.error('Błąd podczas pobierania typów biletów:', err);
    } finally {
      setIsLoading(false);
    }
  };

  const handlePurchaseTicket = async (ticketTypeId: number, ticketName: string, price: number) => {
    setIsPurchasing(true);
    setError(null);

    try {
      const command: PurchaseTicketCommand = {
        ticketTypeId: ticketTypeId,
      };

      await apiService.purchaseTicket(command);
      
      Alert.alert(
        'Sukces! ✅',
        `Bilet "${ticketName}" za ${price.toFixed(2)} PLN został kupiony!\n\nBilet jest gotowy do aktywacji.`,
        [{ text: 'OK', onPress: () => {} }]
      );
    } catch (err: any) {
      setError(translateApiError(err.message));
      Alert.alert(
        'Błąd',
        translateApiError(err.message),
        [{ text: 'OK' }]
      );
    } finally {
      setIsPurchasing(false);
    }
  };

  const formatDuration = (minutes: number): string => {
    if (minutes < 60) return `${minutes} min`;
    if (minutes === 60) return '1 godzina';
    if (minutes === 1440) return '1 dzień';
    const hours = minutes / 60;
    return `${hours} godzin`;
  };

  const renderTicketCard = ({ item }: { item: TicketTypeDto }) => (
    <View style={styles.ticketCard}>
      <View style={styles.ticketHeader}>
        <Text style={styles.ticketName}>{item.name}</Text>
        <Text style={styles.ticketPrice}>{item.price.toFixed(2)} PLN</Text>
      </View>

      <View style={styles.ticketDetails}>
        <View style={styles.detailRow}>
          <Text style={styles.detailLabel}>⏱️ Czas ważności:</Text>
          <Text style={styles.detailValue}>{formatDuration(item.durationInMinutes)}</Text>
        </View>
        <View style={styles.detailRow}>
          <Text style={styles.detailLabel}>🗺️ Strefy:</Text>
          <Text style={styles.detailValue}>{item.zoneLimit} {item.zoneLimit === 1 ? 'strefa' : 'strefy'}</Text>
        </View>
      </View>

      <TouchableOpacity
        style={styles.buyButton}
        onPress={() => handlePurchaseTicket(item.id, item.name, item.price)}
        disabled={isPurchasing}
      >
        <Text style={styles.buyButtonText}>
          {isPurchasing ? 'Przetwarzanie...' : '🎫 Kup bilet'}
        </Text>
      </TouchableOpacity>
    </View>
  );

  if (isLoading) {
    return (
      <SafeAreaView style={styles.container}>
        <View style={styles.centerContainer}>
          <ActivityIndicator size="large" color="#346699" />
          <Text style={styles.loadingText}>Ładowanie biletów...</Text>
        </View>
      </SafeAreaView>
    );
  }

  return (
    <SafeAreaView style={styles.container} edges={['top', 'left', 'right']}>
      <ScrollView contentContainerStyle={styles.scrollContent} showsVerticalScrollIndicator={false}>
        <View style={styles.header}>
          <Text style={styles.headerTitle}>Kup bilet</Text>
          <Text style={styles.headerSubtitle}>Wybierz najlepszy bilet dla siebie</Text>
        </View>

        {error && (
          <View style={styles.errorContainer}>
            <Text style={styles.errorText}>{error}</Text>
          </View>
        )}

        {tickets.length > 0 ? (
          <FlatList
            data={tickets}
            renderItem={renderTicketCard}
            keyExtractor={(item) => item.id.toString()}
            scrollEnabled={false}
            contentContainerStyle={styles.listContent}
          />
        ) : (
          <View style={styles.emptyContainer}>
            <Text style={styles.emptyText}>Brak dostępnych biletów.</Text>
          </View>
        )}
      </ScrollView>

      <LoadingOverlay visible={isPurchasing} />
    </SafeAreaView>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#F5F7FA',
  },
  scrollContent: {
    flexGrow: 1,
    paddingHorizontal: 16,
    paddingTop: 20,
    paddingBottom: 40,
  },
  centerContainer: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
  },
  loadingText: {
    marginTop: 12,
    color: '#666',
    fontSize: 16,
  },
  header: {
    marginBottom: 24,
  },
  headerTitle: {
    fontSize: 28,
    fontWeight: 'bold',
    color: '#1A1C1E',
  },
  headerSubtitle: {
    fontSize: 14,
    color: '#666',
    marginTop: 4,
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
  listContent: {
    gap: 12,
  },
  ticketCard: {
    backgroundColor: '#FFF',
    borderRadius: 16,
    padding: 20,
    borderWidth: 1,
    borderColor: '#E1E8EF',
    elevation: 2,
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.05,
    shadowRadius: 4,
  },
  ticketHeader: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    marginBottom: 16,
    paddingBottom: 12,
    borderBottomWidth: 1,
    borderBottomColor: '#E1E8EF',
  },
  ticketName: {
    fontSize: 18,
    fontWeight: 'bold',
    color: '#1A1C1E',
    flex: 1,
  },
  ticketPrice: {
    fontSize: 20,
    fontWeight: '900',
    color: '#346699',
  },
  ticketDetails: {
    gap: 8,
    marginBottom: 16,
  },
  detailRow: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
  },
  detailLabel: {
    fontSize: 13,
    color: '#666',
    fontWeight: '500',
  },
  detailValue: {
    fontSize: 13,
    color: '#346699',
    fontWeight: 'bold',
  },
  buyButton: {
    backgroundColor: '#346699',
    paddingVertical: 14,
    borderRadius: 12,
    alignItems: 'center',
    justifyContent: 'center',
    elevation: 2,
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.1,
    shadowRadius: 4,
  },
  buyButtonText: {
    color: '#FFF',
    fontSize: 16,
    fontWeight: 'bold',
  },
  emptyContainer: {
    alignItems: 'center',
    justifyContent: 'center',
    paddingVertical: 60,
  },
  emptyText: {
    fontSize: 16,
    color: '#666',
    textAlign: 'center',
  },
});

export default BuyTicketScreen;
