using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject[] nodes;
    public GameObject[] players;
    

    [SerializeField] private float _cameraElevation;
    [SerializeField] private float _camNodeDistanceTolerance;
    [SerializeField] private float _camPlayerDistance;
    [SerializeField] private float _smoothTime;
    private float _cameraSpeed;
    private Vector3 _target;
    private Vector3 _targetCameraPosition;
    private int _indexNextNode;
    private float _maxTargetDist;

    void Start()
    {
        foreach(GameObject node in nodes)
        {
            node.GetComponent<MeshRenderer>().enabled = false;
        }

        transform.position = new Vector3(nodes[0].transform.position.x, _cameraElevation, nodes[0].transform.position.z);
        _indexNextNode = 1;
        _maxTargetDist = _camPlayerDistance + 10f;
    }

    void Update()
    {
        Vector3 sum = new Vector3(0, 0, 0);
        //float speedSum = 0;
        float maxSpeed = 0f;
        foreach (GameObject player in players)
        {
            float speed = player.GetComponent<Rigidbody>().velocity.magnitude;
            /*speedSum += speed;*/
            if (speed > maxSpeed)
            {
                maxSpeed = speed;
            }

            sum += player.transform.position;
        }
        /*_cameraSpeed = speedSum / players.Length;*/
        _cameraSpeed = maxSpeed;
        _target = sum / players.Length;

        this.transform.LookAt(_target);

        _targetCameraPosition = nodes[_indexNextNode].transform.position + new Vector3(0, _cameraElevation, 0);
        Vector3 distToNextPos = new Vector3(nodes[_indexNextNode].transform.position.x, 0, nodes[_indexNextNode].transform.position.z) - new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 distToTarget = _target - this.transform.position;
        Vector3 speedVector = _cameraSpeed * distToNextPos.normalized;

        if (distToNextPos.magnitude > _camNodeDistanceTolerance && distToTarget.magnitude > _camPlayerDistance && distToTarget.magnitude <= _maxTargetDist)
        {
            this.transform.position = Vector3.SmoothDamp(this.transform.position, _targetCameraPosition, ref speedVector, _smoothTime);
        }
        else if (distToTarget.magnitude <= _camPlayerDistance || distToTarget.magnitude > _maxTargetDist) 
        {
            return;
        }
        else
        {
            Debug.Log("Node " + _indexNextNode + " has been reached");

            if (_indexNextNode < nodes.Length - 1)
            {
                _indexNextNode++;
            }
            else
            {
                _indexNextNode = 0;
            }
        }
    }
}
