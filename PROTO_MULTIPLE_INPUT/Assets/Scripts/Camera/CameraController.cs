using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float MinHeight;
    public Vector3 ViewAngle;
    public Vector3 OffsetPos;
    public float SmoothSpeed = 10f;
    public float Ratio;
    public GameObject Target;
    
    void Start()
    {
        Vector3 FCpos = GameManager.Instance.CheckPointManager.GetComponent<CheckPointManager>().FirstCheckpoint
            .transform.position;
        transform.position = new Vector3(FCpos.x, MinHeight, FCpos.y);
        transform.rotation = Quaternion.Euler(ViewAngle.x, ViewAngle.y, ViewAngle.z);
    }

    void LateUpdate()
    {
        if (GameManager.Instance.RaceStarted)
            UpdateCameraPos();
    }

    public void UpdateCameraPos()
    {
        /*float groundedDistanceBetweenFirstAndLast = Target.GetComponent<MiddleController>().DistanceBetweenFirstAndLast;
        float yPosititon = Ratio * groundedDistanceBetweenFirstAndLast > MinHeight
            ? Ratio * groundedDistanceBetweenFirstAndLast
            : MinHeight;
        
        Vector3 cameraDesiredPosition = new Vector3(Target.transform.position.x + OffsetPos.x, yPosititon + OffsetPos.y, Target.transform.position.z + OffsetPos.z);

        //Vector3 velocity = Vector3.zero;
        //Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, cameraDesiredPosition, ref velocity, SmoothSpeed);

        transform.position = cameraDesiredPosition;
        transform.LookAt(Target.transform);*/
        
        
        Vector3 desiredPosition = new Vector3();
        var posFirstPlayer = GameManager.Instance.RaceRanking[0].transform.position;
        var posLastPlayer = GameManager.Instance.RaceRanking[GameManager.Instance.RaceRanking.Count - 1].transform.position;

        float groundedDistance = Vector3.Distance(posFirstPlayer, posLastPlayer);
        
        desiredPosition.x = (posFirstPlayer.x + posLastPlayer.x) / 2;
        desiredPosition.z = (posFirstPlayer.z + posLastPlayer.z) / 2;
        desiredPosition.y = Ratio * groundedDistance > MinHeight ? Ratio * groundedDistance : MinHeight;
        
        Vector3 cameraPos = Vector3.Lerp(transform.position, desiredPosition, SmoothSpeed * Time.deltaTime);
        
        transform.position = cameraPos;
    }
}
