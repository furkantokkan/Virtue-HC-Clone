using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }
    public enum GameState
    {
        BeforeStart,
        Normal,
        Failed,
        Victory,
        Pause,
        Fight
    }

    public const float MAX_X = 4.25f;
    public const float MIN_X = -4.25f;

    [Header("Template Settings")]
    [SerializeField] private bool isDebug = true;

    [SerializeField] private bool startLevelMusicOnPlay = false;

    [SerializeField] private bool giveInputOnFirstClick = false;

    [SerializeField] private bool takeInputCount = false;

    [SerializeField] private bool showMenuOnNewSceneLoaded = false;

    [SerializeField] private bool useAppMetrica = false;

    [SerializeField] private bool useAppsFlyer = false;

    [SerializeField] private bool clearAllPoolObjectsOnNewLevelLoad = false;

    [SerializeField] private bool giveInputToUser = false;
    public bool GiveInputToUser { get { return giveInputToUser; } set { giveInputToUser = value; } }
    public bool IsDebug { get { return isDebug; } }
    public bool GiveInputOnFirstClick { get { return giveInputOnFirstClick; } }
    public bool TakeInputCount { get { return takeInputCount; } }
    public bool ShowMenuOnNewSceneLoaded { get { return showMenuOnNewSceneLoaded; } }
    public bool UseAppMetrica { get { return useAppMetrica; } }
    public bool UseAppsFlyer { get { return useAppsFlyer; } }
    public bool ClearAllPoolObjectsOnNewLevelLoad { get { return clearAllPoolObjectsOnNewLevelLoad; } }
    public bool StartLevelMusicOnPlay { get { return startLevelMusicOnPlay; } }

    public static Action onWinEvent;
    public static Action onLoseEvent;

    [Header("Game Settings")]
    public GameState currentState = GameState.BeforeStart;
    public Material[] allMaterails;
    public float horizontalSpeed = 20f;
    public float forwardSpeed = 5f;
    public float playerSmooth = 8f;
    public float playerGrowSize = 0.25f;
    public int playerSize = 1;

    public Action<int> onCharacterTake;
    public Action onRightCharacterTake;
    public Action onWrongCharacterTake;

    public Transform actor;
    public Transform finishLine;

    public CinemachineVirtualCamera[] cameras;

    private bool onWin = false;


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

        if (isDebug == true)
        {
            Debug.LogWarning("Debug Mode Active");
        }

        if (!useAppMetrica)
        {
            FindObjectOfType<AppMetrica>().gameObject.SetActive(false);
        }

        if (!useAppsFlyer)
        {
            FindObjectOfType<AppsFlyerObjectScript>().gameObject.SetActive(false);
        }

        Application.targetFrameRate = 60;
    }
    private void Start()
    {
        if (IsDebug)
        {
            OnNewLevelLoaded();
        }
        LevelManager.onLevelRendered += OnNewLevelLoaded;
    }
    private void OnDisable()
    {
        LevelManager.onLevelRendered -= OnNewLevelLoaded;
    }
    private void OnNewLevelLoaded()
    {
        actor = GameObject.FindGameObjectWithTag("Player").transform;
        finishLine = GameObject.FindGameObjectWithTag("FinishLine").transform;
        UIManager manager = FindObjectOfType<UIManager>();
        manager.SetProgresBarMaxValue(finishLine.transform.position.z);
    }
    private void Update()
    {
        if (currentState == GameState.Fight)
        {
            cameras[1].Priority = 2;
        }

        if (currentState == GameState.Victory)
        {
            if (onWin == false)
            {
                print("Win Event");
                onWinEvent?.Invoke();
                onWin = true;
            }
            print("Victory");
        }
        else if (currentState == GameState.Failed)
        {

        }
    }
}
