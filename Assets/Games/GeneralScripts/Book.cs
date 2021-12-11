using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Book : MonoBehaviour{
    Image cover;
    Transform spawn;
    PagePrefab pageInst;

    [System.Serializable]
    private class Page{
        string title;
        string content;
        string foot;
        Image secondaryContent;
    }

    [SerializeField] List<Page> pages = default;
    int actualPage = 0;
    int add = 1;
    int limit = 1;
    

    void ChangePage(int increace){
        if((actualPage == pages.Count && increace > 0) || (actualPage == 0 && increace < 0)){
            return;
        }

        if(actualPage == 0 || actualPage + increace <= 0){
            add+= limit;
            limit*=-1;
        }

        int i;
        DeleteAllChildrenInTransform();
        PagePrefab pp;
        for(i = 0 ; i < add && actualPage < pages.Count ; i++){
            pp = Instantiate<PagePrefab>(pageInst, spawn);
            pp.Setup();
            actualPage+=increace;
        }
    }

    void DeleteAllChildrenInTransform(){
        foreach (Transform child in spawn){
            Destroy(child.gameObject);
        }
    }
}
