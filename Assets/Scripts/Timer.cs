using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float startTime;
    public float timeRemaining;
    public TextMeshProUGUI timeText;
    public bool isActive = false;
    Color defaultColour;
    public float turnRedBelow;
    public Material LowTimeMaterial;

    void Awake()
    {
        defaultColour = timeText.color;
    }
    void Update()
    {
        if (isActive)
        {   if(timeRemaining < turnRedBelow)
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
