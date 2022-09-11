using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScarecrowController1 : MonoBehaviour
{
    public Punching player;

    private int healthPoints;

    Animator anim;

    private void Start(){
        healthPoints = 3;
        anim = GetComponent<Animator>();
    }

    private void Update(){
        if (healthPoints == 0){
            anim.SetTrigger("Dead");
            // Add 1 second timer
            Destroy(gameObject, 1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && player.isAttacking)
        {
            anim.SetTrigger("Hit");
            healthPoints--;
        }
    }
}
