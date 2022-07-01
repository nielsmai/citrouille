using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    /// Variables
    public int playerHealth;
    public int numOfSouls;

    public Image[] souls;
    public Sprite fullSoul;
    public Sprite emptySoul;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth > numOfSouls){
            playerHealth = numOfSouls;
        }

        for (int i = 0; i < souls.Length; i++){

            if (i < playerHealth){
                souls[i].sprite = fullSoul;
            } else {
                souls[i].sprite = emptySoul;
            }

            if (i < numOfSouls){
                souls[i].enabled = true;
            } else {
                souls[i].enabled = false;
            }
        }
    }
}
