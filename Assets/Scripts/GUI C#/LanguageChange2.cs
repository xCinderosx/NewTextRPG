using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class LanguageChange2 : MonoBehaviour {
    
    public LanguagesTexts CurrentLanguageMENU;

    public List<Text> textsList;

    public Button ContinueButton;
    public Button Savebutton;
    public Button Loadbutton;
    public Button Exitbutton;
    
    private void Awake()
    {
        
    }

    // Use this for initialization
    void Start () {
        ChangeLanguage();
    }

    public void ChangeLanguage()
    {
        textsList[0].text = CurrentLanguageMENU.LocalizationTexts[6];
        textsList[1].text = CurrentLanguageMENU.LocalizationTexts[0];
        textsList[2].text = CurrentLanguageMENU.LocalizationTexts[1];
        textsList[3].text = CurrentLanguageMENU.LocalizationTexts[2];
    }
    

    // Update is called once per frame
    void Update ()
        {
            
        }
	}

