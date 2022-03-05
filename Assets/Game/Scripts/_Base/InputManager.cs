using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager instance;
    public static InputManager Instance { get { return instance; } }

    private Vector2 lastClickPos;
    private Vector3 direction;

    public event Action onTouchStart;
    public event Action onTouchStationary;
    public event Action onTouchEnd;
    public event Action onTouchMove;
    public event Action onTouchCancel;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        if (Application.isMobilePlatform && Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    onTouchStart?.Invoke();
                    direction = Vector3.zero;
                    lastClickPos = touch.position;
                    break;
                case TouchPhase.Moved:
                    onTouchMove?.Invoke();
                    direction = lastClickPos - touch.position;
                    lastClickPos = touch.position;
                    break;
                case TouchPhase.Stationary:
                    onTouchStationary?.Invoke();
                    break;
                case TouchPhase.Ended:
                    onTouchEnd?.Invoke();
                    break;
                case TouchPhase.Canceled:
                    direction = Vector3.zero;
                    onTouchCancel?.Invoke();
                    break;
                default:
                    break;
            }
        }
        else
        {
            bool mouseMoving = false;

            if (Input.GetMouseButtonDown(0))
            {
                onTouchStart?.Invoke();
                lastClickPos = Input.mousePosition;
                direction = Vector3.zero;
            }
            else if (Input.GetMouseButton(0))
            {
                direction = new Vector3(lastClickPos.x, lastClickPos.y, 0) - Input.mousePosition;
                lastClickPos = Input.mousePosition;

                if (direction.x != 0 || direction.y != 0)
                {
                    mouseMoving = true;
                    onTouchMove?.Invoke();
                }
                else if (!mouseMoving)
                {
                    onTouchStationary?.Invoke();
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                mouseMoving = false;
                direction = Vector3.zero;
                onTouchEnd?.Invoke();
            }
        }
    }
    public Vector3 GetDirection()
    {
        return new Vector2(direction.x / Screen.width, direction.y / Screen.height);
    }
}
