import React from 'react';
import { View, StyleSheet, Modal } from 'react-native';
import LottieView from 'lottie-react-native';

interface Props {
  visible: boolean;
}

const LoadingOverlay = ({ visible }: Props) => {
  return (
    <Modal transparent={true} animationType="fade" visible={visible}>
      <View style={styles.overlay}>
          <LottieView
            source={require('../assets/animations/loading-velo.json')}
            style={styles.animation}
            autoPlay
            loop
            resizeMode="contain"
          />
        </View>
    </Modal>
  );
};

const styles = StyleSheet.create({
  overlay: {
    flex: 1,
    backgroundColor: '#F5F7FA',
    justifyContent: 'center',
    alignItems: 'center',
    
  },

  animation: {
    position: 'absolute',
    left: 0,
    right: 0,
    top: 0,
    bottom: 0,
  },
});

export default LoadingOverlay;
