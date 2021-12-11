using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GrabMachine : MonoBehaviour{
    Coroutine coroutine = null;
    [SerializeField] GameObject keyToPress = default;
    [SerializeField] Transform positionKey = default;

    private void OnCollisionEnter2D(Collision2D other) {
        Instantiate<GameObject>(keyToPress , positionKey);
        coroutine = StartCoroutine(collisionShow());
    }

    private void OnCollisionExit2D(Collision2D other) {
        StopCoroutine(coroutine);
    }
    //I have to add a listener
    IEnumerator collisionShow(){
        while(!Input.GetButtonDown("Fire1")){
            yield return null;
        }
        yield return null;
    }
}
