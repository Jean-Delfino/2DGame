using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerContainer : MonoBehaviour{
    //[SerializeField] RectTransform painel = default;
    [SerializeField] RectTransform bar = default;
    float maxHeight;

    void Start(){
        maxHeight = this.GetComponent<RectTransform>().rect.height;
    }

    public void ChangeColor(Color newColor){
        bar.GetComponent<Image>().color = newColor;
    }

    //mayank1513 in StackOverflow
    public void ChangeHeightBar(float porcentage){
        float height = maxHeight * porcentage;

        bar.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical , height);
        Vector2 shiftDirection = new Vector2(0.5f - bar.anchorMax.x, 0.5f - bar.anchorMax.y);
        bar.anchoredPosition = shiftDirection * bar.rect.size;
    }

    //Makes the rt position the same as the other, just invert if you want other to fit rt
    //IgorAherne (some site i don't remember)
    public void MatchOther(RectTransform rt,  RectTransform other){
        Vector2 myPrevPivot = rt.pivot;
        myPrevPivot = other.pivot;
        rt.position =  other.position;
 
        rt.localScale = other.localScale;
         
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,  other.rect.width);
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,  other.rect.height);
        //rectTransf.ForceUpdateRectTransforms(); - needed before we adjust pivot a second time?
        rt.pivot = myPrevPivot;
    }
}
