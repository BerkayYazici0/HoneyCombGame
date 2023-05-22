using PathCreation;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject menuPanel, gamePanel, failPanel, successPanel;
    public GameObject starPath, star, triangle, trianglePath;
    public GameObject needle;
    private void Start()
    {
        HideAllPanel();
        menuPanel.SetActive(true);
    }


    public void StartGameButton()
    {
        GameManager.Instance.StartGame();
        HideAllPanel();
        gamePanel.SetActive(true);
    }

    [System.Obsolete]
    

    public void NextLevelButton()
    {
        GameManager.Instance.NextLevel();
        HideAllPanel();
        gamePanel.SetActive(true);
        star.SetActive(true);
        starPath.SetActive(true);
        triangle.SetActive(false);
        trianglePath.SetActive(false);
        SugarBreakup.Instance.i = 0;
        SugarBreakup.Instance.pathPoints.Clear();
        SugarBreakup.Instance.levelObject = GameObject.FindGameObjectsWithTag("Sugar");
        SugarBreakup.Instance.sugars = GameObject.FindGameObjectsWithTag("Star");
        SugarBreakup.Instance.brokenSugars = GameObject.FindGameObjectsWithTag("Broken");
        SugarBreakup.Instance.brokenLevelObject = GameObject.FindGameObjectsWithTag("StarBroken");
        SugarBreakup.Instance.currentPath = GameObject.FindGameObjectWithTag("Path");
        SugarBreakup.Instance.path = SugarBreakup.Instance.currentPath.GetComponent<PathCreator>();

        for (int i = 0; i < SugarBreakup.Instance.brokenSugars.Length; i++)
        {
            SugarBreakup.Instance.brokenSugars[i].transform.gameObject.SetActive(false);
        }
        for (int i = 0; i < SugarBreakup.Instance.brokenLevelObject.Length; i++)
        {
            SugarBreakup.Instance.brokenLevelObject[i].transform.gameObject.SetActive(false);
        }
        for (int i = 0; i < SugarBreakup.Instance.path.path.NumPoints; i++)
        {
            SugarBreakup.Instance.pathPoints.Add(SugarBreakup.Instance.path.path.GetPoint(i));
        }
    }

    public void Success()
    {
        HideAllPanel();
        successPanel.SetActive(true);
    }

    public void Fail()
    {
        HideAllPanel();
        failPanel.SetActive(true);
    }

    public void RestartLevelButton()
    {
        GameManager.Instance.RestartLevel();
        HideAllPanel();
        gamePanel.SetActive(true);
        SceneManager.LoadScene("SampleScene");
    }

    private void HideAllPanel()
    {
        menuPanel.SetActive(false);
        gamePanel.SetActive(false);
        failPanel.SetActive(false);
        successPanel.SetActive(false);
    }
}
