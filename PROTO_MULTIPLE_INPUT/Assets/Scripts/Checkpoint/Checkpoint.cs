using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        GetComponentInParent<CheckPointManager>().CurrentCheckpoint = transform;
    }
}
