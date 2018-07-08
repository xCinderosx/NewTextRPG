using System.Collections;
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
    public bool brief = true;
    public LanguageChange1 Localization;
    private Text briefing;


    private void Awake()
    {
        StartCoroutine("ShowObjective");
    }
    // Use this for initialization
    void Start() {
        missionTEXT.enabled = true;
        briefing = GameObject.FindGameObjectWithTag("MissionBrief").GetComponent<Text>();
        briefing.text = Localization.CurrentLanguageMENU.LocalizationTexts[12].Replace("\\n", "\n");
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
        brief = false;
        countdown.enabled = false;
        missionTEXT.enabled = false;
        Time.timeScale = 1;
    }

    IEnumerator ShowTimeLeft()
    {
        timeleft = 120;
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
        briefing.text = Localization.CurrentLanguageMENU.LocalizationTexts[15].Replace("\\n", "\n");
        missionTEXT.enabled = true;

        Time.timeScale = 0;
    }

    public void Win()
    {
        var winText = Localization.CurrentLanguageMENU.LocalizationTexts[13].Replace("\\n", "\n") + (120 - timeleft) + Localization.CurrentLanguageMENU.LocalizationTexts[14].Replace("\\n", "\n");
        briefing.text = winText;
        missionTEXT.enabled = true;

        Time.timeScale = 0;
    }
}
