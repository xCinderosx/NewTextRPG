using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript_1 : MonoBehaviour
{

   
    Quaternion fixedRotation;

    void Awake()
    {
        fixedRotation = transform.rotation;

    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        transform.rotation = fixedRotation;
        /*float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (Input.GetMouseButton(1))
        {
            
            float xPos = h * range;
            float yPos = v * range;
            transform.Rotate(xPos, yPos, 0);
        }
        */

    }
}
