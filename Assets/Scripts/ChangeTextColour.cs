using TMPro;
using UnityEngine;

public class ChangeTextColour : MonoBehaviour
{
    void TurnRedBelow(int[] args)
    {
        if (args.Length != 2) return;
        int value = args[0];
        int threshold = args[1];

        if (value <= threshold)
        {
            gameObject.GetComponent<TextMeshProUGUI>().color = new Color32(240, 33, 33, 255);
        }
        else
        {
            gameObject.GetComponent<TextMeshProUGUI>().color = Color.white;
        }
    }
}
