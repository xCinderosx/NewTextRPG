using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject PlayerCamera;
    public Transform CameraNestTransform, CameraNormalPos, CameraAttackPos, CurrentTarget;
    public Collider CameraCollider;
    public Mission1 mission;
    private Ray ray;
    Vector3 Rayhit;
    [SerializeField] private Vector3 OldCameraRotation, OldCameraNestTransformRotation;
    [SerializeField] private List<Transform> TargetsList = new List<Transform>();
    [SerializeField] private float CameraTransitionSpeed;
    [SerializeField] private string CurrentCameraMode;
    [SerializeField] public bool UnderAttack, DebugMode = false, CheckTimerTrigger = false;
    [SerializeField] private bool ReadyToUpdate = true;
    [SerializeField] private float FleeDuration = 0.5f;
    private int oldEnemies;

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
        mission = GameObject.FindGameObjectWithTag("MissionCanvas").GetComponent<Mission1>();
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        CameraTransitionSpeed = 0.2f;
        Rayhit = PlayerCamera.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(0.5f, 0.5f, PlayerCamera.GetComponent<Camera>().nearClipPlane));
    }

    void Update()
    {
        if (CurrentCameraMode == "Normal")
        {
            CameraNestTransform.transform.position = transform.position;
        }

        if (Input.GetKeyDown(KeyCode.F12))
        {
            if (!DebugMode)
            {
                DebugMode = true;
                CameraPosition(9);
            }
            else if (DebugMode)
            {
                DebugMode = false;
            }
        }

        if (Input.GetButton("Fire2") && UnderAttack == false)
        {
            CameraNestTransform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * 5f * Time.deltaTime, 0).normalized, Space.World);
            CameraNestTransform.Rotate(new Vector3(-Input.GetAxis("Mouse Y") * 0.5f * Time.deltaTime, 0, 0).normalized);
        }

        if (TargetsList.Count > 0)
        {

            CurrentTarget = TargetsList[0];
            UnderAttack = true;

            if (TargetsList.Count != oldEnemies)
            {
                StopCoroutine("UpdateCameraPositionTo");
                StopCoroutine("CameraTransitionToNormal");
            }
        }
        else
        {
            StopCoroutine("CameraCombatTransitionTo");
            StopCoroutine("UpdateCameraPositionTo");
        }

        if (CheckTimerTrigger)
        {
            CheckTimer();
        }
    }

    private void LateUpdate()
    {

        if (UnderAttack == false && CurrentCameraMode != "Normal")
        {
            CameraPosition(0);
        }

        if (UnderAttack && CurrentCameraMode == "Normal")
        {
            CameraPosition(1);
        }
        else if (UnderAttack && ReadyToUpdate)
        {
            CameraPosition(2);
        }
    }

    void CheckTimer()
    {
        if (Timer <= Time.time)
        {
            UnderAttack = false;
            CheckTimerTrigger = false;
        }
    }

    void CameraPosition(int CameraMode)
    {
        switch (CameraMode)
        {
            default:
                break;

            case 0:

                StopCoroutine("UpdateCameraPositionTo");
                StopCoroutine("CameraCombatTransitionTo");
                StartCoroutine(CameraTransitionToNormal(CameraTransitionSpeed));
                CurrentTarget = null;

                Debug.Log("NormalCameraMode_ON");
                CurrentCameraMode = "Normal";
                break;

            case 1:
                StopCoroutine("UpdateCameraPositionTo");
                StopCoroutine("CameraCombatTransitionTo");
                StartCoroutine(CameraCombatTransitionTo(CameraTransitionSpeed));
                Debug.Log("FightCameraMode_Switch");
                CurrentCameraMode = "Fight";
                break;

            case 2:
                Vector3 CurrentNestPoint = CalculateEnemiesAveragePosition(TargetsList);
                StartCoroutine(UpdateCameraPositionTo(CurrentNestPoint));
                CurrentCameraMode = "FloatyCamera";
                Debug.Log("FightCameraMode_ON");
                break;

            case 9:
                Debug.Log("DebugFreeCameraMode_ON");
                CurrentCameraMode = "DebugCamera";
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
                oldEnemies++;
            }
        }

        if (other.tag == "WinCondition")
        {
            mission.Win();
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
                oldEnemies--;
            }
        }
    }

    IEnumerator CameraTransitionToNormal(float time)
    {
        float elapsedTime = 0;

        Vector3 StartingCameraPosition = PlayerCamera.transform.position;

        while (elapsedTime < time)
        {
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
            //CameraNestTransform.position = Vector3.Lerp(CameraNestTransform.position, AverageEnemyPosition, elapsedTime / time);

            PlayerCamera.transform.position = Vector3.Lerp(StartingCameraPosition, CameraAttackPos.position, elapsedTime / time);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        ReadyToUpdate = true;
    }

    IEnumerator UpdateCameraPositionTo(Vector3 NewNestPos)
    {

        float elapsedTime = 0;
        var OldNestPos = CameraNestTransform.position;
        while (elapsedTime < 0.1f)
        {
            ReadyToUpdate = false;
            CameraNestTransform.transform.position = Vector3.Lerp(OldNestPos, NewNestPos, elapsedTime / 0.1f);

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
        AveragePoint += Camera.main.transform.position;
        result = AveragePoint / (Targets.Count + 2);

        return result;
    }
}
