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
}
