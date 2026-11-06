using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float startTime;
    public float timeRemaining;
    public TextMeshProUGUI timeText;
    public bool isActive = false;

    void Start()
    {
        StartTimer();
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

    public void StartTimer()
    {
        timeRemaining = startTime;
        timeText.gameObject.SetActive(true);
        isActive = true;
    }

    public void StopTimer()
    {
        isActive = false;
    }
}
