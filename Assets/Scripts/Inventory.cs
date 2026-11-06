using System;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int fish = 0;
    public int fishValue = 100;
    int fishThisLevel = 0;
    int timeScore = 0;
    int levelScore = 0;
    int score = 0;
    AudioSource audioSource;
    public AudioClip pickupSound;
    public int lives = 3;
    public InventoryGUI gui;
    public int fishFor1Up;
    public Timer timer;
    private bool hasWon;
    PlayerController playerController;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        fish = 0;
        fishThisLevel = 0;
        audioSource = GetComponent<AudioSource>();
        gui.UpdateFish(fish, fishFor1Up);
        gui.UpdateLives(lives);
        gui.UpdateScore(score);
    }
    public void FishPickup()
    {
        audioSource.PlayOneShot(pickupSound);
        fish++;

        fishThisLevel++;
        levelScore += fishValue;
        score += fishValue;
        gui.UpdateFish(fish, fishFor1Up);
        gui.UpdateScore(score);
        if(fish >= fishFor1Up)
        {
            lives++;
            print("1UP"); //1UP, sfx, etc
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

    void CalcScore ()
    {   if (!hasWon)
        {
            hasWon = true;
            timer.StopTimer();
            timeScore = (int)Math.Round(timer.timeRemaining, 2) * 1000;
            levelScore += timeScore;
            score += timeScore;
            gui.UpdateScore(score);
        }
    }
    public void EndStage()
    {
        CalcScore();
        gui.ShowStageClear(timeScore, fishThisLevel * fishValue, levelScore, score);
        playerController.FreezeControls();
    }

    public void StartStage()
    {
        gui.HideStageClear();
        timeScore = 0;
        fishThisLevel = 0;
        levelScore = 0;

        playerController.ResumeAtRespawn();
    }
}
