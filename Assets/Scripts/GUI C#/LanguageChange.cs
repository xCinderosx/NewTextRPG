using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class LanguageChange : MonoBehaviour {

    public Dropdown LanguageMenu;
    public List<LanguagesTexts> LanguageList;
    [SerializeField] public static LanguagesTexts CurrentLanguage;

    public List<Text> textsList;

    public Canvas canvasMainMenu;

    public Text GameName;
    public Text optionName;
    public Button backbutton;

    public Button NewGamebutton;
    public Button Optionbutton;
    public Button Exitbutton;

    public Button languageOFFbutton;

    static bool FirstShow = true;

    private void Awake()
    {
        LanguageMenu.options.Clear();
        LanguageMenu.RefreshShownValue();
        foreach (LanguagesTexts language in LanguageList)
        {
            LanguageMenu.options.Add(new Dropdown.OptionData(language.LanguageName));
        }
        //LanguageMenu.value = 0;

        //CurrentLanguage = LanguageList[LanguageMenu.value];

        LanguageMenu.RefreshShownValue();

        // canvasMainMenu.GetComponent<LanguageChange1>().CurrentLanguage = CurrentLanguage;
        
        ChangeLanguage();

    }

    // Use this for initialization
    void Start () {
        canvasMainMenu.GetComponent<LanguageChange1>().CurrentLanguageMENU = CurrentLanguage;

    }

    public void ChangeLanguage()
    {
        CurrentLanguage = LanguageList[LanguageMenu.value];
        canvasMainMenu.GetComponent<LanguageChange1>().CurrentLanguageMENU = CurrentLanguage;
        canvasMainMenu.GetComponent<LanguageChange1>().ChangeLanguage();
        textsList[0].text = CurrentLanguage.LocalizationTexts[6];
        textsList[1].text = CurrentLanguage.LocalizationTexts[5];
        textsList[2].text = CurrentLanguage.LocalizationTexts[4];
        textsList[3].text = CurrentLanguage.LocalizationTexts[3];
        textsList[4].text = CurrentLanguage.LocalizationTexts[2];
        textsList[5].text = CurrentLanguage.LocalizationTexts[1];
        textsList[6].text = CurrentLanguage.LocalizationTexts[0];
    }
    

    // Update is called once per frame
    void Update ()
        {
            
        }
	}

