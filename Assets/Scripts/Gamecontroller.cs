using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gamecontroller : MonoBehaviour
{

    public static Gamecontroller instance;

    public int totalHearts;

    public Button insend;

    public GameObject StartGameButton;
    public GameObject HeartPrefab;
    public GameObject GoldenHeartPrefab;
    public GameObject RottenHeartPrefab;
    public GameObject InputScorePanel;
    public Text PanelScoreDisplay;

    public float CurrentSpeed = 1;

    public int Points = 0;

    public Text ScoreText, TimeText;
    private int _pointsPerHeart = 50;

    private Vector3 _topLeft, _topRight, _bottomLeft, _bottomRight;
    private bool _runSpawner;

    [SerializeField]
    private float _counter;

    private int _round = 0;

    //
    dreamloLeaderBoard dl;
    dreamloPromoCode pc;

    public Text LeaderBoardText;
    public Text LeaderBoardScoreText;
    public InputField GOInput;

    private bool _getLoaderboard;

    // skrald
    public string privateCode = "";
    public string publicCode = "";
    public string DreamloURL;

    public Button LeaderboardButton;


    public float DistanceCenterToSide;

    // Dreamlo http://dreamlo.com/lb/qJ9WiOZafE6bxhb6Xxzb0QXgW8WHU8K0CB-NBSnOuvTw

    public GameObject HeartParticlesPrefab, MusicBox;

    private Musicbox mb;

    private float ComboCounter;

    public GameObject indicator;

    public Dropdown klub;

    public int[] rottenHeart;
    public int[] goldenHeart;

    const string glyphs = "abcdefghijklmnopqrstuvwxyz0123456789";

    public void newPlayer()
    {
        string id = "";

        for (int i = 0; i < 8; i++)
        {
            id += glyphs[Random.Range(0, glyphs.Length)];
        }

        PlayerPrefs.SetString("PlayerID", id);
    }

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("PlayerID"))
            newPlayer();

        instance = this;

        rottenHeart[0] = Random.Range(0, 26);
        rottenHeart[1] = Random.Range(27, 52);
        rottenHeart[2] = Random.Range(53, 70);

        goldenHeart[0] = Random.Range(0, 26);
        goldenHeart[1] = Random.Range(27, 52);
        goldenHeart[2] = Random.Range(53, 70);
    }

    // Use this for initialization
    void Start()
    {
        klub.value = PlayerPrefs.GetInt("sidsteKlub");

        mb = GameObject.Find("musicbox").GetComponent<Musicbox>();

        // get the reference here...
        this.dl = dreamloLeaderBoard.GetSceneDreamloLeaderboard();

        // get the other reference here
        this.pc = dreamloPromoCode.GetSceneDreamloPromoCode();

        calculateMinMax(); // calc for different screen sizes

        dl.LoadScores();
        StartCoroutine(DumbHack());

    }

    IEnumerator DumbHack()
    {
        LeaderboardButton.interactable = false;
        yield return new WaitForSeconds(0.5f);
        LeaderboardButton.interactable = true;
    }



    void calculateMinMax()
    {
        _topLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 10));
        _topRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 10));
        _bottomRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 10));
        _bottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 10));


        DistanceCenterToSide = Vector3.Distance(Vector3.zero, new Vector3(_topLeft.x, _topLeft.y, 0));

        //print(_topLeft);
        //print(_topRight);
        //print(_bottomLeft);
        //print(_bottomRight);
    }

    IEnumerator RunSpawner()
    {
        while (_runSpawner)
        {

            GameObject heart = null;
            // Random spawn inside rect
            for (int i = 0; i < goldenHeart.Length; i++)
            {
                if (totalHearts == goldenHeart[i])
                    heart = Instantiate(GoldenHeartPrefab) as GameObject;
            }

            for (int i = 0; i < rottenHeart.Length; i++)
            {
                if (totalHearts == rottenHeart[i])
                    heart = Instantiate(RottenHeartPrefab) as GameObject;
            }

            if (heart == null)
                heart = Instantiate(HeartPrefab) as GameObject;

            float x = Random.Range(_bottomLeft.x, _bottomRight.x);
            float y = Random.Range(_bottomLeft.y, _topLeft.y);
            totalHearts++;

            heart.transform.position = new Vector3(x, y, 10);

            yield return new WaitForSeconds(Random.Range(0.1f, 0.4f));
        }
    }


    IEnumerator EndGame()
    {
        _runSpawner = false;
        yield return new WaitForSeconds(1);
        InputScorePanel.SetActive(true);
        PanelScoreDisplay.text = Points + " point";
    }


    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }





    // Update is called once per frame
    void Update()
    {
        if (submitPeriod.instance.winnerName == "")
            setWinner();

        if (GOInput.text.Length == 0 || klub.value == 0)
            insend.interactable = false;
        else
            insend.interactable = true;

        ComboCounter += Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition),
                Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.collider.tag == "Heart")
                {
                    HitHeart(hit.collider.gameObject);
                }
            }
        }

        if (_runSpawner)
        {
            _counter -= Time.deltaTime;
            TimeText.text = "Tid tilbage: " + (Mathf.Floor(_counter) + 1).ToString();
            if (_counter <= 0)
            {
                StartCoroutine(EndGame());
            }
        }
    }

    int combocount = 0;

    public void OpenURL(string url)
    {
        Application.OpenURL(url);
    }

    void HitHeart(GameObject target)
    {
        int PointWeGave = 0;
        if (ComboCounter <= 0.7f)
        {
            combocount += 1;
            float difference = (0.5f - ComboCounter) + combocount;
            Points += (int)(target.GetComponentInParent<Heart>().Point * difference);
            PointWeGave = (int)(target.GetComponentInParent<Heart>().Point * difference);
        }
        else
        {
            combocount = 0;
            Points += target.GetComponentInParent<Heart>().Point;
            PointWeGave = target.GetComponentInParent<Heart>().Point;
            print("non combo");
        }

        GameObject hitter = Instantiate(indicator) as GameObject;
        hitter.transform.position = target.transform.position;
        hitter.GetComponentInChildren<Text>().text = PointWeGave.ToString();
        hitter.GetComponentInChildren<Text>().color = target.GetComponentInParent<Heart>().textColor;

        ScoreText.text = Points.ToString();

        GameObject tmp = Instantiate(target.GetComponentInParent<Heart>().ps, target.transform.position, Quaternion.identity);

        mb.POPONE();

        Destroy(target);

        ComboCounter = 0;
    }

    public void StartGame()
    {
        StartGameButton.SetActive(false);
        _runSpawner = true;
        StartCoroutine(RunSpawner());
    }

    IEnumerator savescore ()
    {
        WWW www = new WWW(dl.dreamloWebserviceURL + "5bae2eb1613a88061429d460" + "/pipe-get/" + WWW.EscapeURL("bY0dqpzLrCvwt2iO3aOg"));
        yield return www;
        string s = www.text;

        char[] dollarSign = "$".ToCharArray();
        char[] stick = "|".ToCharArray();


        string[] all = s.Split(stick[0]);
        string gameInfoString = all[3];

        string[] splittedGameInfo = gameInfoString.Split(dollarSign[0]); 

        switch (splittedGameInfo[1].ToString().ToLower())
        {
            case "true":
                break;
            case "false":
                PlayerPrefs.SetInt("sidsteKlub", klub.value);
                // add the score...
                if (dl.publicCode == "") Debug.LogError("You forgot to set the publicCode variable");
                if (dl.privateCode == "") Debug.LogError("You forgot to set the privateCode variable");

                dl.AddScore(GOInput.text + "$" + klub.value + "$" + PlayerPrefs.GetString("PlayerID"), Points); // add the score
                dl.LoadScores();
                StartCoroutine(DumbWaitHAckAgain());
                break;
        }
    }

    public void SaveScore()
    {
        StartCoroutine(savescore());
    }

    IEnumerator DumbWaitHAckAgain()
    {
        yield return new WaitForSeconds(1.3f);
        LoadScores();
        setWinner();
    }

    public void LoadScores()
    {
        StartCoroutine(LoadScoresCoroutine());

    }

    public GameObject LeaderBoard;

    public void LoadLeaderBoard()
    {
        LeaderBoard.SetActive(true);

        LoadScores();
        //  _getLoaderboard = true;

        // LoadScores();
    }

    public void setWinner()
    {
        List<dreamloLeaderBoard.Score> scoreList = dl.ToListHighToLow();

        if (scoreList.Count == 0)
            return;

        string[] y = scoreList[0].playerName.Split("$".ToCharArray());
        submitPeriod.instance.winnerName = y[0];
        submitPeriod.instance.winnerClub = klub.options[int.Parse(y[1])].text;
        submitPeriod.instance.winnerScore = scoreList[0].score;
    }

    IEnumerator LoadScoresCoroutine()
    {
        bool closedUpdate = true;


        while (closedUpdate)
        {
            List<dreamloLeaderBoard.Score> scoreList = dl.ToListHighToLow();

            if (scoreList == null)
            {
                print("Score list is null");
                yield return null;
            }
            else
            {
                print("score list is not null");
                int maxToDisplay = 10;
                int count = 0;

                print("list size: " + scoreList.Count);
                string localhelper = "";
                string localscores = "";
                foreach (dreamloLeaderBoard.Score currentScore in scoreList)
                {
                    print("checking score" + currentScore.playerName);
                    count++;

                    string[] x = currentScore.playerName.Split("$".ToCharArray());
                    localhelper += x[0] + " <color=#BCBCBC9D>fra</color> " + klub.options[int.Parse(x[1])].text + "\n";
                    localscores += currentScore.score + "\n";

                    if (count >= maxToDisplay)

                        break;
                }
                LeaderBoardText.text = localhelper.Replace("+", " ");
                LeaderBoardScoreText.text = localscores;
                yield return null;
            }
            closedUpdate = false;
        }

        yield break;
    }


}
