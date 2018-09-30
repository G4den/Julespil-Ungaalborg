using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;


public class submitPeriod : MonoBehaviour
{

    public GameObject submitUI;
    public GameObject button;
    public Text submitButtonText;
    public static submitPeriod instance = null;
    public Text winnerText;
    public GameObject winnerUI;
    public GameObject leaderboard;
    public Text WinnerYearUIText;
    public dreamloLeaderBoard dreamlo;

    public Text menuText;

    public string winnerName;
    public string winnerClub;
    public int winnerScore;

    [Header("Text prefixes")]
    public string MenuText;
    public string MenuText2;
    public string WinneYearText;
    public string SeeWinnerButtonText;



    [HideInInspector]
    public string EndDate;
    [HideInInspector]
    public bool IsOver;
    [HideInInspector]
    public int Year;

    [HideInInspector]
    public string endDate;
    [HideInInspector]
    public bool isOver;
    [HideInInspector]
    public int year;

    public void submitScoreButton()
    {
        if (isOver)
        {
            winnerText.text = winnerName + " <color=#BCBCBC9D>fra</color> " + winnerClub + " <color=#BCBCBC9D>med en score på hele</color> " + winnerScore + " point!";
            winnerUI.SetActive(true);
        }

        else
            submitUI.SetActive(true);
    }

    private void Awake()
    {
        instance = this;
        menuText.text = PlayerPrefs.GetString("menutext");
    }

    private void Start()
    {
        GetInfo();
    }

    public void SubmitChanges()
    {
        WWW www = new WWW(dreamlo.dreamloWebserviceURL + "uHQ4c5va9UyAg95d1HZUPg3Ml7DlVOK0uP3HVXdkuk2Q" + "/add-pipe/" + WWW.EscapeURL("bY0dqpzLrCvwt2iO3aOg") + "/" + 0 + "/" + 0 + "/" + WWW.EscapeURL(EndDate) + "$" + IsOver + "$" + Year);
    }

    public void GetInfo()
    {
        StartCoroutine(getGameInfo());
    }

    public void SetYearText()
    {
        if (isOver && Application.isPlaying)
            WinnerYearUIText.text = WinneYearText.Replace("[year]", year.ToString());
    }

    IEnumerator getGameInfo()
    {
        WWW www = new WWW(dreamlo.dreamloWebserviceURL + "5bae2eb1613a88061429d460" + "/pipe-get/" + WWW.EscapeURL("bY0dqpzLrCvwt2iO3aOg"));
        yield return www;
        string s = www.text;

        char[] dollarSign = "$".ToCharArray();
        char[] stick = "|".ToCharArray();


        string[] all = s.Split(stick[0]);
        string gameInfoString = all[3];

        string[] splittedGameInfo = gameInfoString.Split(dollarSign[0]);

        endDate = splittedGameInfo[0].Replace("+", " ");
        year = int.Parse(splittedGameInfo[2]);

        switch (splittedGameInfo[1].ToString().ToLower())
        {
            case "true":
                isOver = true;
                submitButtonText.text = "Se årets vinder";
                menuText.text = MenuText.Replace("[date]", endDate);
                PlayerPrefs.SetString("menutext", MenuText.Replace("[date]", endDate));
                submitButtonText.text = SeeWinnerButtonText;
                break;
            case "false":
                submitButtonText.text = "Insend score";
                PlayerPrefs.SetString("menutext", MenuText2.Replace("[date]", endDate));
                menuText.text = MenuText2.Replace("[date]", endDate);
                isOver = false;
                break;
        }
        button.SetActive(true);
        SetYearText();
    }
}
