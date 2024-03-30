using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class presitanceObjManager : MonoBehaviour
{
    [SerializeField] private GameObject persistantObjectPrefab=null;
    static bool hasSpawned = false;
    private void Awake()
    {
        if (hasSpawned) { return; }
        spawnPersistanceObject();
    }

    private void spawnPersistanceObject()
    {
        GameObject persistanceObject = Instantiate(persistantObjectPrefab);
        DontDestroyOnLoad(persistanceObject);
    }
}