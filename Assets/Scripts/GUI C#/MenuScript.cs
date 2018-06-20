using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class MenuScript : MonoBehaviour {

    public Button button;

    public Canvas canv1;
    public Canvas canv2;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void NewGame()
    {
        SceneManager.LoadScene("Scene One");
    }

    public void BackFromOptions()
    {
        canv1.enabled = false;
        canv2.enabled = true;
        Debug.Log("Button Pressed");
    }

    public void EnableOptionsCanvas()
    {
        canv1.enabled = true;
        canv2.enabled = false;
        Debug.Log("Button Pressed");
    }

    public void exit()
    {
        Application.Quit();
    }
}



