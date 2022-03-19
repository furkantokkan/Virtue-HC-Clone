using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight : MonoBehaviour
{
    public string[] attackAnimations = new string[] { "Punch", "Kick" };
    public int health;
    public Character character;

    private void Awake()
    {
        character = GetComponent<Character>();
    }
    void Start()
    {
        if (GetComponent<Character>().currentCharacterID == Character.CharacterID.Player)
        {
            GameManager.Instance.onCharacterTake += ChangeHealth;
        }
        health = GetComponent<Character>().characterSize;
    }
    private void OnDisable()
    {
        if (GetComponent<Character>().currentCharacterID == Character.CharacterID.Player)
        {
            GameManager.Instance.onCharacterTake -= ChangeHealth;
        }
    }

    private void Update()
    {
        if (health <= 0)
        {
            if (character.currentCharacterID == Character.CharacterID.Player)
            {
                GameManager.Instance.currentState = GameManager.GameState.Failed;
            }
            else if (character.currentCharacterID == Character.CharacterID.Boss)
            {
                GameManager.Instance.currentState = GameManager.GameState.Victory;
            }
            Destroy(gameObject);
        }
    }

    public string GetRandomAttackAnimation()
    {
        int index = UnityEngine.Random.Range(0, attackAnimations.Length);
        return attackAnimations[index];
    }
    private void ChangeHealth(int amount)
    {
        health = amount;
    }
}
