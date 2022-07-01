using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcController : MonoBehaviour
{
    /// Variables

    // Character variables
    public NavMeshAgent npc;
    public Transform sammy;

    // Layer variables
    public LayerMask whatIsGround, whatIsSammy;

    // Patroling variables
    public Vector3 walkPoint;
    private bool walkPointSet;
    public float walkPointRange;

    // Attacking variables
    public float timeBetweenAttacks;
    private bool alreadyAttacked;

    // Range variables
    public float sightRange, attackRange;

    // State variables
    public bool sammyInSightRange, sammyInAttackRange;

    // Bear variables: to move to BearController
    public float health;

    /// Unity basic functions
    private void Awake(){
        
        // Initialize variables, i.e. sammy and npc
        sammy = GameObject.Find("Player").transform;
        npc = GetComponent<NavMeshAgent>();

    }

    private void Update(){
        
        // Check for sight and attack range
        sammyInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsSammy);
        sammyInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsSammy);
        
        // Basic 3 states
        if (!sammyInSightRange && !sammyInAttackRange) Patroling();
        if (sammyInSightRange && !sammyInAttackRange) Chasing();
        if (sammyInSightRange && sammyInAttackRange) Attacking();

    }

    /// NPC 3 basic actions
    private void Patroling(){

        // Find random point to go
        if (!walkPointSet){
            SearchWalkPoint();
        } else {
            npc.SetDestination(walkPoint);
        }

        // Resetting walkPoint flag to false after destination reached
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if (distanceToWalkPoint.magnitude < 1f) walkPointSet = false;

    }

    private void Chasing(){

        npc.SetDestination(sammy.position);

    }

    private void Attacking(){

        // Stop npc movement
        npc.SetDestination(transform.position);

        // Make npc look at sammy :) hihi
        transform.LookAt(sammy);

        // Set attack flag to true
        if (!alreadyAttacked) {
            
            // Attacking method
            // Depends on npc
            // To be updated
            // Code here...

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);

        }
    }

    /// Other possible actions

    // Monkey: to move to MonkeyController
    private void DropSanity(){

        // Sanity rate goes down
        // When down to 0 --> Crazy mode on
        Crazy();
    }

    private void Purified(){

        // Sanity rate goes up
        // When up to 3 --> Sane mode on
        Sane();
    }

    private void Sane(){

    }

    private void Crazy(){

    }

    // Bear: to move to BearController
    private void Stunned(){

        // Stop moving for a few seconds
    }

    private void TakeDamage(int damage){

        health -= damage;

        //To be updated
    }

    /// Other tools
    private void SearchWalkPoint(){
        
        // Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        // Check if random point is in map
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround)) walkPointSet = true;

    }

    private void ResetAttack(){

        alreadyAttacked = false;

    }

    // To be updated
    private void KillNpc(){

    }
}
