using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryGUI : MonoBehaviour
{
    private static readonly WaitForSeconds _waitForSeconds0_7 = new WaitForSeconds(0.7f);

    public TextMeshProUGUI fishText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI scoreText;
    public GameObject scorePanel;
    public TextMeshProUGUI stageClearText;
    public TextMeshProUGUI timeScoreText;
    public TextMeshProUGUI fishThisLevelText;
    public TextMeshProUGUI levelScoreText;
    public TextMeshProUGUI totalScoreText;
    public Button nextButton;

    private static readonly Color32 LowLivesColour = new Color32(240, 33, 33, 255);

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

    public void HideStageClear()
    {
        StopAllCoroutines();
        scorePanel.SetActive(false);
        stageClearText.gameObject.SetActive(false);
        timeScoreText.gameObject.SetActive(false);
        fishThisLevelText.gameObject.SetActive(false);
        levelScoreText.gameObject.SetActive(false);
        totalScoreText.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(false);
    }

    public void ShowStageClear(int timeScore, int fishScore, int levelScore, int totalScore)
    {
        StopAllCoroutines();
        StartCoroutine(ShowStageClearRoutine(timeScore, fishScore, levelScore, totalScore));
    }

    private IEnumerator ShowStageClearRoutine(int timeScore, int fishScore, int levelScore, int totalScore)
    {
        scorePanel.SetActive(true);
        stageClearText.gameObject.SetActive(true);
        yield return _waitForSeconds0_7;

        timeScoreText.text = "Time score: " + timeScore;
        timeScoreText.gameObject.SetActive(true);
        yield return _waitForSeconds0_7;

        fishThisLevelText.text = "Fish score: " + fishScore;
        fishThisLevelText.gameObject.SetActive(true);
        yield return _waitForSeconds0_7;

        levelScoreText.text = "Level score: " + levelScore;
        levelScoreText.gameObject.SetActive(true);
        yield return _waitForSeconds0_7;

        totalScoreText.text = "Total score: " + totalScore;
        totalScoreText.gameObject.SetActive(true);
        yield return _waitForSeconds0_7;

        nextButton.gameObject.SetActive(true);
    }
}
