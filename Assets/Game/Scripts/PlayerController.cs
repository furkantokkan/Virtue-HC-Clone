using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator anim;
    private void Awake()
    {
        anim = GetComponent<Character>().anim;
    }
    void Start()
    {
        InputManager.Instance.onTouchStart += ProcessPlayerSwere;
        InputManager.Instance.onTouchMove += ProcessPlayerSwere;
    }

    private void OnDisable()
    {
        InputManager.Instance.onTouchStart -= ProcessPlayerSwere;
        InputManager.Instance.onTouchMove -= ProcessPlayerSwere;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessPlayerForwardMovement();

        PlayAnimations();
    }

    private void PlayAnimations()
    {
        anim.SetBool("GameStarted", GameManager.Instance.currentState == GameManager.GameState.Normal);
        anim.SetBool("FightStarted", GameManager.Instance.currentState == GameManager.GameState.Fight);
    }

    private void ProcessPlayerForwardMovement()
    {
        if (GameManager.Instance.currentState == GameManager.GameState.Normal)
        {
            GetComponent<Mover>().MoveTo(new Vector3(
                0f,
                0f,
                GameManager.Instance.forwardSpeed));
        }
    }

    private void ProcessPlayerSwere()
    {
        if (GameManager.Instance.currentState == GameManager.GameState.Normal)
        {
            GetComponent<Mover>().MoveTo(new Vector3(
                InputManager.Instance.GetDirection().x * GameManager.Instance.horizontalSpeed, 0f, 0f));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Character>() != null)
        {
            Character targetCharacter = other.GetComponent<Character>();
            Character currentCharacter = GetComponent<Character>();

            if (targetCharacter.currentCharacterID == Character.CharacterID.Stack &&
                targetCharacter.currentMaterial.name == currentCharacter.currentMaterial.name)
            {
                print("Same material");
                int currentAmount = currentCharacter.characterSize;
                currentAmount++;
                currentCharacter.characterSize = currentAmount;
                GameManager.Instance.onCharacterTake?.Invoke(currentAmount);
                OnRightTake();
                GameManager.Instance.onRightCharacterTake?.Invoke();
                Destroy(other.gameObject);
            }
            else if (targetCharacter.currentCharacterID == Character.CharacterID.Stack &&
                targetCharacter.currentMaterial.name != currentCharacter.currentMaterial.name)
            {
                int currentAmount = currentCharacter.characterSize;
                currentCharacter.characterSize = currentAmount;
                GameManager.Instance.onCharacterTake(currentAmount);
                GameManager.Instance.onWrongCharacterTake?.Invoke();
                OnWrongTake();
                Destroy(other.gameObject);
                print("Not same material");
            }
        }


    }
    private void OnRightTake()
    {
        transform.localScale = new Vector3(transform.localScale.x + GameManager.Instance.playerGrowSize,
            transform.localScale.y + GameManager.Instance.playerGrowSize,
            transform.localScale.z + GameManager.Instance.playerGrowSize);
        GameManager.Instance.playerSize++;
    }
    private void OnWrongTake()
    {
        transform.localScale = new Vector3(transform.localScale.x - GameManager.Instance.playerGrowSize,
            transform.localScale.y - GameManager.Instance.playerGrowSize,
            transform.localScale.z - GameManager.Instance.playerGrowSize);
        GameManager.Instance.playerSize--;
    }
}
