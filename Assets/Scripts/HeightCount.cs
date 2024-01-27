using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeightCount : MonoBehaviour
{
    public Text heightText;
    public Text heightLb;
    public float height;
    // Start is called before the first frame update
    void Start()
    {
        height = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow)){
            addHeight(0.01f);
        }
        if(Input.GetKeyDown(KeyCode.DownArrow)){
            removeHeight(0.01f);
        }

        heightText.text = height.ToString("0.00");
        if(height > 0){
            heightLb.text = "<LBS>";
        }else{
            heightLb.text = "<>";
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
