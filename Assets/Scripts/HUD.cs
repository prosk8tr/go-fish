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
    public Camera gameplayCamera;
    public Camera aerialCamera;
    public GameObject fadeToBlackPanel;
    public float previewSeconds = 7f;
    public float fadeSeconds = 0.5f;
    public GameObject[] hudElements; // everything hidden during the stage preview
    public TextMeshProUGUI stageTitle;
    public TextMeshProUGUI stageTitleScore;
    private Color32 LowLivesColour = new Color32(240, 33, 33, 255);
    public Material LowLivesMaterial;
    public AudioClip highScoreSound;

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
        yield return new WaitForSeconds(0.7f);

        totalScoreText.text = "Total score: " + totalScore;
        totalScoreText.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.7f);

        if (isHighScore)
        {
            stageHighScoreText.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.7f);
        }

        nextButton.gameObject.SetActive(true);
    }

    public void SetHudVisible(bool visible)
    {
        foreach (GameObject element in hudElements)
            element.SetActive(visible);
    }

    public void UpdateStageTitle(int stage, int score)
    {
        stageTitle.text = "Stage " + stage;
        stageTitleScore.text = "High Score: " + (score > 0 ? score : "---");
    }

    public IEnumerator StagePreview()
    {
        AerialCameraController aerialCameraController = aerialCamera.GetComponent<AerialCameraController>();

        stageTitle.gameObject.SetActive(true);
        SetHudVisible(false);
        gameplayCamera.enabled = false;
        aerialCameraController.ResetCamera();
        aerialCamera.enabled = true;
        aerialCameraController.isActive = true;
        yield return new WaitForSeconds(previewSeconds);

        yield return Fade(0f, 1f);
        aerialCameraController.isActive = false;
        aerialCamera.enabled = false;
        gameplayCamera.enabled = true;
        yield return new WaitForSeconds(0.5f);
        stageTitle.gameObject.SetActive(false);
        SetHudVisible(true);
        yield return Fade(1f, 0f);
    }

    IEnumerator Fade(float from, float to)
    {
        Image panel = fadeToBlackPanel.GetComponent<Image>();
        fadeToBlackPanel.SetActive(true);
        for (float t = 0f; t < fadeSeconds; t += Time.deltaTime)
        {
            Color c = panel.color;
            c.a = Mathf.Lerp(from, to, t / fadeSeconds);
            panel.color = c;
            yield return null;
        }
        Color end = panel.color;
        end.a = to;
        panel.color = end;
        fadeToBlackPanel.SetActive(to > 0f);
    }
}
