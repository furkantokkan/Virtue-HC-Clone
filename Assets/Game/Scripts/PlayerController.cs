using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
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
}
