using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float startTime;
    public float timeRemaining;
    public TextMeshProUGUI timeText;
    public bool isActive = false;
    Color defaultColour;

    void Awake()
    {
        defaultColour = timeText.color;
    }
    void Update()
    {
        if (isActive)
        {   if(timeRemaining <= 11)
            {
                timeText.color = new Color32(240, 33, 33, 255);
            }
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                timeText.text = timeRemaining.ToString("0.00");
            }
            else
            {
                timeRemaining = 0;
                timeText.text = "0.00";
                StopTimer();
            }
        }
    }

    // Sets the timer back to its start time and shows it, without starting it
    public void ResetTimer()
    {
        isActive = false;
        timeRemaining = startTime;
        timeText.color = defaultColour;
        timeText.text = timeRemaining.ToString("0.00");
        timeText.gameObject.SetActive(true);
    }

    public void StartTimer()
    {
        isActive = true;
    }

    public void StopTimer()
    {
        isActive = false;
    }
}
