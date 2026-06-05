import React, { useState, useEffect, useCallback } from 'react';
import {
  StyleSheet,
  View,
  Text,
  FlatList,
  ActivityIndicator,
} from 'react-native';
import { useIsFocused } from '@react-navigation/native';
import apiService from '../api/apiService';
import type { PaymentDto, PaymentStatus, PaymentMethod } from '../types';

const STATUS_TRANSLATIONS: Record<PaymentStatus, string> = {
  Completed: 'Zakończona',
  Pending: 'Oczekująca',
  Failed: 'Odrzucona',
};

const METHOD_TRANSLATIONS: Record<PaymentMethod, string> = {
  Card: 'Karta płatnicza',
  PayPal: 'PayPal',
  Giftcard: 'Karta podarunkowa',
};

const STATUS_COLORS: Record<PaymentStatus, string> = {
  Completed: '#34C759',
  Pending: '#FF9500',
  Failed: '#FF3B30',
};

const PaymentHistoryScreen = () => {
  const isFocused = useIsFocused();

  const [payments, setPayments] = useState<PaymentDto[]>([]);
  const [page, setPage] = useState<number>(1);
  const [hasNextPage, setHasNextPage] = useState<boolean>(true);

  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [isRefreshing, setIsRefreshing] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);

  const fetchPayments = async (pageNumber: number, shouldRefresh = false) => {
    if (isLoading || (!hasNextPage && !shouldRefresh)) return;

    shouldRefresh ? setIsRefreshing(true) : setIsLoading(true);
    setError(null);

    try {
      const response = await apiService.getPayments(pageNumber, 10);

      setPayments(prev =>
        shouldRefresh ? response.items : [...prev, ...response.items],
      );
      setHasNextPage(response.hasNextPage);
      setPage(response.pageNumber);
    } catch (err: any) {
      setError(err.message || 'Nie udało się pobrać historii płatności.');
    } finally {
      setIsLoading(false);
      setIsRefreshing(false);
    }
  };

  useEffect(() => {
    if(isFocused) fetchPayments(1, true);
  }, [isFocused]);

  const handleRefresh = () => {
    fetchPayments(1, true);
  };

  const handleLoadMore = () => {
    if (hasNextPage && !isLoading) {
      fetchPayments(page + 1);
    }
  };

  const renderPaymentItem = ({ item }: { item: PaymentDto }) => {
    const date = new Date(item.createdAt).toLocaleDateString('pl-PL', {
      day: '2-digit',
      month: '2-digit',
      year: 'numeric',
      hour: '2-digit',
      minute: '2-digit',
    });

    return (
      <View style={styles.card}>
        <View style={styles.cardHeader}>
          <Text style={styles.methodText}>Płatność: {METHOD_TRANSLATIONS[item.paymentMethod] || item.paymentMethod}</Text>
          <Text style={styles.dateText}>{date}</Text>
        </View>

        <View style={styles.cardBody}>
          <Text style={styles.amountText}>
            +{item.amount.toFixed(2)} {item.currency}
          </Text>
          <View
            style={[
              styles.statusBadge,
              { backgroundColor: STATUS_COLORS[item.status] || '#999' },
            ]}
          >
            <Text style={styles.statusText}>
              {STATUS_TRANSLATIONS[item.status] || item.status}
            </Text>
          </View>
        </View>
      </View>
    );
  };

  const renderFooter = () => {
    if (!isLoading) return null;
    return (
      <View style={styles.footerLoader}>
        <ActivityIndicator size="small" color="#346699" />
      </View>
    );
  };

  return (
    <View style={styles.container}>
      {error && (
        <View style={styles.errorContainer}>
          <Text style={styles.errorText}>{error}</Text>
        </View>
      )}

      <FlatList
        data={payments}
        keyExtractor={(item, index) => `${item.id}-${index}`}
        renderItem={renderPaymentItem}
        contentContainerStyle={styles.listContent}
        showsVerticalScrollIndicator={false}
        refreshing={isRefreshing}
        onRefresh={handleRefresh}
        onEndReached={handleLoadMore}
        onEndReachedThreshold={0.5}
        ListFooterComponent={renderFooter}
        ListEmptyComponent={
          !isLoading ? (
            <View style={styles.emptyContainer}>
              <Text style={styles.emptyText}>Brak historii płatności.</Text>
            </View>
          ) : null
        }
      />
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#F5F7FA',
  },
  listContent: {
    padding: 16,
    paddingBottom: 40,
  },
  card: {
    backgroundColor: '#FFF',
    borderRadius: 12,
    padding: 16,
    marginBottom: 12,
    borderWidth: 1,
    borderColor: '#E1E8EF',
    elevation: 2,
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.05,
    shadowRadius: 4,
  },
  cardHeader: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    marginBottom: 8,
  },
  methodText: {
    fontSize: 14,
    color: '#666',
    fontWeight: '500',
  },
  dateText: {
    fontSize: 12,
    color: '#999',
  },
  cardBody: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
  },
  amountText: {
    fontSize: 18,
    fontWeight: '700',
    color: '#1A2A3A',
  },
  statusBadge: {
    paddingVertical: 4,
    paddingHorizontal: 8,
    borderRadius: 8,
  },
  statusText: {
    color: '#FFF',
    fontSize: 12,
    fontWeight: 'bold',
  },
  footerLoader: {
    paddingVertical: 16,
    alignItems: 'center',
  },
  emptyContainer: {
    padding: 32,
    alignItems: 'center',
  },
  emptyText: {
    color: '#666',
    fontSize: 16,
  },
  errorContainer: {
    backgroundColor: '#FFE5E5',
    padding: 12,
    margin: 16,
    borderRadius: 8,
    borderLeftWidth: 4,
    borderLeftColor: '#FF3B30',
  },
  errorText: {
    color: '#D92D20',
    fontSize: 14,
  },
});

export default PaymentHistoryScreen;
