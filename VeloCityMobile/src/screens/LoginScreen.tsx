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
import { loginSchema } from '../utils/authSchema';
import Logo from '../assets/logo.png';
import { useAuthStore } from '../store/useAuthStore';
import apiService from '../api/apiService';
import LoadingOverlay from '../components/LoadingOverlay';
import { translateApiError } from '../utils/errorTranslator';

type Props = NativeStackScreenProps<RootStackParamList, 'Login'>;

const LoginScreen = ({ navigation }: Props) => {
  const signIn = useAuthStore(state => state.signIn);

  const [formData, setFormData] = useState({
    email: '',
    password: '',
  });

  const [isLoading, setIsLoading] = useState(false);
  const [errors, setErrors] = useState<Record<string, string[]>>({});

  const handleLogin = async () => {
    setErrors({});
    const result = loginSchema.safeParse(formData);

    if (result.success) {
      setIsLoading(true);
      try {
        const response = await apiService.login({
          email: result.data.email,
          password: result.data.password,
        });

        signIn(response.name ?? 'User', response.token ?? '');
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
        <Text style={styles.subtitle}>System Zarządzania Transportem</Text>
      </View>
      <View style={styles.form}>
        {errors.global && errors.global.length > 0 && (
          <View style={styles.globalErrorContainer}>
            <Text style={styles.globalErrorText}>{errors.global[0]}</Text>
          </View>
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

        <TouchableOpacity style={styles.button} onPress={handleLogin}>
          <Text style={styles.buttonText}>Zaloguj się</Text>
        </TouchableOpacity>
      </View>

      <TouchableOpacity onPress={() => navigation.navigate('Register')}>
        <Text style={styles.footerText}>Nie masz konta? Zarejestruj się</Text>
      </TouchableOpacity>
      <LoadingOverlay visible={isLoading} />
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

  inputError: {
    borderColor: '#FF3B30',
  },

  errorText: {
    color: '#FF3B30',
    fontSize: 12,
    marginTop: -12,
    marginLeft: 4,
  },

  globalErrorContainer: {
    backgroundColor: '#FFE5E5',
    padding: 12,
    borderRadius: 8,
    borderLeftWidth: 4,
    borderLeftColor: '#FF3B30',
    marginBottom: 8,
  },

  globalErrorText: {
    color: '#D92D20',
    fontSize: 14,
    fontWeight: '500',
    textAlign: 'center'
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

export default LoginScreen;
