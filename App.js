import React, { useEffect } from 'react';
import { StyleSheet, Text, View } from 'react-native';
import UnityView from "@asmadsen/react-native-unity-view";
import { UnityModule } from "@asmadsen/react-native-unity-view";

export default function App() {
  useEffect(() => {
    const checkUnity = async () => {
      const isUnityReady = await UnityModule.isReady();
      console.log('is Unity ready?', isUnityReady);
    };

    checkUnity();
  }, []);

  return (
    <View style={styles.container}>
      <UnityView style={styles.unity} />
      <Text style={styles.welcomeText}>
        Welcome to React Native!
      </Text>
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: 'grey',
    alignItems: 'center',
    justifyContent: 'center',
  },
  unity: {
    position: 'absolute',
    left: 0,
    right: 0,
    top: 0,
    bottom: 0,
  },
  welcomeText: {
    fontSize: 14,
    color: 'green',
  },
});
