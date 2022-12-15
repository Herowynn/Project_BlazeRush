using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float InitialOffsetY;
    public Vector3 ViewAngle;

    //intern var
    [SerializeField] private bool _isCameraLaunched = false;
    
    void Start()
    {
        Vector3 FCpos = GameManager.Instance.CheckPointManager.GetComponent<CheckPointManager>().FirstCheckpoint
            .transform.position;
        transform.position = new Vector3(FCpos.x, InitialOffsetY, FCpos.y);
        transform.rotation = Quaternion.Euler(ViewAngle.x, ViewAngle.y, ViewAngle.z);
    }

    void Update()
    {
        if (_isCameraLaunched)
            UpdateCameraPos();
            
    }

    public void UpdateCameraPos()
    {
        Vector3 cameraPos = new Vector3();
        var posFirstPlayer = GameManager.Instance.RaceRanking[0].transform.position;
        var posLastPlayer = GameManager.Instance.RaceRanking[GameManager.Instance.RaceRanking.Count - 1].transform.position;

        float diagonalDistance = Vector3.Distance(posFirstPlayer, transform.position);

        Vector3 groundedFirstPlayer = new Vector3(posFirstPlayer.x, 3, posFirstPlayer.z);
        Vector3 groundedCamera = new Vector3(transform.position.x, 3, transform.position.z);
        float groundDistance = Vector3.Distance(groundedFirstPlayer, groundedCamera);

        float heightDistance =
            MathF.Sqrt(MathF.Pow(diagonalDistance, 2) - MathF.Pow(groundDistance, 2));
        
        Debug.Log("heightDistance : " + heightDistance);
        
        cameraPos.x = (posFirstPlayer.x + posLastPlayer.x) / 2;
        cameraPos.z = (posFirstPlayer.z + posLastPlayer.z) / 2;

        if(heightDistance > InitialOffsetY)
        {
            cameraPos.y = heightDistance;
        }
        else
        {
            cameraPos.y = InitialOffsetY;
        }

        Debug.Log("CamerPos " + cameraPos.y);
        
        transform.position = cameraPos;
    }
}
