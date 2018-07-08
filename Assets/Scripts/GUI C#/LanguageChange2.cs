using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class LanguageChange2 : MonoBehaviour {

    [TextArea] public LanguagesTexts CurrentLanguageMENU, DefaultLanguage;

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
        textsList[0].text = CurrentLanguageMENU.LocalizationTexts[6].Replace("\\n", "\n");
        textsList[1].text = CurrentLanguageMENU.LocalizationTexts[0].Replace("\\n", "\n");
        textsList[2].text = CurrentLanguageMENU.LocalizationTexts[8].Replace("\\n", "\n");
        textsList[3].text = CurrentLanguageMENU.LocalizationTexts[1].Replace("\\n", "\n");
        textsList[4].text = CurrentLanguageMENU.LocalizationTexts[2].Replace("\\n", "\n");
        textsList[5].text = CurrentLanguageMENU.LocalizationTexts[11].Replace("\\n", "\n");
    }
    

    // Update is called once per frame
    void Update ()
        {
            
        }
	}

