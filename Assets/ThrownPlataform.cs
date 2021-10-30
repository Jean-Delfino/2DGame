using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Can be a manager to, but just testing
public class ThrownPlataform : MonoBehaviour{
    // Start is called before the first frame update
    [SerializeField] Transform arrow = default;
    [SerializeField] float rotationSpeed = 20f;

    [SerializeField] float limit = 45f;
    [SerializeField] float initialLauchSpeed = 10f;
    [SerializeField] float maxSpeed = 100f;
    [SerializeField] float minSpeed = 5f;
    [SerializeField] float speedIncreace = 5;
    private float initialRotation;


    void Start(){
        initialRotation = arrow.transform.rotation.eulerAngles.z;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            print("Entered");
            other.GetComponent<CharacterControl2D>().ChangeLockMovement();
            StartCoroutine(CharacterThrown());
            other.GetComponent<CharacterControl2D>().ChangeLockMovement();
        }
    }

    IEnumerator CharacterThrown(){
        float angle;
        float speed = initialLauchSpeed;
        float speedInc = speedIncreace;

        while(!Input.GetButton("Fire1")){ //Setting a angle to shoot the player
            arrow.transform.Rotate(0, 0 ,  rotationSpeed * Time.deltaTime);

            //transform.RotateAround(arrow.transform.position, Vector3.right, rotationSpeed * Time.deltaTime);
            angle = arrow.transform.rotation.eulerAngles.z;
            if(angle < initialRotation - limit ||  angle > initialRotation + limit){
                rotationSpeed *= -1;
                print(angle);
            }
            yield return null;
        }

        print("Out");

        while(!Input.GetButton("Fire1")){
            speed += speedInc;
            if(speed > maxSpeed || speed < minSpeed){
                speedInc *= -1;
            }
        }

        yield return null;
    }
}
