using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Language", menuName = "Languages/NewLanguage")]

public class LanguagesTexts : ScriptableObject
{
    public string LanguageName = "Polski";

    public List<string> LocalizationTexts;
}
