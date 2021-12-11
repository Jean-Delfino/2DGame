using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System;

public class DisplayProtein : MonoBehaviour{
    [SerializeField] GameObject proteinPrefab;
    [SerializeField] List<Protein> proteinData;
    [SerializeField] Transform content;

    [SerializeField] VideoPlayer vp = default;

    private float widthPrefab;
    private int actualProtein;
    private int limit = 0;
    
    //50 and 55 are fixed values, need to change that
    //It does not make sense to less than 4, and if you do it gonna die from division
    void Start(){
        Setup();
        limit = proteinData.Count - 3;
        widthPrefab = proteinPrefab.GetComponent<RectTransform>().rect.width / 2;

        widthPrefab = (widthPrefab + ( (55)
            * (limit))) / limit; //This count is very strange, but its work
        limit = proteinData.Count  - proteinData.Count % 2; //For the side check, only for odd

        ChangeProteinOnScreen((proteinData.Count / 2)); //Makes the protein in the middle
    }

    void Setup(){
        Protein pt;
        int i;

        for(i = 0; i < proteinData.Count; i++){
            pt = Instantiate<Protein>(proteinData[i] , content);
            pt.Setup(i);
        }
    }
    
    public void ChangeProteinOnScreen(int change){
        actualProtein = change;
        Centralize();
        ChangeVideo();
    }

    private void ChangeVideo(){
        vp.clip = proteinData[actualProtein].ReturnVideo();
    }

    private void Centralize(){
        Vector3 pos = new Vector3(widthPrefab * (actualProtein - (proteinData.Count / 2)), 0, 0);
        content.localPosition = pos;

        if(actualProtein > proteinData.Count - 1) actualProtein = proteinData.Count - 1;
    }

    public void MoveToSides(int side){
        side *= -1;

        if(actualProtein + side < 0 || actualProtein + side > limit){
            return;
        }

        ChangeProteinOnScreen(actualProtein + side);
        print("After " + actualProtein);
    }

}
