using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    private static PoolManager instance;
    [SerializeField] private List<PoolItem> items;
    private List<GameObject> SpawnedItems = new List<GameObject>();

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
    private void OnEnable()
    {
        if (GameManager.Instance.ClearAllPoolObjectsOnNewLevelLoad)
        {
            LevelManager.onNewLevelLoaded += ClearAllObjects;
        }
    }
    private void OnDisable()
    {
        if (GameManager.Instance.ClearAllPoolObjectsOnNewLevelLoad)
        {
            LevelManager.onNewLevelLoaded -= ClearAllObjects;
        }
    }
    private void Start()
    {
        foreach (PoolItem item in items)
        {
            for (int i = 0; i < item.amount; i++)
            {
                GameObject itemClone = Instantiate(item.prefab);
                itemClone.SetActive(false);
                SpawnedItems.Add(itemClone);
            }
        }
    }
    private GameObject GetObject(string newTag, Vector3 newPosition, Quaternion newRotation)
    {
        for (int i = 0; i < SpawnedItems.Count; i++)
        {
            if (!SpawnedItems[i].activeInHierarchy && SpawnedItems[i].tag == newTag)
            {
                GameObject selectedObject = SpawnedItems[i].gameObject;
                selectedObject.transform.position = newPosition;
                selectedObject.transform.rotation = newRotation;
                selectedObject.gameObject.SetActive(true);
                return selectedObject;
            }
        }
        //Expand Pool
        foreach (PoolItem item in items)
        {
            if (item.prefab.tag == newTag && item.expandable)
            {
                GameObject itemClone = Instantiate(item.prefab);
                itemClone.transform.position = newPosition;
                itemClone.transform.rotation = newRotation;
                itemClone.SetActive(true);
                SpawnedItems.Add(itemClone);
                return itemClone;
            }
        }
        Debug.LogWarning("Missing Tag Or Prefab");
        return null;
    }
    private void ClearAllObjects()
    {
        if (SpawnedItems.Count <= 0)
        {
            return;
        }

        for (int i = 0; i < SpawnedItems.Count; i++)
        {
            if (!SpawnedItems[i].activeInHierarchy)
            {
               SpawnedItems[i].gameObject.SetActive(false);
            }
        }
    }
    public static GameObject ObjectPool(string newTag, Vector3 newPosition, Quaternion newRotation)
    {
        return instance.GetObject(newTag, newPosition, newRotation);
    }
}

[System.Serializable]
public struct PoolItem
{
    public GameObject prefab;
    public int amount;
    public bool expandable;
}