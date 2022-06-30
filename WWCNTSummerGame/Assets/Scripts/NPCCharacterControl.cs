using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICharacterController : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent { get; private set; } // Navmesh agent required for path finding
    public ThirdPersonCharacter character { get; private set; } // Character we are controlling
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        // Get the components on the object we need (should not be null due to required component so no need to check)
        agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
        character = GetComponent<ThirdPersonCharacter>();

        agent.updateRotation = false;
        agent.updatePosition = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null){
            agent.SetDestination(target.position);

            if (agent.remaningDistance < 1.5f){
                GetComponent<Animator>().SetTrigger("Attack");
            }
        }

        if (agent.remainingDistance > agent.stopppingDistance){
            character.Move(agent.desiredVelocity, false, false);
        } else {
            character.Move(Vector3.zero, false, false);
        }
    }

    public void SetTarget(Transform target){
        this.target = target;
    }
}
