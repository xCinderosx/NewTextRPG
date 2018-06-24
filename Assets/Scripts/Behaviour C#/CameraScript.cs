using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject PlayerCamera;
    public Transform CameraNestTransform, CameraNormalPos, CameraAttackPos, CurrentTarget;
    public Collider CameraCollider;
    private Ray ray;
    Vector3 Rayhit;
    [SerializeField] private Vector3 OldCameraRotation, OldCameraNestTransformRotation;
    [SerializeField] private List<Transform> TargetsList = new List<Transform>();
    [SerializeField] private float CameraTransitionSpeed;
    [SerializeField] private string CurrentCameraMode;
    [SerializeField] public bool UnderAttack, DebugMode = false, CheckTimerTrigger = false, ReadyToUpdate = true;

    [SerializeField] private float FleeDuration = 3f;
    float Timer;

    private void OnGUI()
    {

    }

    void Awake()
    {
        CurrentCameraMode = "Normal";
    }
    
    void Start()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        CameraTransitionSpeed = 1.5f;
        Rayhit = PlayerCamera.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(0.5f, 0.5f, PlayerCamera.GetComponent<Camera>().nearClipPlane));
    }
    
    void Update()
    {
        if (!DebugMode && UnderAttack == false)
        {
            CameraNestTransform.transform.position = transform.position;
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

        if (Input.GetButton("Fire2") && UnderAttack == false)
        {
            CameraNestTransform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * 2f * Time.deltaTime, 0).normalized, Space.World);
            CameraNestTransform.Rotate(new Vector3(-Input.GetAxis("Mouse Y") * 0.5f * Time.deltaTime, 0, 0).normalized);
        }
        
        if (TargetsList.Count > 0)
        {
            Vector3 CurrentNestPoint = CalculateEnemiesAveragePosition(TargetsList);
            Debug.Log(CalculateEnemiesAveragePosition(TargetsList));
            if (ReadyToUpdate)
            {
                //UpdateCameraPosition(CurrentNestPoint);
            }
            CurrentTarget = TargetsList[0];
            UnderAttack = true;
        }

        if (CheckTimerTrigger)
        {
            CheckTimer();
        }
    }

    /*void UpdateCameraPosition(Vector3 NewNestPos)
    {
        float elapsedTime = 0;
        var OldNestPos = CameraNestTransform.position;

        while (elapsedTime < 0.7f)
        {
            CameraNestTransform.transform.position = Vector3.Lerp(OldNestPos, NewNestPos, elapsedTime / 0.7f);

            elapsedTime += Time.deltaTime;
        }
        ReadyToUpdate = true;
    } */

    void CheckTimer()
    {
        if (Timer <= Time.time)
        {
            Debug.Log("Checking Timer: " + Timer);
            UnderAttack = false;
            CheckTimerTrigger = false;
        }
    }

    private void LateUpdate()
    {
        if (UnderAttack && CurrentCameraMode != "Fight")
        {
            CameraPosition(1);
        }

        if (UnderAttack == false && CurrentCameraMode != "Normal")
        {
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
                StartCoroutine(CameraTransitionTo(CameraTransitionSpeed));
                CurrentTarget = null;
                Debug.Log("NormalCameraMode_ON");
                CurrentCameraMode = "Normal";
                break;

            case 1:
                StartCoroutine(CameraCombatTransitionTo(CameraTransitionSpeed));
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
            if (TargetsList.Contains(other.transform) == false)
            {
                TargetsList.Add(other.transform);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            TargetsList.Remove(other.transform);

            if (TargetsList.Count == 0)
            {
                Timer = Time.time + FleeDuration;
                CheckTimerTrigger = true;
            }
        }
    }

    IEnumerator CameraTransitionTo(float time)
    {
        float elapsedTime = 0;

        Vector3 StartingCameraPosition = PlayerCamera.transform.position;
        
        while (elapsedTime < time)
        {
            ReadyToUpdate = false;
            PlayerCamera.transform.position = Vector3.Lerp(StartingCameraPosition, CameraNormalPos.position, elapsedTime / time);
            
            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }

    IEnumerator CameraCombatTransitionTo(float time)
    {
        float elapsedTime = 0;
        Vector3 StartingCameraPosition = PlayerCamera.transform.position;
        Vector3 AverageEnemyPosition = CalculateEnemiesAveragePosition(TargetsList);

        while (elapsedTime < time)
        {
            ReadyToUpdate = false;
            CameraNestTransform.position = Vector3.Lerp(CameraNestTransform.position, AverageEnemyPosition, elapsedTime / time);

            PlayerCamera.transform.position = Vector3.Lerp(StartingCameraPosition, CameraAttackPos.position, elapsedTime / time);

            elapsedTime += Time.deltaTime;
            
            yield return null;
        }
        ReadyToUpdate = true;
    }

    Vector3 CalculateEnemiesAveragePosition(List<Transform> Targets)
    {
        Vector3 AveragePoint = Vector3.zero;
        Vector3 result;
        foreach (Transform Target in Targets)
        {
            AveragePoint += Target.position;
        }
        AveragePoint += GetComponentInParent<Transform>().position;
        result = AveragePoint / (Targets.Count + 1);
        return result;
    }
}
