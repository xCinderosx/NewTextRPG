using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject camera;
    public Transform CameraNestTransform;
    private Vector3 CameraNormalPos;
    private Vector3 CameraAttackPos;
    string CurrentCameraMode;
    private Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    RaycastHit hit;
    public float AttackCameraSpeed = 0.1f;
    public bool UnderAttack = false;
    void Awake()
    {
        CameraNormalPos = camera.transform.position;
        CameraAttackPos = camera.transform.position + new Vector3(0, -1.72f, -1.03f);

    }

    // Use this for initialization
    void Start()
    {
        //CameraOffset = new Vector3(0.36f, 4.33f, -2.32f);
    }

    // Update is called once per frame
    void Update()
    {
        Physics.Raycast(ray, out hit);
        Debug.DrawLine(Camera.main.transform.position, hit.point, Color.red);
        CameraNestTransform.transform.position = this.transform.position;
        //camera.transform.LookAt(hit.point);

        if (Input.GetKey(KeyCode.Mouse1) && !UnderAttack)
        {
            
            CameraNestTransform.Rotate(new Vector3(0, Input.GetAxis("Mouse X")*Time.deltaTime*10, 0).normalized);
            camera.transform.Rotate(-Input.GetAxis("Mouse Y"), 0, 0);
        }

        if (UnderAttack && CurrentCameraMode == "Normal")
        {
            CameraPosition(1);
            CurrentCameraMode = "Fight";
        }

        if (!UnderAttack && CurrentCameraMode == "Fight")
        {
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
                camera.transform.position = Vector3.Lerp(camera.transform.position, camera.transform.position - CameraAttackPos, Time.time * AttackCameraSpeed);
                break;
            case 1:
                camera.transform.position = Vector3.Lerp(camera.transform.position, camera.transform.position + CameraAttackPos, Time.time * AttackCameraSpeed);
                break;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        UnderAttack = true;
    }
    private void OnTriggerExit(Collider other)
    {
        Flee();
    }
    // co jeszcze potrzebuje? ustawic pewnie kolider dla walki 
    IEnumerator Flee()
    {
        // suspend execution for 5 seconds
        yield return new WaitForSeconds(5);
        UnderAttack = false;
    }


}
