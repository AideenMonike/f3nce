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
    private Vector3 playerPrevPosition;
    public float distanceToMaintain;
    private float walkSpeed;
    private float walkDirection;
    private float distanceFromPlayer;
    private float vectorAngles;
    private bool inBout;
    //Variables determining how the AI will attack
    public float attackRange;
    bool alreadyAttacked;
    public bool playerInAttackRange;

    void OnCollisionEnter(Collision other)
    {
        if (gameObject.tag == "boutBoundary") inBout = true;
        else inBout = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Capsule").transform;
        agent = GetComponent<NavMeshAgent>();
        playerPrevPosition = player.position;
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

    private void MaintainDistance() 
    {
        distanceFromPlayer = Vector3.Distance(transform.position, player.position);
        walkSpeed = 2 * Mathf.Pow(distanceFromPlayer - 3, 2) + 0.5f;
        Debug.Log(walkSpeed);
        transform.position = Vector3.MoveTowards(transform.position, player.position - new Vector3 (5, 0, 0), walkSpeed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        /*
        distanceFromPlayer = transform.position.x - player.position.x;
        
        vectorAngles = Vector3.Angle(transform.position, player.position);

        walkDirection = player.position.x - playerPrevPosition.x;
        if (walkDirection >= 0) {
            walkDirection = 1;
        }
        else {
            walkDirection = -1;
        }
        switch (walkDirection)
        {
            case -1:
                walkSpeed = Mathf.Abs((distanceFromPlayer * -2) + 5);
                break;
            case 1:
                walkSpeed = Mathf.Abs((distanceFromPlayer * 2) - 5);
                break;
        }
        
        transform.position = transform.position + new Vector3(walkDirection * walkSpeed * Time.deltaTime, 0, 0);

        if ((175 <= vectorAngles && vectorAngles <= 185 || 5 <= vectorAngles && vectorAngles <= 15) && inBout) {
            transform.position = transform.position + new Vector3 (0, 0, walkSpeed * Time.deltaTime);
        }
        */
    }
    private void ChaseIntoAttack() {
        
    }
    private void Lunge() {

    }
}


