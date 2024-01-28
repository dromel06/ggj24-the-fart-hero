using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PisBar : MonoBehaviour
{
    //  create a list of variables
    [SerializeField]
    public float min = 0f;
    [SerializeField]
    public float max = 50f;

    private float life;
    public RectTransform arrow;
    public RectTransform limitBottom;
    public RectTransform limitTop;

    private float minLimit;
    private float maxLimit;
    // Start is called before the first frame update
    void Start()
    {
        minLimit = limitBottom.position.y * min / 100;
        maxLimit = limitTop.position.y * max / 100;
        life = maxLimit / 2;

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