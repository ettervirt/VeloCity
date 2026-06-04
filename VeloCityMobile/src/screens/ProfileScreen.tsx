import React from 'react';
import { View, Text, StyleSheet, TouchableOpacity, SafeAreaView } from 'react-native';
import { useAuthStore } from '../store/useAuthStore';
import apiService from '../api/apiService';

export default function ProfileScreen({ navigation }: any) {

    const { isLoggedIn, user, signOut } = useAuthStore();

    const handleLogout = () => {
        apiService.setToken(null);
        signOut();
    };

    // unauth user
    if (!isLoggedIn || !user) {
        return (
            <SafeAreaView style={styles.container}>
                <View style={styles.card}>
                    <Text style={styles.welcomeText}>Witaj w VeloCity!</Text>
                    <Text style={styles.subtitle}>
                        Zaloguj się, aby zyskać możliwość kupowania biletów, sprawdzania historii podróży oraz wygodnego zarządzania kontem.
                    </Text>

                    <TouchableOpacity
                        style={styles.primaryButton}
                        onPress={() => navigation.navigate('Login')}>
                        <Text style={styles.buttonText}>Zaloguj się</Text>
                    </TouchableOpacity>

                    <TouchableOpacity
                        style={styles.secondaryButton}
                        onPress={() => navigation.navigate('Register')}>
                        <Text style={styles.secondaryButtonText}>Załóż darmowe konto</Text>
                    </TouchableOpacity>
                </View>
            </SafeAreaView>
        );
    }

    // auth user
    return (
        <SafeAreaView style={styles.container}>
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
                <Text style={styles.walletBalance}>0.00 PLN</Text>

                <TouchableOpacity
                    style={styles.depositButton}
                    onPress={() => navigation.navigate('WalletTabs', { screen: 'WalletTab' })}
                >
                    <Text style={styles.depositButtonText}>Doładuj konto</Text>
                </TouchableOpacity>
            </View>

            <View style={styles.actionsContainer}>
                <TouchableOpacity style={styles.actionRow} onPress={() => navigation.navigate('MyTickets')}>
                    <Text style={styles.actionText}>Moje Bilety</Text>
                </TouchableOpacity>

                <TouchableOpacity style={styles.actionRow} onPress={() => navigation.navigate('WalletTabs', { screen: 'HistoryTab' })}>
                    <Text style={styles.actionText}>Historia Transakcji</Text>
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
        padding: 20,
        justifyContent: 'center'
    },
    card: {
        backgroundColor: '#FFF',
        padding: 24,
        borderRadius: 16,
        elevation: 4,
        shadowColor: '#000',
        shadowOffset: { width: 0, height: 2 },
        shadowOpacity: 0.1,
        shadowRadius: 8,
        alignItems: 'center'
    },
    welcomeText: {
        fontSize: 24,
        fontWeight: 'bold',
        color: '#1A1C1E',
        marginBottom: 12
    },
    subtitle: {
        fontSize: 14,
        color: '#666',
        textAlign: 'center',
        marginBottom: 24,
        lineHeight: 20
    },
    primaryButton: {
        backgroundColor: '#346699',
        width: '100%',
        padding: 16,
        borderRadius: 12,
        alignItems: 'center',
        marginBottom: 12
    },
    buttonText: {
        color: '#FFF',
        fontSize: 16,
        fontWeight: 'bold'
    },
    secondaryButton: {
        width: '100%',
        padding: 16,
        borderRadius: 12,
        alignItems: 'center',
        borderWidth: 1,
        borderColor: '#346699'
    },
    secondaryButtonText: {
        color: '#346699',
        fontSize: 16,
        fontWeight: 'bold'
    },

    profileHeader: {
        alignItems: 'center',
        marginTop: 40,
        marginBottom: 24
    },
    avatarCircle: {
        width: 80,
        height: 80,
        borderRadius: 40,
        backgroundColor: '#346699',
        justifyContent: 'center',
        alignItems: 'center',
        marginBottom: 12
    },
    avatarLetter: { 
        color: '#FFF', 
        fontSize: 36, 
        fontWeight: 'bold' 
    },
    userName: { 
        fontSize: 22, 
        fontWeight: 'bold', 
        color: '#1A1C1E' 
    },
    userRole: { 
        fontSize: 14, 
        color: '#666', 
        marginTop: 4 
    },
    walletCard: { 
        backgroundColor: '#FFF', 
        padding: 20, 
        borderRadius: 16, 
        alignItems: 'center', 
        marginBottom: 24, 
        elevation: 2 
    },
    walletLabel: { 
        fontSize: 14, 
        color: '#666', 
        marginBottom: 4 
    },
    walletBalance: { 
        fontSize: 32, 
        fontWeight: 'bold', 
        color: '#1A1C1E', 
        marginBottom: 12 
    },
    depositButton: { 
        backgroundColor: '#E1EBF5', 
        paddingVertical: 10, 
        paddingHorizontal: 20, 
        borderRadius: 20 
    },
    depositButtonText: { 
        color: '#346699', 
        fontWeight: '600' 
    },
    actionsContainer: { 
        backgroundColor: '#FFF', 
        borderRadius: 16, 
        overflow: 'hidden', 
        marginBottom: 'auto' 
    },
    actionRow: { 
        padding: 18, 
        borderBottomWidth: 1, 
        borderBottomColor: '#F0F0F0', 
        flexDirection: 'row', 
        justifyContent: 'space-between' 
    },
    actionText: { 
        fontSize: 16, 
        color: '#1A1C1E', 
        fontWeight: '500' 
    },
    logoutButton: { 
        backgroundColor: '#FFE5E5', 
        padding: 16, 
        borderRadius: 12, 
        alignItems: 'center', 
        marginBottom: 20 
    },
    logoutButtonText: { 
        color: '#FF3B30', 
        fontSize: 16, 
        fontWeight: 'bold' 
    }
});