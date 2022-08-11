
using UnityEngine;

public class PumpkinPickup : Interactable
{
    public GameObject player;
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
            Animator anim = player.GetComponent<Animator>();
            anim.SetTrigger("Picking");
            Destroy(gameObject);
        }
    }
}
