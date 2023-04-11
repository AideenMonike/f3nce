using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BehaviourScript : MonoBehaviour
{
    //Navigation Mesh for obstacle avoidance
    public NavMeshAgent agent;
    //Player Transform 
    public Transform player;
    //Layermasks to identify ground and player
    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;
    //for general movement of ai
    public Vector3 walkPoint;
    bool walkPointSet;
    private float walkSpeed;
    private float walkDirection;
    private float distanceFromPlayer;
    private float absDot;
    private float dotDistance;
    private Vector3 crossDistance;
    //Variables determining how the AI will attack
    public float attackRange;
    bool alreadyAttacked;
    public bool playerInAttackRange;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInAttackRange)
        {
            MaintainDistance();
        }
    }

    private void MaintainDistance() {
        distanceFromPlayer = Vector3.Distance(transform.position, player.position);
        walkSpeed = distanceFromPlayer / 10;
        //calculates whether model is parallel to player, restrained by bout boundaries
        if (Mathf.Approximately(Vector3.Angle(transform.position, player.position), 0 | 180))
        {

        }
        transform.position = transform.position + new Vector3(walkDirection * walkSpeed * Time.deltaTime, 0, 0);
    }
    private void ChaseIntoAttack() {
        
    }
    private void Lunge() {

    }
}
