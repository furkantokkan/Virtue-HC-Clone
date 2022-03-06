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
    public Animator anim;
    public SkinnedMeshRenderer skinnedMesh;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        skinnedMesh = anim.GetComponentInChildren<SkinnedMeshRenderer>();
        currentMaterial = skinnedMesh.materials[0];
    }

    private void Start()
    {
        transform.localScale = new Vector3(characterSize, characterSize, characterSize);
    }
 
 
}
