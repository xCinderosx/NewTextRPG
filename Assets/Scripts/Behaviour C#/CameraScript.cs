﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject camera;
    public Transform CameraNestTransform;
    private Vector3 CameraNormalPos;
    private Vector3 CameraAttackPos;
    [SerializeField] private string CurrentCameraMode;
    private Ray ray;
    RaycastHit hit;
    public float AttackCameraSpeed;
    public bool UnderAttack = false, DebugMode = false;
    void Awake()
    {
        CameraNormalPos = camera.transform.position;
        CameraAttackPos = camera.transform.position + new Vector3(0, -1.72f, -1.03f);
        CurrentCameraMode = "Normal";
    }

    // Use this for initialization
    void Start()
    {
        //CameraOffset = new Vector3(0.36f, 4.33f, -2.32f);
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        AttackCameraSpeed = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        /*Physics.Raycast(ray, out hit);
        Debug.DrawLine(Camera.main.transform.position, hit.point, Color.red);
        
        camera.transform.LookAt(hit.point);*/
        if (!DebugMode)
        {
            CameraNestTransform.transform.position = this.transform.position;
        }
        
        if (Input.GetKeyDown(KeyCode.F12))
        {
            if (!DebugMode)
            {
                DebugMode = true;
            }
            else if (DebugMode)
            {
                DebugMode = false;
            }
            
        }


        if (Input.GetKey(KeyCode.Mouse1) && !UnderAttack)
        {
            Mathf.Clamp(camera.transform.rotation.x, 30f, 75f);
            //Camera.main.rig
            CameraNestTransform.Rotate(new Vector3(0, Input.GetAxis("Mouse X")*Time.deltaTime, 0).normalized);
            camera.transform.Rotate(new Vector3(-Input.GetAxis("Mouse Y")*Time.deltaTime*50, 0, 0).normalized);
        }

        if (UnderAttack && CurrentCameraMode != "Fight")
        {
            Debug.Log("HI");
            CameraPosition(1);
            CurrentCameraMode = "Fight";
        }

        if (!UnderAttack && CurrentCameraMode != "Normal")
        {
            Debug.Log("HI2");
            CameraPosition(0);
            CurrentCameraMode = "Normal";
        }

    }

    void CameraPosition(int CameraMode)
    {
        switch (CameraMode)
        {
            default:
                break;
            case 0:
                Debug.Log("NormalCameraMode_ON");
                Mathf.Clamp(camera.transform.rotation.x, 30f, 75f);
                camera.transform.position = Vector3.Lerp(camera.transform.position, camera.transform.position - CameraAttackPos, Time.time * AttackCameraSpeed).normalized;
                break;
            case 1:
                Debug.Log("FightCameraMode_ON");
                camera.transform.position = Vector3.Lerp(camera.transform.position, camera.transform.position + CameraAttackPos, Time.time * AttackCameraSpeed).normalized;
                break;
            case 9:
                Debug.Log("DebugFreeCameraMode_ON");
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "enemy")
        {
            UnderAttack = true;
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "enemy")
        {
            StartCoroutine("Flee");
        }
        
    }
    // co jeszcze potrzebuje? ustawic pewnie kolider dla walki 
    IEnumerator Flee()
    {
        // suspend execution for 5 seconds
        yield return new WaitForSeconds(5);
        UnderAttack = false;
    }


}
