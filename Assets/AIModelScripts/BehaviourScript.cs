using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BehaviourScript : MonoBehaviour
{
    //Navigation Mesh for obstacle avoidance and Animator component
    public NavMeshAgent agent;
    public Animator anim;
    //Player Transform 
    public GameObject player;
    // Layermasks to identify ground and player
    public LayerMask whatIsPlayer;
    public LayerMask whatIsBout;
    public LayerMask backOfArena;
    // for general movement of ai
    public float distanceToMaintain;
    private float walkSpeed;
    private float distanceFromPlayer;
    private bool inBout;
    private bool returnToField = true;
    private float iTimerField;
    private bool backToArena;
    // Variables determining how the AI will attack
    public float attackRange;
    public float lungeRange; // to be measured with dummy arm
    private bool lungeLock = false;
    private bool alreadyAttacked = false;
    public float attackCooldownTime;
    private bool playerInAttackRange;
    private bool playerInLungeRange;

    // Determines whether dummy is pushed out of bounds or close to it
    void OnTriggerStay(Collider other)
    {
        
    }
    
    //Determines if the dummy is no longer out of bounds
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "AIBoutLimit") {
            returnToField = false;
            iTimerField = 0.5f;
            Debug.Log("return " + returnToField);
        }   
    }

    // Start is called before the first frame update
    // Instantiates components and identifies player, and gets the dummy's previous position
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Shoots a raycast to the ground and determines if the AI is still within bounds of the arena
        inBout = Physics.Raycast(transform.position + new Vector3 (0, 1, 0), Vector3.down, 5, whatIsBout);

        // Checks if player is within attack range, elsewise tries to maintain distance or move back to the middle of the arena
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        playerInLungeRange = Physics.CheckSphere(transform.position, lungeRange, whatIsPlayer);
        if (!lungeLock)
        {
            // Need condition for parry, possibly based on position of foil?
            if (!alreadyAttacked) 
            {
                if (playerInLungeRange && !lungeLock) {
                    Lunge();
                    Debug.Log("Lunge Called");
                }
                else if (playerInAttackRange && !playerInLungeRange) {
                    ChaseIntoAttack();
                }
            }
            
            // Conditional statements to determine whether the AI will move into the middle of the field or if it will attempt to follow the player
            if (!playerInAttackRange && inBout && returnToField) {
                MaintainDistance();
            }
            else if (!playerInAttackRange && inBout && !returnToField) {
                MoveBack();
            }
            Debug.DrawRay(transform.position + new Vector3 (0, 1, 0), Vector3.down, Color.blue, 0.1f);
        }
    }

    private void MaintainDistance() 
    {
        // Calculates the walk speed of the dummy based on distance. Will also ensure the walkspeed is always at least 1 for moving on the Z axis
        distanceFromPlayer = Vector3.Distance(transform.position, player.transform.position);
        walkSpeed = Mathf.Round(2 * Mathf.Pow(distanceFromPlayer - distanceToMaintain, 2));
        if (walkSpeed <= 0) walkSpeed = 1;
        
        // Moves dummy based upon player's position. Will also lock it's Y axis movement to 0 so it's always grounded
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position - new Vector3 (distanceToMaintain, 0, 0), walkSpeed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }
    private void MoveBack()
    {
        // Shoots a raycast backwards to see if it has hit the back of the arena
        if (iTimerField >= 0.5f) backToArena = Physics.Raycast(transform.position, Vector3.left, 0.75f, backOfArena);
        Debug.DrawRay(transform.position, Vector3.left, Color.red, 0.5f);

        // Decides whether it has to move to the middle or to the middle and away from the back of the arena
        if (backToArena)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3 (player.transform.position.x - 0.5f, 0, 0), 2 * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3 (player.transform.position.x - distanceToMaintain, 0, 0), 2 * Time.deltaTime);
        }

        // Timer field is to determine how long the AI will move away from the edge of the arena. 
        // This will also wait for the next updated frame to iterate once again
        // Returns boolean to true once the time limit has run out. This ensures that the model is away from any edges before it starts moving again.
        iTimerField -= Time.deltaTime;
        new WaitForFixedUpdate();
        if (iTimerField <= 0) {
            returnToField = true;
        }
    }
    // Glorified move towards but speedy
    private void ChaseIntoAttack() {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position - new Vector3 (0.5f, 0, 0), 3 * Time.deltaTime);
        transform.position = new Vector3 (transform.position.x, 0, transform.position.z);
    }
    // need to wrap hitbox around animation component
    private void Lunge() {
        anim.SetBool("withinLungeRange", true);
        lungeLock = true;
        alreadyAttacked = true;
        // Get animation state: if done start coroutine, if not wait
        // can afford stopping THE ENTIRE SCRIPT (i think) if animation is still playing
        // find variable speed for animation
        StartCoroutine(AttackCooldown(attackCooldownTime));
    }
    private void Unlock() {
        anim.SetBool("withinLungeRange", false);
        lungeLock = false;
        Debug.Log("Unlock called");
    }
    // i have no fucking clue how to do this ((:
    private void Parry() {

    }

    private IEnumerator AttackCooldown(float timer)
    {
        while (timer > 0) {
            timer -= Time.deltaTime;
            Debug.Log(timer);
            yield return new WaitForEndOfFrame();
        }
        alreadyAttacked = false;
    }
}