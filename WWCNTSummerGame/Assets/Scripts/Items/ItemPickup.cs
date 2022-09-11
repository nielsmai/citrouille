
using UnityEngine;

public class ItemPickup : Interactable
{
    public Item item;
    public GameObject player;
    public override void Interact()
    {
        base.Interact();

        PickUp();
    }

    private void PickUp () {
        Debug.Log("Picking up " + item.name);
        bool wasPickedUp = Inventory.instance.Add(item);
        if (wasPickedUp) {
            Animator anim = player.GetComponent<Animator>();
            anim.SetTrigger("Picking");
            Destroy(gameObject);
        }
    }
}
