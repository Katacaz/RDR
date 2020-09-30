using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoloDeathmatchManager : MonoBehaviour
{

    [Header("Timer")]
    public bool timerStarted;
    public float roundTimer;
    private float timerCounter;
    private float timeRatio;
    public TextMeshProUGUI timerText;
    public Image ratioImage;
    //public Animator timerAnimator;

    [Header("Managers")]
    CharacterManager characterManager;
    RespawnManager respawnManager;
    EliminationManager eliminationManager;
    TeamManager teamManager;
    GameModeSettingsManager gameModeSettingsManager;

    public CharacterInfo mainPlayerInfo;

    [Header("Player Health UI")]
    public TextMeshProUGUI healthText;

    [Header("Player Elims UI")]
    public TextMeshProUGUI elimsText;

    [Header("End Game Screen")]
    public GameObject gameEndScreen;
    public TextMeshProUGUI winnerNameText;
    public TextMeshProUGUI winnerElimsText;
    public TextMeshProUGUI secondNameText;
    public TextMeshProUGUI secondScoreText;
    public TextMeshProUGUI thirdNameText;
    public TextMeshProUGUI thirdScoreText;

    int highestScore = 0;
    string highestScoreName;
    int secondScore = 0;
    string secondScoreName;
    int thirdScore = 0;
    string thirdScoreName;

    bool startTimerActive = true;
    public int startDelayTime = 3;
    public TextMeshProUGUI startTimerCountdown;
    private void Awake()
    {
        Time.timeScale = 0f;
        gameEndScreen.SetActive(false);
        characterManager = GetComponent<CharacterManager>();
        respawnManager = GetComponent<RespawnManager>();
        eliminationManager = GetComponent<EliminationManager>();
        teamManager = GetComponent<TeamManager>();
        gameModeSettingsManager = GetComponent<GameModeSettingsManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        startTimerActive = true;
        StartCoroutine(startDelay());
        teamManager.charManager = characterManager;
        teamManager.SeperateTeams();
    }

    IEnumerator startDelay()
    {
        for (int i = startDelayTime; i > 0; i-- ){
            startTimerCountdown.text = i.ToString();
            yield return new WaitForSecondsRealtime(1f);

        }
        startTimerCountdown.text = "GO!";
        yield return new WaitForSecondsRealtime(0.5f);
        startTimerCountdown.gameObject.SetActive(false);
        startTimerActive = false;
        StartRound();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHeartUI();
        UpdateElimsUI();
        if (timerStarted)
        {
            if (timerCounter > 0)
            {
                timerCounter -= Time.deltaTime;
                
            } else
            {
                timerStarted = false;
                RoundEnd();
            }
            UpdateTimerUI();
        }
    }

    public void StartRound()
    {
        Time.timeScale = 1f;
        timerStarted = true;
        timerCounter = roundTimer;
        respawnManager.SetStockLives();
        Debug.Log("Round Started");
    }
    public void UpdateTimerUI()
    {
        timeRatio = timerCounter / roundTimer;
        ratioImage.fillAmount = 1 - timeRatio;
        timerText.text = Mathf.RoundToInt(timerCounter).ToString();
        //timerAnimator.SetFloat("Time", timerCounter);
    }

    public void UpdateHeartUI()
    {
        healthText.text = mainPlayerInfo.info.health.ToString();
    }
    public void UpdateElimsUI()
    {
        elimsText.text = mainPlayerInfo.info.eliminations.ToString();
    }

    public void RoundEnd()
    {
        Time.timeScale = 0f;
        FindWinner();
        winnerNameText.text = highestScoreName;
        winnerElimsText.text = highestScore.ToString();
        secondNameText.text = secondScoreName;
        secondScoreText.text = secondScore.ToString();
        thirdNameText.text = thirdScoreName;
        thirdScoreText.text = thirdScore.ToString();
        gameEndScreen.SetActive(true);
    }

    public void FindWinner()
    {

        ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
        ScoreInfo firstInfo = ScriptableObject.CreateInstance(typeof(ScoreInfo)) as ScoreInfo;
        firstInfo = scoreManager.FindScore(0);
        ScoreInfo secondInfo = ScriptableObject.CreateInstance(typeof(ScoreInfo)) as ScoreInfo;
        secondInfo = scoreManager.FindScore(1);
        ScoreInfo thirdInfo = ScriptableObject.CreateInstance(typeof(ScoreInfo)) as ScoreInfo;
        thirdInfo = scoreManager.FindScore(2);

        highestScore = firstInfo.score;
        highestScoreName = firstInfo.username;

        secondScore = secondInfo.score;
        secondScoreName = secondInfo.username;

        thirdScore = thirdInfo.score;
        thirdScoreName = thirdInfo.username;

        


    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
