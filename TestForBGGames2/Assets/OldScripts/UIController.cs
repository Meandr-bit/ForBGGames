using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject ShieldButton, ContinueButton, DarkPanel;
    Coroutine eS = null;
    float shieldTimer;

    public void ButtonDownShield()
    {
        if (eS == null)
        {
            eS = StartCoroutine(EnableShield());
        }
    }

    public void ButtonUpShield()
    {
        ShieldButton.GetComponent<Button>().interactable = true;
        StopCoroutine(eS);
        eS = null;
        PlayerController.instance.DisableShield();
    }

    IEnumerator EnableShield()
    {
        PlayerController.instance.EnableShield();
        yield return new WaitForSeconds(2f);
        ShieldButton.GetComponent<Button>().interactable = false;
        PlayerController.instance.DisableShield();
    }

    public void Pause()
    {
        ContinueButton.SetActive(true);
        Time.timeScale = 0.000001f;
        //��������� �����
        StartCoroutine(DarkenTheScreen());
    }

    IEnumerator DarkenTheScreen()
    {
        DarkPanel.GetComponent<Image>().enabled = true;
        float step = 0.05f;
        while (true)
        {
            Color currentColor = DarkPanel.GetComponent<Image>().color;
            DarkPanel.GetComponent<Image>().color = new Color(currentColor.r, currentColor.g, currentColor.b, currentColor.a + step);
            yield return new WaitForSeconds(Time.timeScale*0.1f);
        }
    }

    public void Continue()
    {
        DarkPanel.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        DarkPanel.GetComponent<Image>().enabled = false;
        Time.timeScale = 1;
        ContinueButton.SetActive(false);
    }
}