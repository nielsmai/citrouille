using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScarecrowController1 : MonoBehaviour
{
    public Punching player;

    private int healthPoints;

    private void Start(){
        healthPoints = 3;
    }

    private void Update(){
        if (healthPoints == 0){
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && player.isAttacking)
        {
            Animator anim = GetComponent<Animator>();
            anim.SetTrigger("Hit");
            healthPoints--;
        }
    }
}
