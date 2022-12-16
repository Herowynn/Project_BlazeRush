using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    [HideInInspector] public string Username = "";
    public int RankPosition;

    public void Start()
    {
        InitializePrefab();
    }

    private void InitializePrefab()
    {
        //Intern variable change
        Username = GameManager.Instance.PlayersUsernames[GameManager.Instance.CurrentIndexForUsernames];
        
        //Modification for next instantiation;
        GameManager.Instance.CurrentIndexForUsernames++;
        
        //Gameobject's name
        StringBuilder sb = new StringBuilder();
        sb.Append("Player (" + Username + ")");
        transform.gameObject.name = sb.ToString();
        
        //Add self to the GO tab & the ranking table
        GameManager.Instance.PlayersGameObjects.Add(transform.gameObject);
        GameManager.Instance.RaceRanking.Add(transform.gameObject);
        
        //Initialize Position
        transform.gameObject.transform.position = GameManager.Instance.CheckPointManager
            .GetComponent<CheckPointManager>().FirstCheckpoint.transform.position;
    }
}
