using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static int fish = 0;
    AudioSource audioSource;
    public AudioClip pickupSound;
    public int lives = 3;
  
    private void Start()
    {
        fish = 0;
        audioSource = GetComponent<AudioSource>();
    }
    void fishPickup()
    {
        audioSource.PlayOneShot(pickupSound);
        fish++;
        //Update HUD
        if(fish >= 10)
        {
            lives++;
            print("1UP"); //1UP, sfx, etc
        }
    }

    void LoseLife()
    {
        lives--;
    }
}
