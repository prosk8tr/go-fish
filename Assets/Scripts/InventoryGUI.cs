using TMPro;
using UnityEngine;

public class InventoryGUI : MonoBehaviour
{
    void UpdateItemCount(int itemCount)
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = "x " + itemCount.ToString();
    }
    void UpdateItemCount(int[] args)
    {
        if (args.Length != 2) return;
        int itemCount = args[0];
        int limit = args[1];
        gameObject.GetComponent<TextMeshProUGUI>().text = "x " + itemCount.ToString() + " / " + limit.ToString();
    }
}
