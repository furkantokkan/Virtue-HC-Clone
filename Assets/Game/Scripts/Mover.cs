using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public void MoveTo(Vector3 targetDir)
    {
        targetDir += transform.localPosition;
        targetDir.x = Mathf.Clamp(targetDir.x, GameManager.MIN_X, GameManager.MAX_X);
        //transform.position = targetDir;
        transform.position = Vector3.Lerp(transform.position, targetDir, GameManager.Instance.playerSmooth * Time.deltaTime);
    }
}
