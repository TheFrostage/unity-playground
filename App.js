import React, { useEffect, useRef } from 'react';
import { StyleSheet, Text, View, TouchableOpacity } from 'react-native';
import UnityView from "@asmadsen/react-native-unity-view";
import { UnityModule } from "@asmadsen/react-native-unity-view";


export default function App() {
  const unityRef = useRef(null);

  useEffect(() => {
    const checkUnity = async () => {
      const isUnityReady = await UnityModule.isReady();
      console.log('is Unity ready?', isUnityReady);
    };

    checkUnity();
  }, []);

  const onUnityMessage = (handler) => {
    console.log(handler.name); // the message name
    console.log(handler.data); // the message data
    setTimeout(() => {
      // You can also create a callback to Unity.
      handler.send('I am callback!');
    }, 2000);
  };

  const onPress = () => {
    if (unityRef) {
      console.log('Ref exists');
      UnityModule.postMessage('GameObject/Cube', 'toggleRotate', 'message');
    }
  }

  return (
    <View style={styles.container}>
      <UnityView ref={unityRef} style={styles.unity} onUnityMessage={onUnityMessage} />
      <View style={styles.content}>
        <Text style={styles.welcomeText}>
          Welcome to React Native!
        </Text>
        <TouchableOpacity style={styles.button} onPress={onPress}>
          <Text style={styles.buttonLabel}>Send message to Unity</Text>
        </TouchableOpacity>
      </View>
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
  },
  content: {
    flex: 1,
    justifyContent: 'flex-end',
    alignItems: 'center',
    padding: 50,
  },
  unity: {
    position: 'absolute',
    left: 0,
    right: 0,
    top: 0,
    bottom: 0,
  },
  welcomeText: {
    fontSize: 20,
    color: 'green',
  },
  button: {
    width: 200,
    height: 50,
    borderRadius: 25,
    backgroundColor: 'grey',
    justifyContent: 'center',
    alignItems: 'center',
    marginVertical: 50,
  },
  buttonLabel: {
    color: 'white',
  },
});
