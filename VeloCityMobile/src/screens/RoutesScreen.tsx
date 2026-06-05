import React, { useState, useEffect } from 'react';
import {
  View,
  Text,
  StyleSheet,
  TouchableOpacity,
  ScrollView,
  ActivityIndicator,
  FlatList,
  Alert,
} from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';
import { NativeStackNavigationProp } from '@react-navigation/native-stack';
import { RootStackParamList } from '../navigation/types';
import apiService from '../api/apiService';
import { translateApiError } from '../utils/errorTranslator';
import type { Line, Stop, LineDetailsDto } from '../types';

interface RoutesScreenProps {
  navigation: NativeStackNavigationProp<RootStackParamList>;
}

type ScreenState = 'lines' | 'stops' | 'directions';

const RoutesScreen = ({ navigation }: RoutesScreenProps) => {
  const [screenState, setScreenState] = useState<ScreenState>('lines');
  const [lines, setLines] = useState<Line[]>([]);
  const [selectedLine, setSelectedLine] = useState<LineDetailsDto | null>(null);
  const [directions, setDirections] = useState<number[]>([]);
  const [selectedDirection, setSelectedDirection] = useState<number | null>(null);
  const [isLoading, setIsLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    if (screenState === 'lines') {
      fetchLines();
    }
  }, [screenState]);

  const fetchLines = async () => {
    try {
      setError(null);
      setIsLoading(true);
      const response = await apiService.getLines(1, 100);
      setLines(response.items);
    } catch (err: any) {
      setError(translateApiError(err.message));
      console.error('Błąd podczas pobierania linii:', err);
    } finally {
      setIsLoading(false);
    }
  };

  const handleLineSelect = async (lineId: number) => {
    try {
      setError(null);
      setIsLoading(true);
      const lineDetail = await apiService.getLineDetail(lineId);
      setSelectedLine(lineDetail);

      // Wyciągnij unikalne kierunki
      const uniqueDirections = Array.from(
        new Set(lineDetail.Stops.map((stop: Stop) => stop.direction))
      ) as number[];
      setDirections(uniqueDirections.sort((a, b) => a - b));

      setScreenState('directions');
    } catch (err: any) {
      setError(translateApiError(err.message));
      console.error('Błąd podczas pobierania szczegółów linii:', err);
    } finally {
      setIsLoading(false);
    }
  };

  const handleDirectionSelect = (direction: number) => {
    setSelectedDirection(direction);
    setScreenState('stops');
  };

  const handleBuyTicket = () => {
    navigation.navigate('Main', { screen: 'BuyTicketScreen' });
  };

  const handleBack = () => {
    if (screenState === 'directions') {
      setSelectedLine(null);
      setDirections([]);
      setScreenState('lines');
    } else if (screenState === 'stops') {
      setSelectedDirection(null);
      setScreenState('directions');
    }
  };

  // LINES SCREEN
  if (screenState === 'lines') {
    return (
      <SafeAreaView style={styles.container} edges={['top', 'left', 'right']}>
        <ScrollView contentContainerStyle={styles.scrollContent} showsVerticalScrollIndicator={false}>
          <View style={styles.header}>
            <Text style={styles.headerTitle}>Linie transportu</Text>
            <Text style={styles.headerSubtitle}>Wybierz linię, aby zobaczyć przystanki</Text>
          </View>

          {error && (
            <View style={styles.errorContainer}>
              <Text style={styles.errorText}>{error}</Text>
            </View>
          )}

          {isLoading ? (
            <View style={styles.centerContainer}>
              <ActivityIndicator size="large" color="#346699" />
              <Text style={styles.loadingText}>Ładowanie linii...</Text>
            </View>
          ) : lines.length > 0 ? (
            <FlatList
              data={lines}
              renderItem={({ item }) => (
                <TouchableOpacity
                  style={styles.lineCard}
                  onPress={() => handleLineSelect(item.id)}
                >
                  <View style={styles.lineCardContent}>
                    <Text style={styles.lineNumber}>Linia {item.name}</Text>
                    <Text style={styles.lineStatus}>
                      {item.isActive ? '🟢 Aktywna' : '🔴 Nieaktywna'}
                    </Text>
                  </View>
                  <Text style={styles.arrowIcon}>→</Text>
                </TouchableOpacity>
              )}
              keyExtractor={(item) => item.id.toString()}
              scrollEnabled={false}
              contentContainerStyle={styles.listContent}
            />
          ) : (
            <View style={styles.emptyContainer}>
              <Text style={styles.emptyText}>Brak dostępnych linii.</Text>
            </View>
          )}
        </ScrollView>
      </SafeAreaView>
    );
  }

  // DIRECTIONS SCREEN
  if (screenState === 'directions' && selectedLine) {
    return (
      <SafeAreaView style={styles.container} edges={['top', 'left', 'right']}>
        <ScrollView contentContainerStyle={styles.scrollContent} showsVerticalScrollIndicator={false}>
          <TouchableOpacity style={styles.backButton} onPress={handleBack}>
            <Text style={styles.backButtonText}>← Wróć</Text>
          </TouchableOpacity>

          <View style={styles.header}>
            <Text style={styles.headerTitle}>Linia {selectedLine.name}</Text>
            <Text style={styles.headerSubtitle}>Wybierz kierunek</Text>
          </View>

          {isLoading ? (
            <View style={styles.centerContainer}>
              <ActivityIndicator size="large" color="#346699" />
            </View>
          ) : directions.length > 0 ? (
            <FlatList
              data={directions}
              renderItem={({ item: direction }) => (
                <TouchableOpacity
                  style={styles.directionCard}
                  onPress={() => handleDirectionSelect(direction)}
                >
                  <Text style={styles.directionText}>🧭 Kierunek {direction}</Text>
                  <Text style={styles.arrowIcon}>→</Text>
                </TouchableOpacity>
              )}
              keyExtractor={(item) => item.toString()}
              scrollEnabled={false}
              contentContainerStyle={styles.listContent}
            />
          ) : (
            <View style={styles.emptyContainer}>
              <Text style={styles.emptyText}>Brak kierunków dla tej linii.</Text>
            </View>
          )}
        </ScrollView>
      </SafeAreaView>
    );
  }

  // STOPS SCREEN
  if (screenState === 'stops' && selectedLine && selectedDirection !== null) {
    const filteredStops = selectedLine.Stops.filter(
      (stop) => stop.direction === selectedDirection
    );

    return (
      <SafeAreaView style={styles.container} edges={['top', 'left', 'right']}>
        <ScrollView contentContainerStyle={styles.scrollContent} showsVerticalScrollIndicator={false}>
          <TouchableOpacity style={styles.backButton} onPress={handleBack}>
            <Text style={styles.backButtonText}>← Wróć</Text>
          </TouchableOpacity>

          <View style={styles.header}>
            <Text style={styles.headerTitle}>Przystanki</Text>
            <Text style={styles.headerSubtitle}>Linia {selectedLine.name} • Kierunek {selectedDirection}</Text>
          </View>

          {isLoading ? (
            <View style={styles.centerContainer}>
              <ActivityIndicator size="large" color="#346699" />
            </View>
          ) : filteredStops.length > 0 ? (
            <>
              <FlatList
                data={filteredStops}
                renderItem={({ item: stop }) => (
                  <View style={styles.stopCard}>
                    <View style={styles.stopInfo}>
                      <View style={styles.stopSequence}>
                        <Text style={styles.stopSequenceText}>{stop.sequence}</Text>
                      </View>
                      <View style={styles.stopDetails}>
                        <Text style={styles.stopName}>{stop.stopName}</Text>
                        <Text style={styles.stopMeta}>
                          ID: {stop.stopId}
                        </Text>
                      </View>
                    </View>
                  </View>
                )}
                keyExtractor={(item) => item.stopId.toString()}
                scrollEnabled={false}
                contentContainerStyle={styles.listContent}
              />

              <TouchableOpacity style={styles.buyButton} onPress={handleBuyTicket}>
                <Text style={styles.buyButtonText}>🎫 Kup bilet</Text>
              </TouchableOpacity>
            </>
          ) : (
            <View style={styles.emptyContainer}>
              <Text style={styles.emptyText}>Brak przystanków dla tego kierunku.</Text>
            </View>
          )}
        </ScrollView>
      </SafeAreaView>
    );
  }

  return null;
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#F5F7FA',
  },
  scrollContent: {
    flexGrow: 1,
    paddingHorizontal: 16,
    paddingTop: 12,
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
    marginTop: 12,
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
  backButton: {
    paddingVertical: 10,
    paddingHorizontal: 12,
    backgroundColor: '#FFF',
    borderRadius: 8,
    alignSelf: 'flex-start',
    marginBottom: 12,
    borderWidth: 1,
    borderColor: '#E1E8EF',
  },
  backButtonText: {
    color: '#346699',
    fontWeight: '600',
    fontSize: 14,
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
  lineCard: {
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
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
  },
  lineCardContent: {
    flex: 1,
  },
  lineNumber: {
    fontSize: 20,
    fontWeight: 'bold',
    color: '#1A1C1E',
    marginBottom: 6,
  },
  lineStatus: {
    fontSize: 13,
    color: '#666',
    fontWeight: '500',
  },
  arrowIcon: {
    fontSize: 24,
    color: '#346699',
  },
  directionCard: {
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
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
  },
  directionText: {
    fontSize: 18,
    fontWeight: '600',
    color: '#1A1C1E',
  },
  stopCard: {
    backgroundColor: '#FFF',
    borderRadius: 12,
    padding: 16,
    borderWidth: 1,
    borderColor: '#E1E8EF',
    marginBottom: 12,
  },
  stopInfo: {
    flexDirection: 'row',
    gap: 12,
  },
  stopSequence: {
    backgroundColor: '#346699',
    width: 40,
    height: 40,
    borderRadius: 20,
    justifyContent: 'center',
    alignItems: 'center',
  },
  stopSequenceText: {
    color: '#FFF',
    fontWeight: 'bold',
    fontSize: 16,
  },
  stopDetails: {
    flex: 1,
    justifyContent: 'center',
  },
  stopName: {
    fontSize: 16,
    fontWeight: '600',
    color: '#1A1C1E',
    marginBottom: 4,
  },
  stopMeta: {
    fontSize: 12,
    color: '#666',
  },
  buyButton: {
    backgroundColor: '#346699',
    paddingVertical: 16,
    borderRadius: 12,
    alignItems: 'center',
    justifyContent: 'center',
    elevation: 2,
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.1,
    shadowRadius: 4,
    marginTop: 20,
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

export default RoutesScreen;