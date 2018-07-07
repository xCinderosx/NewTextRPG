﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;

public class Mission1 : MonoBehaviour {

    float time1, curtime;
    [SerializeField] public int timeleft;
    public Canvas missionTEXT;
    Text timer, countdown;
    public CameraScript CameraTransform;


    private void Awake()
    {
        StartCoroutine("ShowObjective");
    }
    // Use this for initialization
    void Start() {
        missionTEXT.enabled = true;

        time1 = Time.time + 10;
        Time.timeScale = 0;
        timer = GameObject.FindGameObjectWithTag("Timer").GetComponent<Text>();
        countdown = GameObject.FindGameObjectWithTag("TimerCountdown").GetComponent<Text>();
        
        StartCoroutine("ShowTimeLeft");
    }

    // Update is called once per frame
    void Update() {

    }

    IEnumerator ShowObjective()
    {
        var timertime = 15;
        while (timertime > 1)
        {
            Debug.Log(timertime);
            yield return new WaitForSecondsRealtime(1);
            // Decrease the timer
            countdown.text = ((timertime-5).ToString());
            timertime--;
        }
        CameraTransform.StartCoroutine(CameraTransform.CameraTransitionToNormal(3f));
        countdown.enabled = false;
        missionTEXT.enabled = false;
        Time.timeScale = 1;
    }

    IEnumerator ShowTimeLeft()
    {
        timeleft = 180;
        while (timeleft > 0)
        {
            var minutes = timeleft / 60;
            var seconds = timeleft % 60;
            // Draw the resting time

            if (seconds >= 10)
            {
                timer.text = "0" + minutes + ":" + seconds;
            }
            else
            {
                timer.text = "0" + minutes + ":" + "0" + seconds;
            }
            


            yield return new WaitForSeconds(1);

            // Decrease the timer
            timeleft--;
        }

        Fail();
    }
    public void Fail()
    {
        missionTEXT.GetComponentInChildren<Text>().text = "You waste exacly " + (180 - timeleft) + " of my seconds!";
        missionTEXT.enabled = true;

        Time.timeScale = 0;
    }

    public void Win()
    {
        missionTEXT.GetComponentInChildren<Text>().text = "Good job! \nYou made it in " + (180 - timeleft) + " seconds.";
        missionTEXT.enabled = true;

        Time.timeScale = 0;
    }
}