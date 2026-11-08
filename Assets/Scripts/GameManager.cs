using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int fish = 0;
    public int fishValue = 100;
    int fishThisLevel = 0;
    int timeScore = 0;
    int stageScore = 0;
    int score = 0;
    AudioSource audioSource;
    public AudioClip pickupSound;
    public int lives = 3;
    public HUD gui;
    public int fishFor1Up;
    public Timer timer;
    public int countdownSeconds = 3;
    private bool hasWon = false;
    private bool isTimedOut = false; // player has run out of time
    private bool isGameOver = false; // the run is over (no lives left)
    PlayerController playerController;
    public int retryCountdownSeconds = 3;
    public float gameOverWait = 3f;
    int stageStartScore;
    int stageStartFish;
    Pickup[] pickups;
    GameObject[] stages;
    int currentStage = 1;

    private void Start()
    {
        pickups = FindObjectsByType<Pickup>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        playerController = GetComponent<PlayerController>();
        currentStage = 1;
        fish = 0;
        fishThisLevel = 0;
        audioSource = GetComponent<AudioSource>();
        gui.UpdateFish(fish, fishFor1Up);
        gui.UpdateLives(lives);
        gui.UpdateScore(score);
        playerController.DisableMovement();
        timer.ResetTimer();
        stageStartScore = score;
        stageStartFish = fish;
        StartCoroutine(BeginStage(true));
    }

    void Update()
    {
        if(timer.timeRemaining <= 0 && !hasWon && !isTimedOut && !isGameOver)
        {
            isTimedOut = true;
            TimeOut();
        }
    }
    public void FishPickup()
    {
        audioSource.PlayOneShot(pickupSound);
        fish++;

        fishThisLevel++;
        stageScore += fishValue;
        score += fishValue;
        gui.UpdateFish(fish, fishFor1Up);
        gui.UpdateScore(score);
        if(fish >= fishFor1Up)
        {
            lives++;
            fish = 0; // Reset the counter -- collected fish already added to total score so nothing is lost
            gui.UpdateFish(fish, fishFor1Up);
            gui.UpdateLives(lives);
        }
    }

    public void LoseLife()
    {
        lives--;
        gui.UpdateLives(lives);
    }
    void TimeOut()
    {
        playerController.FreezeControls();
        playerController.SlowDown();
        gui.ShowTimeOut();
        LoseLife();
        if (OutOfLives)
            GameOver();
        else
            StartCoroutine(RetryRoutine());
    }

    public bool OutOfLives => lives <= 0;

    public void GameOver()
    {
        isGameOver = true;
        timer.StopTimer();
        playerController.FreezeControls();
        StartCoroutine(GameOverRoutine());
        currentStage = 1;
    }

    IEnumerator RetryRoutine()
    {
        for (int i = retryCountdownSeconds; i > 0; i--)
        {
            gui.ShowRetryCountdown(i);
            yield return new WaitForSeconds(1f);
        }
        ResetStageProgress();
        StartStage(false); // a retry skips the intro
    }

    IEnumerator GameOverRoutine()
    {
        gui.ShowGameOver();
        yield return new WaitForSeconds(gameOverWait);
        GoToGameOverScreen();
    }

    void GoToGameOverScreen()
    {
        currentStage = 1;
        bool isHighScore = HighScores.TrySetTotalHighScore(score);
        // TODO: load the game over screen once it exists
        Debug.Log("Game over");
    }

    void ResetStageProgress()
    {
        timeScore = 0;
        fishThisLevel = 0;
        stageScore = 0;
        score = stageStartScore;
        fish = stageStartFish;
        gui.UpdateScore(score);
        gui.UpdateFish(fish, fishFor1Up);
        RespawnPickups();
    }

    void RespawnPickups()
    {
        foreach (Pickup pickup in pickups)
            pickup.gameObject.SetActive(true);
    }

    void CalcScore ()
    {   if (!hasWon && !isTimedOut && !isGameOver)
        {
            hasWon = true;
            timer.StopTimer();
            timeScore = (int)(Mathf.Round(timer.timeRemaining * 1000));
            stageScore += timeScore;
            score += timeScore;
            gui.UpdateScore(score);
        }
    }
    
    public void EndStage()
    {
        
        CalcScore();
        bool isHighScore = HighScores.TrySetStageHighScore(currentStage, stageScore);
        gui.ShowStageClear(timeScore, fishThisLevel * fishValue, stageScore, score, isHighScore);
        playerController.FreezeControls();
    }

    public void StartStage(bool playIntro)
    {
        currentStage++;
        gui.HideStageClear();
        gui.HideTimeOut();
        timeScore = 0;
        fishThisLevel = 0;
        stageScore = 0;
        hasWon = false;
        isTimedOut = false;

        playerController.ResetToRespawn();
        timer.ResetTimer();
        stageStartScore = score;
        stageStartFish = fish;
        StartCoroutine(BeginStage(playIntro));
    }

    IEnumerator BeginStage(bool playIntro)
    {
        if (playIntro)
        {
            gui.UpdateStageTitle(currentStage, HighScores.GetStageHighScore(currentStage));
            yield return StartCoroutine(gui.StagePreview());
        }
        yield return StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        for (int i = countdownSeconds; i > 0; i--)
        {
            gui.ShowCountdown(i.ToString(), countdownSeconds - i);
            yield return new WaitForSeconds(1f);
        }
        gui.ShowCountdown("GO!", countdownSeconds, true);
        playerController.EnableControls();
        timer.StartTimer();
        yield return new WaitForSeconds(0.7f);
        gui.HideCountdown();
    }
}
