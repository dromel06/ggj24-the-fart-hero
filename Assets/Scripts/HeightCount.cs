
using UnityEngine;
using UnityEngine.UI;

public class HeightCount : MonoBehaviour
{
    public Text heightText;
    public Text heightLb;
    public float height = 0;
    private string listOfS;

    private void OnEnable()
    {
        PointAwarder.OnScoreChange += SetHeight;
    }

    void Update(){
        
        EvaluateHeight();
        DebugHeight();

    }
    public void EvaluateHeight()
    {
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
            for(int i = 0; i < height-5; i+= 2){
                listOfS += "S";
            }
            heightLb.text = "<LB" + listOfS + ">";
        }
    }
    public void SetHeight(float points) => height = points;
    
    void DebugHeight()
    {
        // if(Input.GetKeyDown(KeyCode.UpArrow)){
        //     addHeight(0.5f);
        // }
        // if(Input.GetKeyDown(KeyCode.DownArrow)){
        //     removeHeight(0.5f);
        // }
    }
    //
    // public void addHeight(float points){
    //     height += points;
    // }
    //
    // public void removeHeight(float points){
    //     height -= points;
    //     if(height < 0){
    //         height = 0;
    //     }
    // }
    //
    // public float getHeight(){
    //     return height;
    // }
}
