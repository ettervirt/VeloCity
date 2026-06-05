import React, { useEffect, useState } from 'react';
import { View, Text, StyleSheet, TouchableOpacity, ScrollView, ActivityIndicator, Alert, TextInput } from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';
import { useAuthStore } from '../store/useAuthStore';
import apiService from '../api/apiService';
import { TicketTypeDto } from '../types/ticket';

type TabType = 'tickets' | 'vehicles' | 'lines';

const AdminDashboard = ({ navigation }: any) => {
  const user = useAuthStore((state) => state.user);
  const signOut = useAuthStore((state) => state.signOut);
  const [activeTab, setActiveTab] = useState<TabType>('tickets');
  const [isLoading, setIsLoading] = useState(false);

  const [tickets, setTickets] = useState<TicketTypeDto[]>([]);
  const [vehicles, setVehicles] = useState<any[]>([]);
  const [lines, setLines] = useState<any[]>([]);

  const [newItemName, setNewItemName] = useState('');
  const [newItemPrice, setNewItemPrice] = useState('');
  const [newItemDuration, setNewItemDuration] = useState('');
  const [newItemZone, setNewItemZone] = useState('');

  const [editingTicketId, setEditingTicketId] = useState<number | null>(null);

  const scrollRef = React.useRef<ScrollView>(null);

  const stats = {
    totalTickets: tickets.length,
    totalVehicles: vehicles.length,
    totalLines: lines.length,
  };

  const loadAllData = async () => {
  setIsLoading(true);
  try {
    const [ticketsResponse, vehiclesResponse, linesResponse] = await Promise.all([
      apiService.getTicketTypes().catch(() => []),
      apiService.getVehicles().catch(() => []),
      apiService.getLines().catch(() => []),
    ]);

    const ticketsData = Array.isArray(ticketsResponse) ? ticketsResponse : [];

    const vehiclesData = vehiclesResponse && (vehiclesResponse as any).items 
      ? (vehiclesResponse as any).items 
      : (Array.isArray(vehiclesResponse) ? vehiclesResponse : []);

    const linesData = linesResponse && (linesResponse as any).items 
      ? (linesResponse as any).items 
      : (Array.isArray(linesResponse) ? linesResponse : []);

    setTickets(ticketsData);
    setVehicles(vehiclesData);
    setLines(linesData);
  } catch (error) {
    console.error('Błąd odświeżania danych:', error);
  } finally {
    setIsLoading(false);
  }
};

  useEffect(() => {
    loadAllData();
  }, []);

  const handleAddItem = async () => {
    if (!newItemName.trim()) return Alert.alert('Błąd', 'Wpisz nazwę');

    try {
      if (activeTab === 'tickets') {
        const ticketPayload = {
          name: newItemName,
          price: Number(newItemPrice) || 0,
          durationInMinutes: Number(newItemDuration) || 0,
          zoneLimit: Number(newItemZone) || 0,
        };
        await apiService.createTicket(ticketPayload);
      } else if (activeTab === 'vehicles') {
        await apiService.createVehicle({ vehicleId: Number(newItemName) || 0 });
      } else if (activeTab === 'lines') {
        await apiService.createLine({ name: newItemName });
      }
      
      setNewItemName('');
      setNewItemPrice('');
      setNewItemDuration('');
      setNewItemZone('');
      Alert.alert('Sukces', 'Dodano pomyślnie!');
      loadAllData();
    } catch (err) {
      Alert.alert('Błąd', 'Nie udało się dodać elementu.');
    }
  };

  const handleUpdateItem = async () => {
  if (editingTicketId === null) return;
  if (!newItemName.trim()) return Alert.alert('Błąd', 'Wpisz nazwę');

  try {
    const updatedPayload = {
      name: newItemName,
      price: Number(newItemPrice) || 0,
      durationInMinutes: Number(newItemDuration) || 0,
      zoneLimit: Number(newItemZone) || 0,
    };

    await apiService.updateTicket(editingTicketId, updatedPayload);

    setEditingTicketId(null);
    setNewItemName('');
    setNewItemPrice('');
    setNewItemDuration('');
    setNewItemZone('');
    
    Alert.alert('Sukces', 'Bilet został zaktualizowany!');
    loadAllData();
  } catch (err) {
    Alert.alert('Błąd', 'Nie udało się zaktualizować biletu.');
  }
};

const startEditTicket = (t: TicketTypeDto) => {
    setEditingTicketId(t.id);
    setNewItemName(t.name);
    setNewItemPrice(t.price ? t.price.toString() : '');
    setNewItemDuration(t.durationInMinutes ? t.durationInMinutes.toString() : '');
    setNewItemZone(t.zoneLimit ? t.zoneLimit.toString() : '');

    scrollRef.current?.scrollTo({y:0, animated: true});
  };

  const handleDeleteItem = (id: number) => {
    Alert.alert('Potwierdzenie', 'Czy na pewno chcesz to usunąć?', [
      { text: 'Anuluj' },
      {
        text: 'Usuń',
        style: 'destructive',
        onPress: async () => {
          try {
            if (activeTab === 'tickets') await apiService.deleteTicket(id);
            if (activeTab === 'vehicles') await apiService.deleteVehicle(id);
            if (activeTab === 'lines') await apiService.deleteLine(id);
            
            Alert.alert('Sukces', 'Usunięto zasób.');
            loadAllData();
          } catch (err) {
            Alert.alert('Błąd', 'Nie udało się usunąć.');
          }
        },
      },
    ]);
  };

  return (
    <SafeAreaView style={styles.container} edges={['top', 'left', 'right']}>
      <View style={styles.header}>
        <View>
          <Text style={styles.adminBadge}>PANEL ADMINISTRATORA 🛠️</Text>
          <Text style={styles.greetingText}>Cześć, {user?.name || 'Admin'}!</Text>
        </View>
        <TouchableOpacity style={styles.logoutButton} onPress={() => signOut()}>
          <Text style={styles.logoutText}>Wyloguj</Text>
        </TouchableOpacity>
      </View>

      <ScrollView ref={scrollRef} contentContainerStyle={styles.scrollContent}>
        
        <Text style={styles.sectionTitle}>Statystyki Systemu (Live)</Text>
        <View style={styles.statsGrid}>
          <View style={styles.statsCard}>
            <Text style={styles.statsValue}>{stats.totalTickets}</Text>
            <Text style={styles.statsLabel}>Typów biletów</Text>
          </View>
          <View style={styles.statsCard}>
            <Text style={styles.statsValue}>{stats.totalVehicles}</Text>
            <Text style={styles.statsLabel}>Zarejestrowanych aut</Text>
          </View>
          <View style={styles.statsCard}>
            <Text style={styles.statsValue}>{stats.totalLines}</Text>
            <Text style={styles.statsLabel}>Aktywnych linii</Text>
          </View>
        </View>

        <View style={styles.tabContainer}>
          {(['tickets', 'vehicles', 'lines'] as TabType[]).map((tab) => (
            <TouchableOpacity
              key={tab}
              style={[styles.tabButton, activeTab === tab && styles.activeTabButton]}
              onPress={() => setActiveTab(tab)}>
              <Text style={[styles.tabButtonText, activeTab === tab && styles.activeTabButtonText]}>
                {tab === 'tickets' 
                ? '🎫 Bilety' 
                : tab === 'vehicles' 
                ? '🚌 Pojazdy' 
                : '🗺️ Linie'}
              </Text>
            </TouchableOpacity>
          ))}
        </View>

        <View style={styles.formCard}>
          <Text style={styles.formTitle}>
            {activeTab === 'tickets' && '🎫 Tworzenie nowego typu biletu'}
            {activeTab === 'vehicles' && '🚌 Rejestracja nowego pojazdu'}
            {activeTab === 'lines' && '🗺️ Tworzenie nowej linii komunikacyjnej'}
          </Text>

          {activeTab === 'tickets' && (
            <>
              <TextInput
                style={styles.input}
                placeholder="Nazwa biletu (np. Jednorazowy ulgowy)"
                placeholderTextColor="#6C7A92"
                value={newItemName}
                onChangeText={setNewItemName}
              />
              <TextInput
                style={styles.input}
                placeholder="Cena (np. 4.50)"
                placeholderTextColor="#6C7A92"
                keyboardType="numeric"
                value={newItemPrice}
                onChangeText={setNewItemPrice}
              />
              <TextInput
                style={styles.input}
                placeholder="Czas ważności (w minutach)"
                placeholderTextColor="#6C7A92"
                keyboardType="numeric"
                value={newItemDuration}
                onChangeText={setNewItemDuration}
              />
              <TextInput
                style={styles.input}
                placeholder="Limit strefy (np. 1 lub 2)"
                placeholderTextColor="#6C7A92"
                keyboardType="numeric"
                value={newItemZone}
                onChangeText={setNewItemZone}
              />
            </>
          )}

          {activeTab === 'vehicles' && (
            <>
              <TextInput
                style={styles.input}
                placeholder="Numer ID pojazdu / Numer boczny (np. 104)"
                placeholderTextColor="#6C7A92"
                keyboardType="numeric"
                value={newItemName}
                onChangeText={setNewItemName}
              />
            </>
          )}

          {activeTab === 'lines' && (
            <>
              <TextInput
                style={styles.input}
                placeholder="Nazwa linii (np. Linia 15)"
                placeholderTextColor="#6C7A92"
                value={newItemName}
                onChangeText={setNewItemName}
              />
              <TextInput
                style={styles.input}
                placeholder="Przystanki początkowy i końcowy (np. Krowodrza - Podwawelskie)"
                placeholderTextColor="#6C7A92"
                value={newItemPrice} 
                onChangeText={setNewItemPrice}
              />
            </>
          )}

          <TouchableOpacity style={styles.addButton} onPress={() => handleAddItem()}>
            <Text style={styles.addButtonText}>➕ Zatwierdź i zapisz w bazie</Text>
          </TouchableOpacity>
        </View>

        {editingTicketId !== null && (
          <TouchableOpacity 
            style={[styles.addButton, { backgroundColor: '#7F8C8D', marginTop: 8 }]} 
            onPress={() => {
              setEditingTicketId(null);
              setNewItemName('');
              setNewItemPrice('');
              setNewItemDuration('');
              setNewItemZone('');
            }}
          >
    <Text style={styles.addButtonText}>❌ Anuluj edycję</Text>
  </TouchableOpacity>
)}

        <Text style={styles.sectionTitle}>Zarejestrowane zasoby</Text>
        
        {isLoading ? (
          <ActivityIndicator size="large" color="#E74C3C" />
        ) : (
          <View style={styles.listContainer}>
            {activeTab === 'tickets' && Array.isArray(tickets) && tickets.map((t, index) => (
              <View key={t.id ?? `ticket-${index}`} style={styles.listItem}>
                <View style={{ flex: 1, paddingRight: 12 }}>
                  
                  <Text style={styles.itemMainText}>
                    {t.name || `Bilet bez nazwy (#${t.id})`}
                  </Text>
                  
                  <Text style={[styles.itemSubText, { color: '#2ECC71', fontWeight: '600', marginTop: 4 }]}>
                    💰 Cena: {typeof t.price === 'number' ? t.price.toFixed(2) : '0.00'} PLN
                  </Text>
                  
                  <View style={{ flexDirection: 'row', flexWrap: 'wrap', gap: 10, marginTop: 6 }}>
                    <View style={styles.detailBadge}>
                      <Text style={styles.detailBadgeText}>
                        ⏳ Czas: {t.durationInMinutes} min
                      </Text>
                    </View>
                    
                    <View style={styles.detailBadge}>
                      <Text style={styles.detailBadgeText}>
                        🌐 Strefa: {t.zoneLimit}
                      </Text>
                    </View>

                    <View style={[styles.detailBadge, { backgroundColor: '#2C3E50' }]}>
                      <Text style={[styles.detailBadgeText, { color: '#BDC3C7' }]}>
                        🆔 ID: {t.id}
                      </Text>
                    </View>
                  </View>

                </View>
                
                <TouchableOpacity style={[styles.deleteButton, { backgroundColor: '#2980B9' }]} onPress={() => startEditTicket(t)}>
                  <Text style={styles.deleteButtonText}>Edytuj</Text>
                </TouchableOpacity>

                <TouchableOpacity style={styles.deleteButton} onPress={() => handleDeleteItem(t.id)}>
                  <Text style={styles.deleteButtonText}>Usuń</Text>
                </TouchableOpacity>
              </View>
            ))}

            {activeTab === 'vehicles' && Array.isArray(vehicles) && vehicles.map((v) => (
              <View key={v.id || v.vehicleId} style={styles.listItem}>
                <View>
                  <Text style={styles.itemMainText}>Pojazd #{v.vehicleId || v.id}</Text>
                  <Text style={styles.itemSubText}>{v.status || 'W trasie'}</Text>
                </View>
                <TouchableOpacity style={styles.deleteButton} onPress={() => handleDeleteItem(v.id || v.vehicleId)}>
                  <Text style={styles.deleteButtonText}>Usuń</Text>
                </TouchableOpacity>
              </View>
            ))}

            {activeTab === 'lines' && Array.isArray(lines) && lines.map((l) => (
              <View key={l.id} style={styles.listItem}>
                <View>
                  <Text style={styles.itemMainText}>Linia: {l.name || l.lineName}</Text>
                  <Text style={styles.itemSubText}>Liczba przystanków: {l.stops?.length || 0}</Text>
                </View>
                <TouchableOpacity style={styles.deleteButton} onPress={() => handleDeleteItem(l.id)}>
                  <Text style={styles.deleteButtonText}>Usuń</Text>
                </TouchableOpacity>
              </View>
            ))}
          </View>
        )}

      </ScrollView>
    </SafeAreaView>
  );
};

const styles = StyleSheet.create({
  container: { flex: 1, backgroundColor: '#111318' },
  scrollContent: { paddingHorizontal: 20, paddingBottom: 40 },
  sectionTitle: { fontSize: 14, fontWeight: 'bold', color: '#8A94A6', marginTop: 26, marginBottom: 12, textTransform: 'uppercase', letterSpacing: 0.5 },
  header: { flexDirection: 'row', justifyContent: 'space-between', alignItems: 'center', paddingHorizontal: 20, paddingVertical: 20, borderBottomWidth: 1, borderColor: '#1F242F' },
  adminBadge: { fontSize: 10, fontWeight: 'bold', color: '#E74C3C', letterSpacing: 1 },
  greetingText: { fontSize: 22, fontWeight: 'bold', color: '#FFFFFF', marginTop: 4 },
  logoutButton: { backgroundColor: '#1F242F', paddingVertical: 8, paddingHorizontal: 14, borderRadius: 8, borderWidth: 1, borderColor: '#252B36' },
  logoutText: { color: '#BDC3C7', fontSize: 12, fontWeight: '600' },
  statsGrid: { flexDirection: 'row', justifyContent: 'space-between', marginBottom: 10 },
  statsCard: { backgroundColor: '#1C1F26', width: '31%', borderRadius: 12, padding: 14, alignItems: 'center', borderWidth: 1, borderColor: '#252B36', elevation: 2, shadowColor: '#000', shadowOffset: { width: 0, height: 2 }, shadowOpacity: 0.2, shadowRadius: 3 },
  statsValue: { fontSize: 20, fontWeight: 'bold', color: '#FFFFFF' },
  statsLabel: { fontSize: 10, color: '#6C7A92', marginTop: 6, textAlign: 'center', fontWeight: '500' },
  tabContainer: { flexDirection: 'row', backgroundColor: '#1C1F26', borderRadius: 12, padding: 4, marginTop: 15, borderWidth: 1, borderColor: '#252B36' },
  tabButton: { flex: 1, paddingVertical: 12, alignItems: 'center', borderRadius: 9 },
  activeTabButton: { backgroundColor: '#E74C3C' },
  tabButtonText: { color: '#6C7A92', fontWeight: 'bold', fontSize: 13 },
  activeTabButtonText: { color: '#FFFFFF' },
  formCard: { backgroundColor: '#1C1F26', borderRadius: 14, padding: 18, marginTop: 18, borderWidth: 1, borderColor: '#252B36' },
  formTitle: { color: '#FFFFFF', fontSize: 14, fontWeight: 'bold', marginBottom: 14 },
  input: { backgroundColor: '#111318', color: '#FFFFFF', padding: 14, borderRadius: 8, marginBottom: 12, borderWidth: 1, borderColor: '#252B36', fontSize: 14 },
  addButton: { backgroundColor: '#2E7D32', padding: 14, borderRadius: 8, alignItems: 'center', marginTop: 4 },
  addButtonText: { color: '#FFFFFF', fontWeight: 'bold', fontSize: 15 },
  detailBadge: { backgroundColor: '#1F242F', paddingVertical: 4, paddingHorizontal: 8, borderRadius: 6, borderWidth: 1, borderColor: '#252B36' },
  detailBadgeText: { color: '#8A94A6', fontSize: 11, fontWeight: '500' },
  listContainer: { gap: 10 },
  listItem: { flexDirection: 'row', justifyContent: 'space-between', alignItems: 'center', backgroundColor: '#1C1F26', padding: 16, borderRadius: 12, borderWidth: 1, borderColor: '#252B36' },
  itemMainText: { color: '#FFFFFF', fontSize: 15, fontWeight: 'bold' },
  itemSubText: { color: '#6C7A92', fontSize: 12, marginTop: 4 },
  deleteButton: { backgroundColor: '#C0392B', paddingVertical: 8, paddingHorizontal: 14, borderRadius: 7 },
  deleteButtonText: { color: '#FFFFFF', fontSize: 12, fontWeight: 'bold' },
});

export default AdminDashboard;