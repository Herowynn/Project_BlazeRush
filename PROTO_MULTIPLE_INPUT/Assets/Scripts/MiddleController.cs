using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleController : MonoBehaviour
{
    public float DistanceBetweenFirstAndLast;
    void Update()
    {
        if (GameManager.Instance.RaceStarted)
        {
            var posFirstPlayer = GameManager.Instance.RaceRanking[0].transform.position;
            var posLastPlayer = GameManager.Instance.RaceRanking[GameManager.Instance.RaceRanking.Count - 1].transform.position;

            DistanceBetweenFirstAndLast = Vector3.Distance(posFirstPlayer, posLastPlayer);
            
            var desiredPosition = new Vector3((posFirstPlayer.x + posLastPlayer.x) / 2, posFirstPlayer.y, (posFirstPlayer.z + posLastPlayer.z) / 2);
            transform.position = desiredPosition;
        }
    }
}
