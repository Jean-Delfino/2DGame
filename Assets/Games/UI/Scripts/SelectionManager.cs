using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour{
    [SerializeField] float gap = 2;
    [SerializeField] GameObject boltSelection;
    [SerializeField] int actualSelected;
    private Transform childRef;
    int childQTD;

    [SerializeField] Transform partitionsFather;
    GameObject father;

    void OnEnable(){
        StartCoroutine(ChangeBolt());
    }

    void Start(){
        actualSelected = 0;

        childQTD = this.gameObject.transform.childCount;
        father = this.transform.parent.gameObject;

        childRef = this.gameObject.transform.GetChild(actualSelected);
        InstantiateBolt(childRef);
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)){
            ChangePartitionByIndex(actualSelected);
        }
    }

    void PositioningBolt(int increment){
        DestroyBolt(childRef);
        actualSelected += increment;
        childRef = this.gameObject.transform.GetChild(actualSelected);
        InstantiateBolt(childRef);
    }

    public void PositioningBoltByIndex(int index){
        DestroyBolt(childRef);
        actualSelected = index;
        childRef = this.gameObject.transform.GetChild(actualSelected);
        InstantiateBolt(childRef);
    }
    void DestroyBolt(Transform origin){
        int i;

        for (i = origin.childCount - 1; i > -1; i--){
            GameObject.Destroy(origin.GetChild(i).gameObject);
        }
    }

    void InstantiateBolt(Transform origin){
        GameObject boltHold = null;
        float width = origin.GetComponent<RectTransform>().rect.width;

        SpawnBolt(boltHold , origin , new Vector3(-(width + gap), 0 , 0), new Vector3(0 , 0 , 0));
        SpawnBolt(boltHold , origin , new Vector3(width + gap , 0 , 0), new Vector3(0 , 0 , 180));
    }

    void SpawnBolt(GameObject holder , Transform origin , Vector3 pos , Vector3 rotation){
        holder = Instantiate<GameObject>(boltSelection , origin);
        holder.GetComponent<RectTransform>().localPosition += pos;
        holder.GetComponent<RectTransform>().Rotate(rotation , Space.Self);
    }

    IEnumerator ChangeBolt(){
        int inp = 0;

        while(true){
            inp = (int) (Input.GetAxisRaw("Vertical") * -1);

            if(inp != 0){
                if(actualSelected + inp > -1 && actualSelected + inp < childQTD ){
                    PositioningBolt(inp);
                }
                yield return new WaitForSeconds(0.2f);
            }

            yield return null;
        }
    }

    public void ChangePartitionByIndex(int index){
        GameObject hold = partitionsFather.GetChild(index).gameObject;
        hold.SetActive(!hold.activeSelf);

        father.SetActive(!father.activeSelf);
        actualSelected = index; //Just to be sure
    }

    public void Quit(){
        Application.Quit();
    }
}
