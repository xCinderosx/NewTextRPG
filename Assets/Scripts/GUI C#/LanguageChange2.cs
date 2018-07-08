using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class LanguageChange2 : MonoBehaviour {
    
    public LanguagesTexts CurrentLanguageMENU, DefaultLanguage;

    public List<Text> textsList;
    

    private void Awake()
    {
        if (LanguageChange.CurrentLanguage == null)
        {
            LanguageChange.CurrentLanguage = DefaultLanguage;
        }
        CurrentLanguageMENU = LanguageChange.CurrentLanguage;
        
        
    }

    // Use this for initialization
    void Start () {
        ChangeLanguage();
    }

    public void ChangeLanguage()
    {
        textsList[0].text = CurrentLanguageMENU.LocalizationTexts[6];
        textsList[1].text = CurrentLanguageMENU.LocalizationTexts[0];
        textsList[2].text = CurrentLanguageMENU.LocalizationTexts[8];
        textsList[3].text = CurrentLanguageMENU.LocalizationTexts[1];
        textsList[4].text = CurrentLanguageMENU.LocalizationTexts[2];
        textsList[5].text = CurrentLanguageMENU.LocalizationTexts[11];
    }
    

    // Update is called once per frame
    void Update ()
        {
            
        }
	}

