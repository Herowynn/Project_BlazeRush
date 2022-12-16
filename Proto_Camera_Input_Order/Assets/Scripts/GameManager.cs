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

    //Intern
    [Header("Logic Variables")]
    public List<GameObject> RaceRanking;
    public bool RaceStarted;
    
    private void Awake()
    {
        Instance = this;
    }
    
    private void Start()
    {
        PlayersGameObjects = new List<GameObject>();
        RaceRanking = new List<GameObject>();
        RaceStarted = false;
        CurrentIndexForUsernames = 0;
    }

    private void FixedUpdate()
    {
        if (RaceStarted)
            UpdatePlayersRank();
    }

    private void UpdatePlayersRank()
    {
        RaceRanking.Sort((a, b) => b.GetComponent<PlayerCheckpoint>().GetNumberOfCheckpointPassed()
            .CompareTo(a.GetComponent<PlayerCheckpoint>().GetNumberOfCheckpointPassed()));

        for (int i = 0; i < RaceRanking.Count; i++)
        {
            RaceRanking[i].GetComponent<PlayerInfo>().RankPosition = i + 1;
        }
    }
}