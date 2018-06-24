using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Serializer : MonoBehaviour {

    static string SAVE_FILE = "SAVE_1.json";

    public GameObject PlayerCharacter;

    public Dropdown SavesDropdown;
    public Button SavesButton;
    public Button Menu1Button;
    public Button Menu2Button;
    public Button Menu3Button;
    public Button Menu4Button;
    public Button Menu5Button;

    public string filename;
    static int SaveCount = 1;
    public static string roamingFolder;


    bool NameSelector(string Text)
    {
        //Debug.Log(Text);
        if (File.Exists(Text))
        {
            SaveCount++;
            SAVE_FILE = "SAVE_" + SaveCount + "_" + System.DateTime.Now.Date.ToShortDateString() + "_" + System.DateTime.Now.Hour + ":" + System.DateTime.Now.Minute + ".json";
            return true;
        }
        else { return true; }
    }

    private void Start()
    {


    }

    public void Save()
    {
        SaveData data = new SaveData()
        { PlayerLanguage = LanguageChange.CurrentLanguage,
            postion = PlayerCharacter.transform.position,
            rotation = PlayerCharacter.transform.rotation
        };

        string json = JsonUtility.ToJson(data);

        filename = Path.Combine(Application.persistentDataPath, SAVE_FILE);

        if (NameSelector(filename))
        {
            File.WriteAllText(filename, json);
        }

        Debug.Log(File.Exists(filename));


    }

    public void CloseLoad()
    {
        Menu1Button.interactable = true;
        Menu2Button.interactable = true;
        Menu3Button.interactable = true;
        Menu4Button.interactable = true;
        Menu5Button.interactable = true;

        Menu5Button.interactable = false;
        SavesDropdown.interactable = false;
        SavesButton.interactable = false;
    }

    public void OpenLoad()
    {
        Menu1Button.interactable = false;
        Menu2Button.interactable = false;
        Menu3Button.interactable = false;
        Menu4Button.interactable = false;
        



        SavesDropdown.ClearOptions();
        SavesDropdown.RefreshShownValue();
        var info = new DirectoryInfo(Application.persistentDataPath);
        var fileInfo = info.GetFiles();
        foreach (var item in fileInfo)
        {
            if (item.Extension.Contains("json"))
            {
                SavesDropdown.options.Add(new Dropdown.OptionData(item.Name));
            }
        }

        SavesDropdown.RefreshShownValue();
        Menu5Button.interactable = true;
        SavesDropdown.interactable = true;
        SavesButton.interactable = true;
    }
	
	public void Load(string SaveFile)
    {


        string jsonFromFile = File.ReadAllText(SaveFile);
        SaveData CopyToLoad = JsonUtility.FromJson<SaveData>(jsonFromFile);


        LanguageChange.CurrentLanguage = CopyToLoad.PlayerLanguage;
        PlayerCharacter.transform.position = CopyToLoad.postion;
        PlayerCharacter.transform.rotation = CopyToLoad.rotation;

	}

}
