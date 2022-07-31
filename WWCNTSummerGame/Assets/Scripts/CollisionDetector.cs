using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    public Combat combat;
    public GameObject pumpkin;
    public DamagePick dommage;

    private void OnTriggerEnter(Collider other)
    {
        if (/*other.tag == "DamagePick" &&*/ combat.isAttacking)
        {
            Animator anim = pumpkin.GetComponent<Animator>();
            anim.SetTrigger("Hit");
            dommage.deleteHealth();
        }
    }

}
