using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    public Transform FirstCheckpoint;
    public Transform CurrentCheckpoint;

    private void Start()
    {
        CurrentCheckpoint = FirstCheckpoint;
    }

    private void OnTriggerEnter(Collider other)
    {
        other.transform.position = CurrentCheckpoint.transform.position;
    }

    public void PlaceInstanceOnStartingLine(GameObject gameObject)
    {
        gameObject.transform.position = FirstCheckpoint.transform.position;
        Debug.Log(gameObject.name);
    }
}
