using TMPro;
using UnityEngine;

public class ChangeTextColour : MonoBehaviour
{
    void TurnRedBelow(int value, int threshold)
    {
        if (value < threshold)
        {
            gameObject.GetComponent<TextMeshProUGUI>().color = new Color();
        }
    }
}
