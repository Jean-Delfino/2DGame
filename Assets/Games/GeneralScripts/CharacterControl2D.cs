using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl2D : MonoBehaviour{
    [SerializeField] float moveSpeed = default;
    [SerializeField] float jumpForce = default;

    bool lockMovement = false;
    void Start(){}
    void Update(){
        if(lockMovement) return;

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 mov = ((transform.right * x * moveSpeed) + (transform.up * y * jumpForce));
        mov *= Time.deltaTime;

        this.transform.Translate(mov);
    }

    public void ChangeLockMovement(){
        lockMovement = !lockMovement;
    }
}
