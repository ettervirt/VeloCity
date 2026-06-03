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
import apiService from '../api/apiService';
import LoadingOverlay from '../components/LoadingOverlay';
import { translateApiError } from '../utils/errorTranslator';

type Props = NativeStackScreenProps<RootStackParamList, 'Register'>;
const RegisterScreen = ({ navigation }: Props) => {

  const [formData, setFormData] = useState({
    name: '',
    surname: '',
    email: '',
    password: '',
    password_confirmation: '',
  });

  const [isLoading, setIsLoading] = useState(false);
  const [errors, setErrors] = useState<Record<string, string[]>>({});

  const handleRegister = async () => {
    setErrors({});
    const result = registerSchema.safeParse(formData);
    if (result.success) {
      setIsLoading(true);
      try{
        await apiService.register({
          name: result.data.name,
          surname: result.data.surname,
          email: result.data.email,
          password: result.data.password,
        });
        navigation.navigate('Login');
      } catch (error: any) {
        const polishMessage = translateApiError(error.message);
        setErrors({ global: [polishMessage] });
      } finally {
        setIsLoading(false);
      }
    } else {
      setErrors(result.error.flatten().fieldErrors);
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
                {errors.global && errors.global.length > 0 && (
                  <View style={styles.globalErrorContainer}>
                    <Text style={styles.globalErrorText}>{errors.global[0]}</Text>
                  </View>
                )}
          <TextInput
            style={[styles.input, errors.email && styles.inputError]}
            placeholder="Imię"
            placeholderTextColor="#666"
            value={formData.name}
            onChangeText={text => {
              setFormData({ ...formData, name: text });
              if(errors.name) setErrors({ ...errors, name: [] });
            }}
            autoCapitalize="none"
          />
          {errors.email && errors.email.length > 0 && (
            <Text style={styles.errorText}>{errors.name[0]}</Text>
          )}
          <TextInput
            style={[styles.input, errors.surname && styles.inputError]}
            placeholder="Nazwisko"
            placeholderTextColor="#666"
            value={formData.surname}
            onChangeText={text => {
              setFormData({ ...formData, surname: text });
              if (errors.surname) setErrors({ ...errors, surname: [] });
            }}
            autoCapitalize="none"
          />
          {errors.surname && errors.surname.length > 0 && (
            <Text style={styles.errorText}>{errors.surname[0]}</Text>
          )}
          <TextInput
            style={[styles.input, errors.email && styles.inputError]}
            placeholder="E-mail / Login"
            placeholderTextColor="#666"
            value={formData.email}
            onChangeText={text => {
              setFormData({ ...formData, email: text });
              if (errors.email) setErrors({ ...errors, email: [] });
            }}
            keyboardType="email-address"
            autoCapitalize="none"
          />
          {errors.email && errors.email.length > 0 && (
            <Text style={styles.errorText}>{errors.email[0]}</Text>
          )}
          <TextInput
            style={[styles.input, errors.password && styles.inputError]}
            placeholder="Hasło"
            placeholderTextColor="#666"
            value={formData.password}
            onChangeText={text => {
              setFormData({ ...formData, password: text });
              if (errors.password) setErrors({ ...errors, password: [] });
            }}
            secureTextEntry
          />
          {errors.password && errors.password.length > 0 && (
            <Text style={styles.errorText}>{errors.password[0]}</Text>
          )}
          <TextInput
            style={[styles.input, errors.password_confirmation && styles.inputError]}
            placeholder="Powtórz hasło"
            placeholderTextColor="#666"
            value={formData.password_confirmation}
            onChangeText={text => {
              setFormData({ ...formData, password_confirmation: text });
              if (errors.password_confirmation) setErrors({ ...errors, password_confirmation: [] });
            }}
            secureTextEntry
          />
          {errors.password_confirmation && errors.password_confirmation.length > 0 && (
            <Text style={styles.errorText}>{errors.password_confirmation[0]}</Text>
          )}

          <TouchableOpacity style={styles.button} onPress={handleRegister}>
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

  inputError: {
    borderColor: '#FF3B30',
  },

  errorText: {
    color: '#FF3B30',
    fontSize: 12,
    marginTop: -8,
    marginLeft: 4,
  },

  globalErrorContainer: {
    backgroundColor: '#FFE5E5',
    padding: 12,
    borderRadius: 8,
    borderLeftWidth: 4,
    borderLeftColor: '#FF3B30',
    marginBottom: 4,
  },

  globalErrorText: {
    color: '#D92D20',
    fontSize: 14,
    fontWeight: '500',
    textAlign: 'center',
  },

  footerText: {
    textAlign: 'center',
    color: '#346699',
    marginTop: 24,
    fontWeight: '600',
  },
});

export default RegisterScreen;
