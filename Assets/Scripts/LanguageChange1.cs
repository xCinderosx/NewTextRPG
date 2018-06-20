using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class LanguageChange1 : MonoBehaviour {
    
    public LanguagesTexts CurrentLanguageMENU;

    public List<Text> textsList;

    public Canvas canvasOptions;
    

    public Button NewGamebutton;
    public Button Optionbutton;
    public Button Exitbutton;

    public Button ContinueButton;
    public Button Savebutton;
    public Button Loadbutton;

    private void Awake()
    {
        CurrentLanguageMENU = LanguageChange.CurrentLanguage;
        ChangeLanguage();
    }

    // Use this for initialization
    void Start () {
    }

    public void ChangeLanguage()
    {
        textsList[0].text = CurrentLanguageMENU.LocalizationTexts[6];
        textsList[1].text = CurrentLanguageMENU.LocalizationTexts[0];
        textsList[2].text = CurrentLanguageMENU.LocalizationTexts[1];
        textsList[3].text = CurrentLanguageMENU.LocalizationTexts[2];
        textsList[4].text = CurrentLanguageMENU.LocalizationTexts[7];
        textsList[5].text = CurrentLanguageMENU.LocalizationTexts[8];
        textsList[6].text = CurrentLanguageMENU.LocalizationTexts[9];
    }
    

    // Update is called once per frame
    void Update ()
        {
            
        }
	}

