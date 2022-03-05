using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Pause
    }

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
}
