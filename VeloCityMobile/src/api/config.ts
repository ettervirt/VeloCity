import { Platform } from 'react-native';

const getBaseUrl = (): string => {
  if (__DEV__) {
    // Development
    if (Platform.OS === 'android') {
      return 'http://192.168.50.116:8080';
    } else if (Platform.OS === 'ios') {
      return 'http://localhost:8080';
    }
  }

  return 'https://example.com/api';
};

export const API_BASE_URL = getBaseUrl();
