using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PisBar : MonoBehaviour
{
    private float life;
    public RectTransform arrow;
    public RectTransform limitBottom;
    public RectTransform limitTop;
    // Start is called before the first frame update
    void Start()
    {
        life = (limitTop.position.y + limitBottom.position.y) / 2;

    }

    // Update is called once per frame
    void Update()
    {

        arrow.position = new Vector3(arrow.position.x, life, arrow.position.z);
        if(life <= limitBottom.position.y){
            // Game Over
        }
    }

    public float getBarPoint(){
        return life;
    }

    public void addPoints(float points){
        life += points;
        if(life + points > limitTop.position.y){
            life = limitTop.position.y;
            return;
        }
    }

    public void removePoints(float points){
        life -= points;
        if(life - points < limitBottom.position.y){
            life = limitBottom.position.y;
            return;
        }
    }
}