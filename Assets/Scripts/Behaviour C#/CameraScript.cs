using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject camera;
    public Transform CameraNestTransform, CameraNormalPos, CameraAttackPos, CurrentTarget;
    private Ray ray;
    [SerializeField] private Quaternion OldCameraRotation, oldCameraNestTransformRotation;
    RaycastHit hit;
    [SerializeField] private List<Transform> TargetsList;
    [SerializeField] private float CameraTransitionSpeed;
    [SerializeField] private string CurrentCameraMode;
    [SerializeField] public bool UnderAttack = false, DebugMode = false;


    void Awake()
    {
        CurrentCameraMode = "Normal";
    }


    void Start()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        CameraTransitionSpeed = 1.5f;
    }


    void Update()
    {
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
            camera.transform.Rotate(new Vector3(-Input.GetAxis("Mouse Y")*0.5f * Time.deltaTime, 0, 0).normalized);
        }

        if (TargetsList.Count != 0)
        {
            UnderAttack = true;
        }
        
    }

    private void LateUpdate()
    {
        if (UnderAttack && CurrentCameraMode != "Fight")
        {
            //Debug.Log("HI");
            CameraPosition(1);
        }
        
        if (!UnderAttack && CurrentCameraMode != "Normal")
        {
            //Debug.Log("HI2");
            CameraPosition(0);
        }
    }

    void CameraPosition(int CameraMode)
    {
        
        switch (CameraMode)
        {
            default:
                break;
            case 0:
                //Mathf.Clamp(camera.transform.rotation.x, 30f, 75f);
                StartCoroutine(CameraTransitionTo(CameraNormalPos, CameraTransitionSpeed));
                CurrentTarget = null;
                if (OldCameraRotation != null)
                {
                    camera.transform.rotation = OldCameraRotation;
                }
                if (oldCameraNestTransformRotation != null)
                {
                    CameraNestTransform.rotation = oldCameraNestTransformRotation;
                }
                Debug.Log("NormalCameraMode_ON");
                CurrentCameraMode = "Normal";
                break;

            case 1:
                CurrentTarget = TargetsList[0];
                StartCoroutine(CameraTransitionTo(CameraAttackPos, CameraTransitionSpeed));
                StartCoroutine(CameraFaceAt(CurrentTarget, CameraTransitionSpeed)); //WIP
                OldCameraRotation = camera.transform.rotation;
                
                //camera.transform.LookAt(TargetsList[0]);

                Debug.Log("FightCameraMode_ON");
                CurrentCameraMode = "Fight";
                break;

            case 9:
                Debug.Log("DebugFreeCameraMode_ON");
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        /*Debug.Log(other.name + " have entered combat trigger, and his tag is: " + other.tag);*/
        if (other.tag == "Enemy")
        {
            TargetsList.Add(other.transform);
            UnderAttack = true;
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            TargetsList.Remove(other.transform);
            StartCoroutine("Flee");
        }
        
    }
    IEnumerator Flee()
    {
        // suspend execution for 5 seconds
        UnderAttack = false;
        yield return new WaitForSeconds(5);
    }

    IEnumerator CameraTransitionTo(Transform CameraTarget, float time)
    {
        float elapsedTime = 0;

        Vector3 startingPos = camera.transform.position;

        while (elapsedTime < time)

        {

            camera.transform.position = Vector3.Lerp(startingPos, CameraTarget.position, elapsedTime / time);

            elapsedTime += Time.deltaTime;

            yield return null;

        }
    }

    IEnumerator CameraFaceAt(Transform Target, float time)

    {

        float elapsedTime = 0;

        OldCameraRotation = camera.transform.rotation;
        oldCameraNestTransformRotation = CameraNestTransform.rotation;

        while (elapsedTime < time)

        {
            camera.transform.localRotation = new Quaternion(camera.transform.rotation.x, 0, 0, 0);
            CameraNestTransform.localRotation = new Quaternion(0, CameraNestTransform.transform.rotation.y, 0, 0);
            var newRot = Quaternion.LookRotation(Target.position - camera.transform.position);
            camera.transform.rotation = Quaternion.Lerp(OldCameraRotation, newRot, time);
            CameraNestTransform.rotation = Quaternion.Lerp(oldCameraNestTransformRotation, newRot, time);
            elapsedTime += Time.deltaTime;
            /////////////////////////////////////////////////////
            //camera.transform.localEulerAngles = new Vector3(camera.transform.localEulerAngles.x, 0, 0);
            //CameraNestTransform.localEulerAngles = new Vector3(0, CameraNestTransform.localEulerAngles.y, 0);
            

            yield return null;

        }

    }

}
