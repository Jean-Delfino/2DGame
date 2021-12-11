using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//In the stage 3 for setting the speed and power of thrown
public class PowerManager : MonoBehaviour{
    [SerializeField] PowerContainer powerPrefab = default;
    [SerializeField] int qtdBars = default;
    [SerializeField] float maxPowerPerBar = default;
    [SerializeField] float powerAdd = default;
    [SerializeField] Color low = default;
    [SerializeField] Color high = default;
    Gradient gradient;

    int actualBar;  
    int actualStrenght;

    delegate float MyDelegate(List<PowerContainer> p);
    List<MyDelegate> save;

    private void Start() {
        SetGradient();
        save = new List<MyDelegate>();
        save.Add(AddFromPowerContainer);
        save.Add(DeleteFromPowerContainer);

        StartCoroutine(StartThrownCor());
    }   

    //Unity Gradient
    void SetGradient(){
        gradient = new Gradient();
        GradientColorKey[] colorKey;
        GradientAlphaKey[] alphaKey;
        // Populate the color keys at the relative time 0 and 1 (0 and 100%)
        colorKey = new GradientColorKey[2];
        colorKey[0].color = low;
        colorKey[0].time = 0.0f;
        colorKey[1].color = high;
        colorKey[1].time = 1.0f;
        // Populate the alpha  keys at relative time 0 and 1  (0 and 100%)
        alphaKey = new GradientAlphaKey[2];
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = 1.0f;
        alphaKey[1].time = 1.0f;

        gradient.SetKeys(colorKey, alphaKey);
    }
    public IEnumerator StartThrownCor(){
        int actualFunction = -1;

        float linearStrenght;
        float addStr = powerAdd * -1;
        actualStrenght = actualBar = 0;

        List<PowerContainer> powerBar = new List<PowerContainer>();

        while(!Input.GetButton("Fire1")){
            linearStrenght = 0;

            if(actualBar > qtdBars - 1|| actualBar == 0){
                actualFunction++; //It is "binary", but C# is stupid so hash
                actualFunction = actualFunction % save.Count;
                addStr *= -1;
            }

            linearStrenght = save[actualFunction](powerBar);
            while(actualBar > 0 && linearStrenght <= maxPowerPerBar && linearStrenght >= 0){
                linearStrenght += addStr; 
                float porcentage = (float)linearStrenght / maxPowerPerBar;
                powerBar[actualBar - 1].ChangeHeightBar(porcentage);
                if(Input.GetButton("Fire1")){
                    yield return null;
                }
                yield return new WaitForSeconds(0.025f);
            }
            yield return null;
        }

        yield return null;
    }
    private float DeleteFromPowerContainer(List<PowerContainer> p){
        actualBar--;
        DestroyInPosition(p , actualBar);
        return maxPowerPerBar;
    }

    private void DestroyInPosition(List<PowerContainer> p, int index){
        Destroy(p[index].gameObject);
        p.RemoveAt(index);
    }
    
    private void DeleteAll(List<PowerContainer> p){
        int i;
        int count = p.Count;
        for(i = 0 ; i < count ; i++){
            DestroyInPosition(p, p.Count);
        }
    }

    private float AddFromPowerContainer(List<PowerContainer> p){
        float ev = (float) actualBar / (qtdBars - 1);
        print(ev);
        p.Add(Instantiate<PowerContainer>(powerPrefab , this.transform, true));
        p[actualBar].ChangeColor(gradient.Evaluate(ev));          
        actualBar++;
        return 0;
    }

}