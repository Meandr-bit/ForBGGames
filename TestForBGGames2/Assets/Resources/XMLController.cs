using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using TMPro;

public class XMLController : MonoBehaviour
{
    public const string path = "Languages";

    private void Start()
    {
        EnableRussian();

    }


    public void EnableEnglish()
    {
        LanguageItems lIs = LanguageItems.Load(path);
        foreach (LanguageItem lI in lIs.languageItems)
        {
            if(lI.name == "English")
            {
                UIController.instance.ShieldButton.GetComponentInChildren<TextMeshProUGUI>().text = lI.Shield;
                UIController.instance.ContinueButton.GetComponentInChildren<TextMeshProUGUI>().text = lI.Continue;
                UIController.instance.ExitB.GetComponentInChildren<TextMeshProUGUI>().text = lI.Exit;
                UIController.instance.PauseB.GetComponentInChildren<TextMeshProUGUI>().text = lI.Pause;
            }
        }
    }

    public void EnableRussian()
    {
        LanguageItems lIs = LanguageItems.Load(path);
        foreach (LanguageItem lI in lIs.languageItems)
        {
            if (lI.name == "Russian")
            {
                UIController.instance.ShieldButton.GetComponentInChildren<TextMeshProUGUI>().text = lI.Shield;
                UIController.instance.ContinueButton.GetComponentInChildren<TextMeshProUGUI>().text = lI.Continue;
                UIController.instance.ExitB.GetComponentInChildren<TextMeshProUGUI>().text = lI.Exit;
                UIController.instance.PauseB.GetComponentInChildren<TextMeshProUGUI>().text = lI.Pause;
            }
        }
    }
}
[XmlRoot("AllLanguages")]
public class LanguageItems
{
    [XmlArray("LanguageItems")]
    [XmlArrayItem("LanguageItem")]
    public List<LanguageItem> languageItems = new List<LanguageItem>();

    public static LanguageItems Load(string path)
    {
        TextAsset _xml = Resources.Load<TextAsset>(path);
        XmlSerializer serializer = new XmlSerializer(typeof(LanguageItems));
        StringReader reader = new StringReader(_xml.text);
        LanguageItems lItems = serializer.Deserialize(reader) as LanguageItems;
        reader.Close();
        return lItems;
    }
}

[System.Serializable]
public class LanguageItem
{
    [XmlAttribute("name")]
    public string name;
    public string Exit;
    public string Pause;
    public string Shield;
    public string Continue;
}