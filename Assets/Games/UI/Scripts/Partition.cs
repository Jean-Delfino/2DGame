using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Partition : MonoBehaviour{
    protected int index;
    void Start(){
        index = transform.GetSiblingIndex();
    }
    public void Resume(){
        FindObjectOfType<SelectionManager>(true).ChangePartitionByIndex(index);
    }
    
}
