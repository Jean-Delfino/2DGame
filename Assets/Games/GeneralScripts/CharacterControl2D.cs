using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl2D : MonoBehaviour{
    [SerializeField] float moveSpeed = default;
    [SerializeField] float jumpForce = default;
    [SerializeField] float gravity = default;

    [SerializeField] LayerMask m_LayerMask = default;
    [SerializeField] BoxCollider2D boxGround = default;

    bool lockMovement = false;

    void Start(){}
    void Update(){
        if(lockMovement) return;

        float x = Input.GetAxis("Horizontal");
        float y = 0;

        bool groundCheck = IsGrounded();
        int groundCheckMultiplayer = 1;

        if(groundCheck){    
            groundCheckMultiplayer = 0;
            if(Input.GetButtonDown("Jump")){
                y = Mathf.Sqrt(jumpForce * -2f * gravity);
            }
        }else{
            y = y * jumpForce;
            x /= 2;
        }

        y += gravity * (float) groundCheckMultiplayer * Time.deltaTime;

        Vector3 mov = ((transform.right * x * moveSpeed) + (transform.up * y));
        mov *= Time.deltaTime;

        this.transform.Translate(mov);
    }

    public void ChangeLockMovement(){
        lockMovement = !lockMovement;
    }

    private bool IsGrounded(){
        bool res = Physics.CheckBox(boxGround.transform.position, 
                    boxGround.transform.localScale, Quaternion.identity, m_LayerMask);
        print(res);
        return false;
    }
}
