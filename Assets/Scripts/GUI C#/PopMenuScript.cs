using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PopMenuScript : MonoBehaviour {

    public Canvas popMenuCanvas;
    public LanguagesTexts CurrentLanguage;
    public List<Text> textsList;
    public Serializer serializer;

    // Use this for initialization
    void Start () {
        popMenuCanvas.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Pause) || Input.GetKeyDown(KeyCode.Escape))
        {
            PopMenu();
        }
	}

    public void PopMenu()
    {
        if (popMenuCanvas.enabled == true)
        {
            popMenuCanvas.enabled = false;
            Time.timeScale = 1;
            serializer.CloseLoad();
        }
        else if (popMenuCanvas.enabled == false)
        {
            popMenuCanvas.enabled = true;
            Time.timeScale = 0;
        }
    }

    public void GameToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("OptionsScene");
    }
}
