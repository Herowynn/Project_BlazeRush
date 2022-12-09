using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Singleton
    public static GameManager Instance;
    
    [Header("Instances")]
    public List<GameObject> PlayersGameObjects;
    public GameObject CheckPointManager;
    
    [Header("GD")]
    public List<string> PlayersUsernames;//Will be replaced by inputs
    public int CurrentIndexForUsernames;

    private void Awake()
    {
        Instance = this;
    }
    
    private void Start()
    {
        PlayersGameObjects = new List<GameObject>();
        CurrentIndexForUsernames = 0;
    }
}