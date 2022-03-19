using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    public enum State
    {
        Idle,
        Run,
        Fight
    }

    public State currentState = State.Idle;

    public Character character;
    public Fight fight;

    [Header("Attack Settings")]
    public float attackDistance = 4f;
    public float attackRate = 1f;
    public float currentAttackTime = 0f;
    [Header("Movement Settings")]
    //public float runDistance = Mathf.Infinity;
    public float turnSpeed = 10f;
    public float moveSpeed = 3f;

    public Transform target;

    public float distanceToTarget;


    private Rigidbody rigidbody;



    private void Awake()
    {
        character = GetComponent<Character>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rigidbody = GetComponent<Rigidbody>();
        fight = GetComponent<Fight>();
    }
    private void Start()
    {
        character.agent.stoppingDistance = attackDistance;
        rigidbody.isKinematic = true;
    }
    private void Update()
    {
        if (GameManager.Instance.currentState == GameManager.GameState.Fight)
        {
            SelectState();
            ExecuteState();
        }
    }

    private void SelectState()
    {
        if (target != null)
        {
            distanceToTarget = Vector3.Distance(transform.position, target.position); 
        }

        if (distanceToTarget > attackDistance)
        {
            currentState = State.Run;
        }
        else if (distanceToTarget <= attackDistance)
        {
            currentState = State.Fight;
        }
       
    }
    private void ExecuteState()
    {
        if (character.agent == null)
        {
            print("Agent Null");
            return;
        }

        switch (currentState)
        {
            case State.Idle:
                IdleState();
                break;
            case State.Run:
                RunState();
                break;
            case State.Fight:
                FightState();
                break;
            default:
                break;
        }
    }

    private void FightState()
    {
        if (target != null)
        {
            character.agent.isStopped = true;
            character.anim.SetBool("FightStarted", true);
            character.anim.SetBool("GameStarted", false);
            character.agent.speed = moveSpeed;
            //animasyon
            LookAtTheTarget(target.position);
            if (currentAttackTime >= attackRate)
            {
                if (!character.anim.IsInTransition(0) && character.anim.GetCurrentAnimatorStateInfo(0).IsName("FightIdle"))
                {
                    print("Attack");
                    character.anim.SetTrigger(fight.GetRandomAttackAnimation());
                    currentAttackTime = 0f;
                }
            }
            else
            {
                currentAttackTime += Time.deltaTime;
            }
        }
    }

    private void RunState()
    {
        //düþmana bakacaðýz
        //koþma animasyonu
        LookAtTheTarget(target.position);
        character.agent.isStopped = false;
        character.agent.speed = moveSpeed * 2;
        character.agent.SetDestination(target.position);
        character.anim.SetBool("FightStarted", false);
        character.anim.SetBool("GameStarted", true);
    }

    private void IdleState()
    {
        character.agent.isStopped = true;
    }

    private void LookAtTheTarget(Vector3 newTarget)
    {
        //transform.LookAt(new Vector3(target.position.x,
        //    transform.position.y,
        //    target.position.z));

        Vector3 targetLookPosition = new Vector3(newTarget.x, transform.position.y, newTarget.z);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetLookPosition - transform.position), 
            turnSpeed * Time.deltaTime);
    }


}
