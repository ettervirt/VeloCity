import React, { useState } from 'react';
import { View, Text, StyleSheet, SafeAreaView, ScrollView, TouchableOpacity } from 'react-native';
import { useAuthStore } from '../store/useAuthStore';

const DriverDashboard = ({ navigation }: any) => {
  const { user } = useAuthStore();
  const [isDriving, setIsDriving] = useState(false);

  // Funkcja przełączająca stan kursu
  const handleToggleRoute = () => {
    setIsDriving(!isDriving);
  };

  return (
    <SafeAreaView style={styles.container}>
      <ScrollView contentContainerStyle={styles.padding} showsVerticalScrollIndicator={false}>
        
        {/* --- NAGŁÓWEK --- */}
        <View style={styles.header}>
          <Text style={styles.roleBadge}>KIEROWCA 🚌</Text>
          <Text style={styles.welcomeText}>Cześć, {user?.name || 'Kierowco'}</Text>
        </View>

        {/* --- STATUS ZMIANY --- */}
        <View style={styles.statusBanner}>
          <View style={[styles.statusDot, { backgroundColor: isDriving ? '#2ECC71' : '#F1C40F' }]} />
          <Text style={styles.statusText}>
            Status: {isDriving ? 'W TRASIE (KURS AKTYWNY)' : 'POSTÓJ / OCZEKIWANIE'}
          </Text>
        </View>

        {/* --- KARTA BIEŻĄCEGO KORSU --- */}
        <View style={styles.routeCard}>
          <Text style={styles.cardLabel}>PRZYPISANY KURS</Text>
          <Text style={styles.lineText}>Linia 502</Text>
          
          <View style={styles.routeRow}>
            <Text style={styles.stationText}>Centrum</Text>
            <Text style={styles.arrowText}>➔</Text>
            <Text style={styles.stationText}>Plac Centralny</Text>
          </View>

          <View style={styles.divider} />

          <View style={styles.infoRow}>
            <Text style={styles.infoLabel}>Planowany odjazd:</Text>
            <Text style={styles.infoValue}>16:45</Text>
          </View>
          <View style={styles.infoRow}>
            <Text style={styles.infoLabel}>Pojazd:</Text>
            <Text style={styles.infoValue}>Autobus Solaris #1042</Text>
          </View>
        </View>

        {/* --- GŁÓWNY PRZYCISK AKCJI --- */}
        <TouchableOpacity 
          style={[styles.actionButton, isDriving ? styles.buttonStop : styles.buttonStart]} 
          onPress={handleToggleRoute}
        >
          <Text style={styles.buttonText}>
            {isDriving ? '🛑 Zakończ bieżący kurs' : '▶ Rozpocznij kurs'}
          </Text>
        </TouchableOpacity>

        {/* --- SZYBKIE INFORMACJE O ZMIANIE --- */}
        <View style={styles.shiftSummary}>
          <Text style={styles.summaryTitle}>Podsumowanie dzisiejszej zmiany</Text>
          <View style={styles.summaryRow}>
            <Text style={styles.summaryLabel}>Czas pracy:</Text>
            <Text style={styles.summaryValue}>3h 45m / 8h</Text>
          </View>
          <View style={styles.summaryRow}>
            <Text style={styles.summaryLabel}>Zrealizowane kursy:</Text>
            <Text style={styles.summaryValue}>4 / 9</Text>
          </View>
        </View>

      </ScrollView>
    </SafeAreaView>
  );
};

const styles = StyleSheet.create({
  container: { 
    flex: 1, 
    backgroundColor: '#F5F7FA' 
  },
  padding: { 
    padding: 20 
  },
  header: { 
    marginBottom: 16, 
    marginTop: 10 
  },
  roleBadge: { 
    color: '#E67E22', 
    fontWeight: 'bold', 
    fontSize: 12, 
    letterSpacing: 1 
  },
  welcomeText: { 
    fontSize: 26, 
    fontWeight: 'bold', 
    color: '#1A1C1E',
    marginTop: 4
  },
  statusBanner: {
    backgroundColor: '#FFF',
    borderRadius: 10,
    padding: 12,
    flexDirection: 'row',
    alignItems: 'center',
    marginBottom: 20,
    borderWidth: 1,
    borderColor: '#E0E0E0',
  },
  statusDot: {
    width: 10,
    height: 10,
    borderRadius: 5,
    marginRight: 10,
  },
  statusText: {
    fontSize: 13,
    fontWeight: 'bold',
    color: '#1A1C1E',
  },
  routeCard: {
    backgroundColor: '#FFF',
    borderRadius: 16,
    padding: 20,
    elevation: 3,
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.05,
    shadowRadius: 6,
    marginBottom: 24,
  },
  cardLabel: {
    fontSize: 11,
    color: '#8E9297',
    fontWeight: 'bold',
    letterSpacing: 0.5,
  },
  lineText: {
    fontSize: 32,
    fontWeight: '900',
    color: '#346699',
    marginTop: 4,
  },
  routeRow: {
    flexDirection: 'row',
    alignItems: 'center',
    marginTop: 10,
    marginBottom: 16,
  },
  stationText: {
    fontSize: 16,
    fontWeight: '600',
    color: '#1A1C1E',
  },
  arrowText: {
    marginHorizontal: 10,
    color: '#8E9297',
    fontSize: 16,
  },
  divider: {
    height: 1,
    backgroundColor: '#EEF0F2',
    marginBottom: 16,
  },
  infoRow: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    marginBottom: 8,
  },
  infoLabel: {
    color: '#666',
    fontSize: 14,
  },
  infoValue: {
    fontWeight: '600',
    color: '#1A1C1E',
    fontSize: 14,
  },
  actionButton: {
    width: '100%',
    padding: 18,
    borderRadius: 14,
    alignItems: 'center',
    justifyContent: 'center',
    elevation: 2,
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.1,
    shadowRadius: 4,
    marginBottom: 24,
  },
  buttonStart: {
    backgroundColor: '#2ECC71',
  },
  buttonStop: {
    backgroundColor: '#E74C3C',
  },
  buttonText: {
    color: '#FFF',
    fontSize: 18,
    fontWeight: 'bold',
  },
  shiftSummary: {
    backgroundColor: '#EAEDF1',
    borderRadius: 12,
    padding: 16,
  },
  summaryTitle: {
    fontSize: 14,
    fontWeight: 'bold',
    color: '#1A1C1E',
    marginBottom: 12,
  },
  summaryRow: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    marginBottom: 6,
  },
  summaryLabel: {
    fontSize: 13,
    color: '#555',
  },
  summaryValue: {
    fontSize: 13,
    fontWeight: 'bold',
    color: '#1A1C1E',
  },
});

export default DriverDashboard;