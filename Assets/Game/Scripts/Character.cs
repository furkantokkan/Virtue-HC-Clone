using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public enum CharacterID
    {
        Player,
        Stack,
        Boss,
        None
    }

    public CharacterID currentCharacterID = CharacterID.None;
    public int characterSize = 1;
    public Material currentMaterial;

    private void Start()
    {
        currentMaterial = GetComponent<MeshRenderer>().material;
        transform.localScale = new Vector3(characterSize, characterSize, characterSize);
        GameManager.Instance.onRightCharacterTake += OnRightTake;
        GameManager.Instance.onWrongCharacterTake += OnWrongTake;
    }
    private void OnDisable()
    {
        GameManager.Instance.onRightCharacterTake -= OnRightTake;
        GameManager.Instance.onWrongCharacterTake -= OnWrongTake;
    }
    private void OnRightTake()
    {
        if (currentCharacterID == CharacterID.Player)
        {
            transform.localScale = new Vector3(transform.localScale.x + GameManager.Instance.playerGrowSize,
                transform.localScale.x + GameManager.Instance.playerGrowSize,
                transform.localScale.x + GameManager.Instance.playerGrowSize);
        }
    }
    private void OnWrongTake()
    {
        if (currentCharacterID == CharacterID.Player)
        {
            transform.localScale = new Vector3(transform.localScale.x - GameManager.Instance.playerGrowSize,
                transform.localScale.x - GameManager.Instance.playerGrowSize,
                transform.localScale.x - GameManager.Instance.playerGrowSize);
        }
    }
}
