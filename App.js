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
    console.log('HANDLER!');
    console.log(1, handler);
    // console.log(handler.name); // the message name
    // console.log(handler.data); // the message data
    // setTimeout(() => {
    //   // You can also create a callback to Unity.
    //   handler.send('I am callback!');
    // }, 2000);
  }

  const onMessage2 = (string) => {
    console.log('ass2', string);
  }

  const onPressLeft = async () => {
    if (unityRef) {
      console.log('Ref exists');
      await UnityModule.postMessageToUnityManager('left');
    }
  }

  const onPressRight = async () => {
    if (unityRef) {
      console.log('Ref exists');
      await UnityModule.postMessageToUnityManager('right');
    }
  }

  const onPressUp = async () => {
    if (unityRef) {
      console.log('Ref exists');
      await UnityModule.postMessageToUnityManager('up');
    }
  }

  const onPressForward = async () => {
    if (unityRef) {
      console.log('Ref exists');
      await UnityModule.postMessageToUnityManager('forward');
    }
  }
 // 123/
  return (
    <View style={styles.container}>
      <UnityView ref={unityRef} style={styles.unity} onMessage={onMessage2} />
      {/*<View style={styles.content}>*/}
      {/*  <Text style={styles.welcomeText}>*/}
      {/*    Welcome to React Native!*/}
      {/*  </Text>*/}
      {/*  <View style={{ flexDirection: 'row'}}>*/}
      {/*    <TouchableOpacity style={styles.button} onPress={onPressLeft}>*/}
      {/*      <Text style={styles.buttonLabel}>Left</Text>*/}
      {/*    </TouchableOpacity>*/}
      {/*    <TouchableOpacity style={styles.button} onPress={onPressRight}>*/}
      {/*      <Text style={styles.buttonLabel}>right</Text>*/}
      {/*    </TouchableOpacity>*/}
      {/*    <TouchableOpacity style={styles.button} onPress={onPressUp}>*/}
      {/*      <Text style={styles.buttonLabel}>up</Text>*/}
      {/*    </TouchableOpacity>*/}
      {/*    <TouchableOpacity style={styles.button} onPress={onPressForward}>*/}
      {/*      <Text style={styles.buttonLabel}>forward</Text>*/}
      {/*    </TouchableOpacity>*/}
      {/*  </View>*/}
      {/*</View>*/}
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
    width: 80,
    height: 50,
    marginHorizontal: 5,
    borderRadius: 25,
    backgroundColor: 'grey',
    justifyContent: 'center',
    alignItems: 'center',
  },
  buttonLabel: {
    color: 'white',
  },
});
