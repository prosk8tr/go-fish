using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private static WaitForSeconds _waitForSeconds0_7 = new WaitForSeconds(0.7f);
    private static WaitForSeconds _waitForSeconds0_5 = new WaitForSeconds(0.5f);
    public int fish = 0;
    public int fishValue = 100;
    int fishThisLevel = 0;
    int timeScore = 0;
    int levelScore = 0;
    int score = 0;
    AudioSource audioSource;
    public AudioClip pickupSound;
    public int lives = 3;
    public TextMeshProUGUI fishGUI;
    public TextMeshProUGUI livesGUI;
    public TextMeshProUGUI scoreGUI;
    public int fishFor1Up;
    public Timer timer;
    private bool hasWon;
    public Rigidbody player;
    PlayerController playerMovement;
    public TextMeshProUGUI stageClearText;
    public CameraController cameraController;
    public TextMeshProUGUI timeScoreText;
    public TextMeshProUGUI fishThisLevelText;
    public TextMeshProUGUI levelScoreText;
    public TextMeshProUGUI totalScoreText;
    public GameObject scorePanel;

    public Button nextButton;

    private void Start()
    {
        playerMovement = player.GetComponent<PlayerController>();
        fish = 0;
        fishThisLevel = 0;
        audioSource = GetComponent<AudioSource>();
        fishGUI.SendMessage("UpdateItemCount", new int[] { fish, fishFor1Up });
        livesGUI.SendMessage("UpdateItemCount", lives);
    }
    void FishPickup()
    {
        audioSource.PlayOneShot(pickupSound);
        fish++;
        fishThisLevel++;
        levelScore += fishValue;
        score += fishValue;
        fishGUI.SendMessage("UpdateItemCount", new int[] { fish, fishFor1Up });
        scoreGUI.SendMessage("UpdateScore", score);
        if(fish >= fishFor1Up)
        {
            lives++;
            print("1UP"); //1UP, sfx, etc
            fish = 0; // Reset the counter -- collected fish already added to total score so nothing is lost
            fishGUI.SendMessage("UpdateItemCount", new int[] { fish, fishFor1Up });
            livesGUI.SendMessage("UpdateItemCount", lives);
        }
    }

    void LoseLife()
    {
        lives--;
        livesGUI.SendMessage("UpdateItemCount", lives);
        livesGUI.SendMessage("TurnRedBelow", new int[] { lives, 0 });
    }

    void CalcScore ()
    {   if (!hasWon)
        {
            hasWon = true;
            timer.SendMessage("StopTimer");
            timeScore = (int)Math.Round(timer.timeRemaining, 2) * 1000;
            levelScore += timeScore;
            score += timeScore;
            scoreGUI.SendMessage("UpdateScore", score);
        }
    }
    void EndStage()
    {
        StartCoroutine("ShowScoreText");
        CalcScore();
        playerMovement.enabled = false;
        cameraController.GetComponent<TiltInput>().tilt = new UnityEngine.Vector2(0f, 0f);
        cameraController.enabled = false;
    }

    void StartStage()
    {
        scorePanel.gameObject.SetActive(false);
        stageClearText.gameObject.SetActive(false);
        timeScoreText.gameObject.SetActive(false);
        timeScore = 0;
        fishThisLevelText.gameObject.SetActive(false);
        fishThisLevel = 0;
        levelScoreText.gameObject.SetActive(false);
        levelScore = 0;
        totalScoreText.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(false);


        player.transform.position = playerMovement.RespawnPosition;
        cameraController.enabled = true;
        playerMovement.enabled = true;
    }

    private IEnumerator ShowScoreText()
    {
        scorePanel.gameObject.SetActive(true);
        stageClearText.gameObject.SetActive(true);
        yield return _waitForSeconds0_7;

        timeScoreText.text = "Time score: " + timeScore;
        timeScoreText.gameObject.SetActive(true);
        yield return _waitForSeconds0_7;

        fishThisLevelText.text = "Fish score: " + fishThisLevel * 100;
        fishThisLevelText.gameObject.SetActive(true);
        yield return _waitForSeconds0_7;

        levelScoreText.text = "Level score: " + levelScore;
        levelScoreText.gameObject.SetActive(true);
        yield return _waitForSeconds0_7;

        totalScoreText.text = "Total score: " + score;
        totalScoreText.gameObject.SetActive(true);
        yield return _waitForSeconds0_7;

        nextButton.gameObject.SetActive(true);
    }
}
