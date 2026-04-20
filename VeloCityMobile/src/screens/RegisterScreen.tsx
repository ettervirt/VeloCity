import React, { useState } from 'react';
import {
  StyleSheet,
  View,
  Text,
  TextInput,
  TouchableOpacity,
  Image,
} from 'react-native';
import { NativeStackScreenProps } from '@react-navigation/native-stack';

import { RootStackParamList } from '../navigation/types';
import { registerSchema } from '../utils/registerSchema';
import Logo from '../assets/logo.png';

type Props = NativeStackScreenProps<RootStackParamList, 'Register'>;
const RegisterScreen = ({ navigation }: Props) => {

  const [formData, setFormData] = useState({
    name: '',
    surname: '',
    email: '',
    password: '',
    password_confirmation: '',
  });

  const handleLogin = () => {
    const result = registerSchema.safeParse(formData);
    if (result.success) {
      console.log('Validation Successful', result.data);
    } else {
      console.log('Validation Error', result.error.flatten());
    }
  };

  return (
      <View style={styles.content}>
        <View style={styles.logoContainer}>
          <Image source={Logo} style={styles.logo} resizeMode="contain" />
          <Text style={styles.brandName}>VeloCity</Text>
          <Text style={styles.subtitle}>Rejestracja</Text>
        </View>
        <View style={styles.form}>
          <TextInput
            style={styles.input}
            placeholder="Imię"
            placeholderTextColor="#666"
            value={formData.name}
            onChangeText={text => setFormData({ ...formData, name: text })}
            autoCapitalize="none"
          />
          <TextInput
            style={styles.input}
            placeholder="Nazwisko"
            placeholderTextColor="#666"
            value={formData.surname}
            onChangeText={text => setFormData({ ...formData, surname: text })}
            autoCapitalize="none"
          />
          <TextInput
            style={styles.input}
            placeholder="E-mail / Login"
            placeholderTextColor="#666"
            value={formData.email}
            onChangeText={text => setFormData({ ...formData, email: text })}
            keyboardType="email-address"
            autoCapitalize="none"
          />
          <TextInput
            style={styles.input}
            placeholder="Hasło"
            placeholderTextColor="#666"
            value={formData.password}
            onChangeText={text => setFormData({ ...formData, password: text })}
            secureTextEntry
          />
          <TextInput
            style={styles.input}
            placeholder="Powtórz hasło"
            placeholderTextColor="#666"
            value={formData.password_confirmation}
            onChangeText={text => setFormData({ ...formData, password_confirmation: text })}
            secureTextEntry
          />

          <TouchableOpacity style={styles.button} onPress={handleLogin}>
            <Text style={styles.buttonText}>Zarejestruj się</Text>
          </TouchableOpacity>
        </View>

        <TouchableOpacity onPress={() => navigation.navigate('Login')}>
          <Text style={styles.footerText}>Masz konto? Zaloguj się</Text>
        </TouchableOpacity>
      </View>
  );
};

const styles = StyleSheet.create({

  content: {
    flex: 1,
    padding: 24,
    justifyContent: 'center',
    backgroundColor: '#F5F7FA',
  },

  logoContainer: {
    alignItems: 'center',
    marginBottom: 48,
  },

  logo: {
    width: 150,
    height: 100,
  },

  brandName: {
    fontSize: 36,
    fontWeight: '900',
    color: '#346699',
    letterSpacing: 1,
  },

  subtitle: {
    fontSize: 14,
    color: '#666',
    marginTop: 4,
  },

  form: {
    gap: 16,
  },

  input: {
    backgroundColor: '#FFF',
    padding: 16,
    borderRadius: 12,
    color: '#666',
    borderWidth: 1,
    borderColor: '#E1E8EF',
    fontSize: 16,
  },

  button: {
    backgroundColor: '#346699',
    padding: 18,
    borderRadius: 12,
    alignItems: 'center',
    marginTop: 8,
    elevation: 3,
  },

  buttonText: {
    color: '#FFF',
    fontSize: 18,
    fontWeight: 'bold',
  },

  footerText: {
    textAlign: 'center',
    color: '#346699',
    marginTop: 24,
    fontWeight: '600',
  },
});

export default RegisterScreen;
