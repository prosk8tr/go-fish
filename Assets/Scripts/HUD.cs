using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{

    public TextMeshProUGUI fishText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI scoreText;
    public GameObject scorePanel;
    public TextMeshProUGUI stageClearText;
    public TextMeshProUGUI timeScoreText;
    public TextMeshProUGUI fishThisLevelText;
    public TextMeshProUGUI stageScoreText;
    public TextMeshProUGUI stageHighScoreText;
    public TextMeshProUGUI totalScoreText;
    public Button nextButton;
    public TextMeshProUGUI countdownText;
    public Material[] countdownMaterials; // one per countdown step, in order, with the final one used for "GO!"
    public Color countdownColour = Color.white;
    public Color goColour = Color.green;
    public TextMeshProUGUI timeOutText;
    public TextMeshProUGUI retryText; 

    private Color32 LowLivesColour = new Color32(240, 33, 33, 255);
    public Material LowLivesMaterial;

    public void UpdateFish(int fish, int fishFor1Up)
    {
        fishText.text = "x " + fish + " / " + fishFor1Up;
    }

    public void UpdateLives(int lives)
    {
        livesText.text = "x " + lives;
        livesText.color = lives <= 0 ? LowLivesColour : Color.white;
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }

    public void ShowCountdown(string text, int step, bool isGo = false)
    {
        if (countdownMaterials.Length > 0)
        {
            countdownText.fontSharedMaterial = countdownMaterials[Mathf.Clamp(step, 0, countdownMaterials.Length - 1)];
        }
        countdownText.color = isGo ? goColour : countdownColour;
        countdownText.text = text;
        countdownText.gameObject.SetActive(true);
    }

    public void HideCountdown()
    {
        countdownText.gameObject.SetActive(false);
    }

    public void ShowTimeOut()
    {
        timeOutText.gameObject.SetActive(true);
    }

    public void HideTimeOut()
    {
        timeOutText.gameObject.SetActive(false);
        retryText.gameObject.SetActive(false);
    }

    public void ShowRetryCountdown(int seconds)
    {
        retryText.text = "Retry in " + seconds + "...";
        retryText.gameObject.SetActive(true);
    }

    public void ShowGameOver()
    {
        retryText.text = "Game over!";
        retryText.gameObject.SetActive(true);
    }

    public void HideStageClear()
    {
        StopAllCoroutines();
        scorePanel.SetActive(false);
        stageClearText.gameObject.SetActive(false);
        timeScoreText.gameObject.SetActive(false);
        fishThisLevelText.gameObject.SetActive(false);
        stageScoreText.gameObject.SetActive(false);
        stageHighScoreText.gameObject.SetActive(false);
        totalScoreText.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(false);
    }

    public void ShowStageClear(int timeScore, int fishScore, int stageScore, int totalScore, bool isHighScore)
    {
        StopAllCoroutines();
        StartCoroutine(ShowStageClearRoutine(timeScore, fishScore, stageScore, totalScore, isHighScore));
    }

    private IEnumerator ShowStageClearRoutine(int timeScore, int fishScore, int stageScore, int totalScore, bool isHighScore)
    {
        scorePanel.SetActive(true);
        stageClearText.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.7f);

        timeScoreText.text = "Time score: " + timeScore;
        timeScoreText.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.7f);

        fishThisLevelText.text = "Fish score: " + fishScore;
        fishThisLevelText.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.7f);

        stageScoreText.text = "Stage score: " + stageScore;
        stageScoreText.gameObject.SetActive(true);
        if (isHighScore)
        {
            stageHighScoreText.gameObject.SetActive(true);
        }
        yield return new WaitForSeconds(0.7f);

        totalScoreText.text = "Total score: " + totalScore;
        totalScoreText.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.7f);

        nextButton.gameObject.SetActive(true);
    }
}
