using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public GameObject fist;
    public bool canAttack = true;
    public float attackCooldown = 1.0f;

    public bool isAttacking = false;

    void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            if (canAttack)
            {
                punch();
            }
        }
    }

    public void punch()
    {
        isAttacking = true;
        canAttack = false;
        Animator anim = fist.GetComponent<Animator>();
        anim.SetTrigger("Attack");
        StartCoroutine(ResetAttackCooldown());
    }

    IEnumerator ResetAttackCooldown()
    {
        StartCoroutine(ResetAttackBool());
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    IEnumerator ResetAttackBool()
    {
        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
    }
}
