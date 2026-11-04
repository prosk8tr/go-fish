using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int fish = 0;
    int score = 0;
    AudioSource audioSource;
    public AudioClip pickupSound;
    public int lives = 3;
    public TextMeshProUGUI fishGUI;
    public TextMeshProUGUI livesGUI;
    public TextMeshProUGUI scoreGUI;
    public int fishFor1Up;
  
    private void Start()
    {
        fish = 0;
        audioSource = GetComponent<AudioSource>();
        fishGUI.SendMessage("UpdateItemCount", new int[] { fish, fishFor1Up });
        livesGUI.SendMessage("UpdateItemCount", lives);
        scoreGUI.SendMessage("UpdateScore", score);
    }
    void fishPickup()
    {
        audioSource.PlayOneShot(pickupSound);
        fish++;
        score += 100;
        fishGUI.SendMessage("UpdateItemCount", new int[] { fish, fishFor1Up });
        scoreGUI.SendMessage("UpdateScore", score);
        if(fish >= fishFor1Up)
        {
            lives++;
            print("1UP"); //1UP, sfx, etc
            fish = 0; // Reset the counter -- collected fish already added to total score so nothing is lost
            fishGUI.SendMessage("UpdateItemCount", new int[] { fish, fishFor1Up });
            livesGUI.SendMessage("UpdateItemCount", lives);
        }
    }

    void LoseLife()
    {
        lives--;
        livesGUI.SendMessage("UpdateItemCount", lives);
        livesGUI.SendMessage("TurnRedBelow", new int[] { lives, 0 });
    }
}
