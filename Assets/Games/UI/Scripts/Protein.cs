using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;

public class Protein : MonoBehaviour{
    [SerializeField] Image rend = default;
    [SerializeField] Sprite showCase = default;
    //[SerializeField] Sprite biggerImage = default;
    [SerializeField] string title;
    [SerializeField] string description;
    [SerializeField] int proteinIndex;

    [SerializeField] VideoClip idlePlay;

    public void Setup(int proteinIndex){
        this.proteinIndex = proteinIndex;
        rend.sprite = showCase;
    }

    public void ShowText(){
        FindObjectOfType<TextShow>(true).ShowText(title, description);
    }

    public VideoClip ReturnVideo(){
        return idlePlay;
    }

    public void GetClick(){
        FindObjectOfType<DisplayProtein>().ChangeProteinOnScreen(proteinIndex);
    }

}
