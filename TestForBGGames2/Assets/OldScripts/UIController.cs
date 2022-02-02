using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Pathfinding;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    public GameObject ShieldButton, ContinueButton, DarkPanel;
    Coroutine eS = null;
    Coroutine darkScreen = null;
    float shieldTimer;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        } else if(instance != this)
        {
            Destroy(gameObject);
        }
    }

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
        //затемняем экран
        darkScreen = StartCoroutine(DarkenTheScreen());
    }

    IEnumerator DarkenTheScreen()
    {
        DarkPanel.GetComponent<Image>().enabled = true;
        float step = 0.05f;
        while (true)
        {
            Color currentColor = DarkPanel.GetComponent<Image>().color;
            DarkPanel.GetComponent<Image>().color = new Color(currentColor.r, currentColor.g, currentColor.b, currentColor.a + step);
            if (currentColor.a >= 1f) break;
            yield return new WaitForSeconds(Time.timeScale*0.1f);
        }
    }

    IEnumerator LightenTheScreen()
    {
        float step = 0.05f;
        while (true)
        {
            Color currentColor = DarkPanel.GetComponent<Image>().color;
            DarkPanel.GetComponent<Image>().color = new Color(currentColor.r, currentColor.g, currentColor.b, currentColor.a - step);
            if (currentColor.a <= 0.1f) break;
            yield return new WaitForSeconds(Time.timeScale * 0.1f);
        }
        DarkPanel.GetComponent<Image>().enabled = false;

    }

    public void Continue()
    {
        StopCoroutine(darkScreen);
        DarkPanel.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        DarkPanel.GetComponent<Image>().enabled = false;
        Time.timeScale = 1;
        ContinueButton.SetActive(false);
    }

    public void ExitButton()
    {
        if (Application.isEditor)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
        else
        {
            Application.Quit();
        }
    }

    public IEnumerator LevelCompleted()
    {
        yield return new WaitForSeconds(2);
        StartCoroutine(DarkenTheScreen());
        yield return new WaitForSeconds(2);
        Debug.Log("1");
        PlayerController.instance.gameObject.SetActive(false);
        yield return new WaitForSeconds(4);
        MazeSpawner.instance.PlaceNewMaze();
        Debug.Log("2");
        StartCoroutine(LightenTheScreen());
        yield return new WaitForSeconds(2);
        Debug.Log("3");
        PlayerController.instance.gameObject.SetActive(true);
        PlayerController.instance.GetComponent<AIPath>().canMove = false;
        PlayerController.instance.transform.position = PlayerController.instance.startPos;
        PlayerController.instance.GetComponent<AIPath>().destination = PlayerController.instance.Finish.transform.position;
        yield return new WaitForSeconds(2);
        PlayerController.instance.GetComponent<AIPath>().canMove = true;
    }
}
