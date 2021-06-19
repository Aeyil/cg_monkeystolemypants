using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{

    NavMeshAgent nm;
    public Transform target;

    public float distanceThreshhold = 10f;

    public enum AIState { idle, chasing};

    public AIState aiState = AIState.idle;

    public Animator animator;
    

    // Start is called before the first frame update
    void Start()
    {
        nm = GetComponent<NavMeshAgent>();
        StartCoroutine(Think());
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Think() {

        while (true) {
            switch (aiState)
            {
                case AIState.idle:
                    float dist = Vector3.Distance(target.position, transform.position);
                    if (dist < distanceThreshhold)
                    {
                        aiState = AIState.chasing;
                        animator.SetBool("Chasing", true);
                    }
                    nm.SetDestination(transform.position);
                    break;
                case AIState.chasing:
                    dist = Vector3.Distance(target.position, transform.position);
                    if (dist > distanceThreshhold)
                    {
                        aiState = AIState.idle;
                        animator.SetBool("Chasing", false);
                    }
                    nm.SetDestination(target.position);
                    break;
                default:
                    break;
            }
            
            yield return new WaitForSeconds(1.0f);
        }
    }
}
