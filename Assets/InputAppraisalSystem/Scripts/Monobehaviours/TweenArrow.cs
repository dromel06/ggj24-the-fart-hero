using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TweenArrow : MonoBehaviour
{
    private bool moveRight;
    [SerializeField] float swapDirectionTime;
    [SerializeField] float moveSpeed = .5f;
    private float currentTime;

    void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= swapDirectionTime)
        {
            currentTime = 0;
            moveRight = !moveRight;
        }

        if (moveRight)
            transform.Translate(Vector3.right * moveSpeed);
        else
            transform.Translate(Vector3.left * moveSpeed);
    }
}