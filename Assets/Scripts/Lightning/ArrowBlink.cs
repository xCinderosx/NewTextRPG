using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ArrowBlink : MonoBehaviour {

    public List<AuraAPI.AuraVolume> Arrows = new List<AuraAPI.AuraVolume>(3);
    private bool running = false;
    public float BlinkSpeed = 0.2f;
    // Use this for initialization
    void Start () {
        Arrows = GetComponentsInChildren<AuraAPI.AuraVolume>().ToList();
	}
	
	// Update is called once per frame
	void Update () {
        if (!running)
        {
            StartCoroutine(Blink(BlinkSpeed));
        }
	}

    IEnumerator Blink(float blinkTime)
    {
        var timertime = 5f;
        while (timertime > 1)
        {
            running = true;
            for (int i = 0; i < 3; i++)
            {
                Arrows[i].enabled = true;
                Arrows[3].transform.position = Arrows[i].transform.position;
                Arrows[3].transform.rotation = Arrows[i].transform.rotation;
                Arrows[3].enabled = true;

                yield return new WaitForSeconds(blinkTime);

                Arrows[i].enabled = false;
                Arrows[3].enabled = false;

                timertime -= blinkTime;
            }

            yield return new WaitForSeconds(blinkTime);

            timertime -= blinkTime;
        }
        running = false;
    }
}
