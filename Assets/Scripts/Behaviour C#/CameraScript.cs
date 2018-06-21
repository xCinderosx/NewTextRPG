using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public float range;
    float yPos, xPos;
    Quaternion fixedRotation;
    Vector3 Ethan;

    void Awake()
    {
        


    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            Ethan = transform.GetChild(0).position;
            this.transform.position = Ethan;
            Debug.Log(GetComponentInChildren<Transform>());
            //transform.rotation = fixedRotation;
        }


        if (Input.GetKey(KeyCode.Mouse1))
        {
            
            float HorizontalValue = Input.GetAxis("Mouse X");
            float VerticalSpeed = Input.GetAxis("Mouse Y");
            Debug.Log(HorizontalValue);
            //transform.Rotate(VerticalSpeed, HorizontalValue, 0);
            transform.Rotate(new Vector3(Ethan.x, HorizontalValue, Ethan.z));
        }
        

    }
}
