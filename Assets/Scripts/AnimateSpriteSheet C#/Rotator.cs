using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float speed = 10f;
    public bool Clockwise;
    
    void Update()
    {
        if (Clockwise)
        {

            transform.Rotate(Vector3.up, speed * Time.deltaTime);
        }
        else
        {
            transform.Rotate(-Vector3.up, speed * Time.deltaTime);
        }
    }
}

