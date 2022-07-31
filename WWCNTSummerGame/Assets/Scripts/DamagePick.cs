using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePick : MonoBehaviour
{
    private CollisionDetector collide;
    private Item pumpkin;
    public int healthPoints = 2;
    private int damage = 1;

    // Update is called once per frame
    void Update()
    {
        if (healthPoints == 0)
        {
            PickUp();
        }
    }

    public void deleteHealth()
    {
        healthPoints -= damage;
    }

    private void PickUp() // TODO
    {
        //print("Picking up " + pumpkin.name);
        bool wasPickedUp = Inventory.instance.Add(pumpkin);
        if (wasPickedUp)
        {
            Destroy(gameObject);
        }
    }
}
