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

    public CharacterInfo mainPlayerInfo;

    [Header("Player Health UI")]
    public TextMeshProUGUI healthText;

    [Header("Player Elims UI")]
    public TextMeshProUGUI elimsText;

    [Header("End Game Screen")]
    public GameObject gameEndScreen;
    public TextMeshProUGUI winnerNameText;
    public TextMeshProUGUI winnerElimsText;

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
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(startDelay());
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
        gameEndScreen.SetActive(true);
    }

    public void FindWinner()
    {
        int loops = 0;
        bool sorting = true;

        
        while (sorting)
        {
            loops++;
            bool newWinner = false;
            
            foreach (CharacterInfo c in characterManager.characters)
            {
                if (c.info.eliminations >= highestScore)
                {
                    highestScore = c.info.eliminations;
                    highestScoreName = c.info.characterName;
                    newWinner = true;
                    
                } else if (c.info.eliminations >= secondScore)
                {
                    secondScore = c.info.eliminations;
                    secondScoreName = c.info.characterName;
                    newWinner = true;
                    
                } else if (c.info.eliminations >= thirdScore)
                {
                    thirdScore = c.info.eliminations;
                    thirdScoreName = c.info.characterName;
                    newWinner = true;
                    
                }
            }
            //Debug.Log("Loops: " + loops.ToString());
            if (newWinner)
            {
                sorting = false;
            }
        }
        Debug.Log("Total Loops: " + loops.ToString());

        
        
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
