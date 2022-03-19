using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight : MonoBehaviour
{
    public string[] attackAnimations = new string[] { "Punch", "Kick" };
    void Start()
    {

    }
    public string GetRandomAttackAnimation()
    {
        int index = UnityEngine.Random.Range(0, attackAnimations.Length);
        return attackAnimations[index];
    }
}
