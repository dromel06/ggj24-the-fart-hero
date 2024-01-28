using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeightCount : MonoBehaviour
{
    public Text heightText;
    public Text heightLb;
    public float height;
    private string listOfS;
    // Start is called before the first frame update
    void Start()
    {
        height = 0;
    }

    // Update is called once per frame
    void Update(){
        if(Input.GetKeyDown(KeyCode.UpArrow)){
            addHeight(0.5f);
        }
        if(Input.GetKeyDown(KeyCode.DownArrow)){
            removeHeight(0.5f);
        }

        heightText.text = height.ToString("0.00");
        if (height < 1){
            heightLb.text = "<>";
        }
        else if(height >= 1 && height < 2){
            heightLb.text = "<S>";
        }else if(height >= 2 && height < 3){
            heightLb.text = "<BS>";
        }else if(height >= 3 && height < 6){
            heightLb.text = "<LBS>";
        }
        else{
            listOfS = "";
            for(int i = 0; i < height-6; i+= 2){
                listOfS += "S";
            }
            heightLb.text = "<LB" + listOfS + ">";
        }
    }

    public void addHeight(float points){
        height += points;
    }

    public void removeHeight(float points){
        height -= points;
        if(height < 0){
            height = 0;
        }
    }

    public float getHeight(){
        return height;
    }
}
