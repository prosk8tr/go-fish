using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static int fish = 0;
    private void Start()
    {
        fish = 0;        
    }
    void fishPickup()
    {
        //Play sound
        fish++;
        //Update HUD
        print(fish);
        if(fish >= 10)
        {
            print("1UP"); //1UP, sfx, etc
        }
    }
}
