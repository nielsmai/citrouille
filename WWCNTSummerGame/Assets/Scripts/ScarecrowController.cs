using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScarecrowController : MonoBehaviour
{
    public Punching fist;
    public GameObject scarecrow;

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
        if (other.tag == "Weapon" && fist.isAttacking)
        {
            Animator anim = scarecrow.GetComponent<Animator>();
            anim.SetTrigger("hit");
            healthPoints--;
        }
    }
}
