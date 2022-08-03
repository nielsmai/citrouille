
using UnityEngine;

public class PumpkinPickup : Interactable
{
    public override void Interact()
    {
        base.Interact();

        PickUp();
    }

    private void PickUp () {
        Debug.Log("Picking up ");
        //Call pickUpPumpkin() in PlayerController.cs
        bool wasPickedUp = PlayerController.pickUpPumpkin();
        if (wasPickedUp) {
            Destroy(gameObject);
        }
    }
}
