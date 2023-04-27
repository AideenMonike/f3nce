﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BehaviourScript : MonoBehaviour
{
    //Navigation Mesh for obstacle avoidance and Animator component
    public NavMeshAgent agent;
    public Animator anim;
    //Player Transform 
    public Transform player;
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
    private float lungeRange; // to be measured with dummy arm
    private bool alreadyAttacked;
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
            iTimerField = 1f;
            Debug.Log("return " + returnToField);
        }   
    }

    // Start is called before the first frame update
    // Instantiates components and identifies player, and gets the dummy's previous position
    void Start()
    {
        player = GameObject.Find("Capsule").transform;
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

        // Need condition for parry, possibly based on position of foil?
        if (!alreadyAttacked) 
        {
            if (playerInLungeRange) {
                Lunge();
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
        Debug.DrawRay(transform.position + new Vector3 (0, 1, 0), Vector3.down, Color.blue, 0.5f);
    }

    private void MaintainDistance() 
    {
        // Calculates the walk speed of the dummy based on distance. Will also ensure the walkspeed is always at least 1 for moving on the Z axis
        distanceFromPlayer = Vector3.Distance(transform.position, player.position);
        walkSpeed = Mathf.Round(2 * Mathf.Pow(distanceFromPlayer - distanceToMaintain, 2));
        if (walkSpeed <= 0) walkSpeed = 1;
        
        // Moves dummy based upon player's position. Will also lock it's Y axis movement to 0 so it's always grounded
        transform.position = Vector3.MoveTowards(transform.position, player.position - new Vector3 (distanceToMaintain, 0, 0), walkSpeed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }
    private void MoveBack()
    {
        // Shoots a raycast backwards to see if it has hit the back of the arena
        backToArena = Physics.Raycast(transform.position, Vector3.left, 0.75f, backOfArena);
        Debug.DrawRay(transform.position, Vector3.left, Color.red, 0.5f);

        // Decides whether it has to move to the middle or to the middle and away from the back of the arena
        if (backToArena)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3 (player.position.x - 0.5f, 0, 0), 3 * Time.deltaTime);
            Debug.Log("raycast hit back of arena");
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3 (player.position.x - distanceToMaintain, 0, 0), 2 * Time.deltaTime);
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
        
    }
    // need to wrap hitbox around animation component
    private void Lunge() {
        
    }
    // i have no fucking clue how to do this ((:
    private void Parry() {

    }

    private IEnumerator AttackCooldown()
    {
        while (attackCooldownTime > 0) attackCooldownTime -= Time.deltaTime;
        alreadyAttacked = false;
        yield return null;
    }
}