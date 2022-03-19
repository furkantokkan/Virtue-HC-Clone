using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTracker : MonoBehaviour
{
    public Character character;
    public Fight fight;
    // Start is called before the first frame update

    private void Awake()
    {
        character = GetComponentInParent<Character>();
        fight = GetComponentInParent<Fight>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnHit(string i)
    {
        if (i == "Kick")
        {
            PoolManager.ObjectPool("Fight", character.leg.position, Quaternion.identity);
        }
        else if (i == "Punch")
        {
            PoolManager.ObjectPool("Fight", character.hand.position, Quaternion.identity);
        }

        fight.health--;
    }
}
