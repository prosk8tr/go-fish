using TMPro;
using UnityEngine;

public class ScoreGUI : MonoBehaviour
{
    void UpdateScore(int itemCount)
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = itemCount.ToString();
    }
}
