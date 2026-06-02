export const translateApiError = (englishMessage: string): string => {
  if (!englishMessage) 
    return 'Wystąpił nieoczekiwany błąd serwera.';

  const translations: Record<string, string> = {
    'Wrong username or password': 'Nieprawidłowy adres e-mail lub hasło.',
  };

  return translations[englishMessage] || englishMessage;
};
