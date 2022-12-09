using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckpoint : MonoBehaviour
{
    [SerializeField] private int _numberOfCheckpointPassed;
    
    // Start is called before the first frame update
    void Start()
    {
        ResetCheckpointCount();
    }

    public void ResetCheckpointCount()
    {
        _numberOfCheckpointPassed = 0;
    }

    public void IncrementCheckpointCount()
    {
        _numberOfCheckpointPassed++;
    }

    public int GetNumberOfCheckpointPassed()
    {
        return _numberOfCheckpointPassed;
    }
}
