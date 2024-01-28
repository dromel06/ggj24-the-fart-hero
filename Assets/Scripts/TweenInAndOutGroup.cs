using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenInAndOutGroup : MonoBehaviour
{
    public enum Transformaciones { Move, MoveLocal}
    
    //public GameObject objeto;
    public Transformaciones transformacion;
    public Vector3 initialPosition;
    public Vector3 target;
    public float modificador;

    [Header("IN Settings")]
    public float animationDurationIN;
    public float delayIN;
    public LeanTweenType introEaseType;

    [Header("OUT Settings")]
    public float animationDurationOUT;
    public float delayOUT;
    public LeanTweenType outroEaseType;
    

    private void OnEnable()
    {
        if (this.transformacion == Transformaciones.Move)
        {
            LeanTween.move(gameObject, new Vector3(transform.position.x, target.y, transform.position.z), animationDurationIN).setEase(introEaseType).setDelay(delayIN).setOnComplete(ContinuarMovOnFail);
        }
        else if (transformacion == Transformaciones.MoveLocal)
        {
            LeanTween.moveLocal(gameObject, new Vector3(transform.position.x, target.y, transform.position.z), animationDurationIN).setEase(introEaseType).setDelay(delayIN);
        }
    }

    public void ContinuarMovOnFail()
    {
        LeanTween.move(gameObject, new Vector3(transform.position.x, -10, transform.position.z), animationDurationOUT).setEase(introEaseType).setDelay(delayIN);
    }

    public void Update()
    {
        if (transform.position.y < -9)
            Destroy(gameObject);
    }
}
