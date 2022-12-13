using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject[] nodes;
    public GameObject[] players;
    

    [SerializeField] private float _cameraElevation;
    [SerializeField] private float _camNodeDistanceTolerance;
    private float _cameraSpeed;
    private Vector3 _target;
    private Vector3 _targetCameraPosition;
    private int _indexNextNode;

    void Start()
    {
        foreach(GameObject node in nodes)
        {
            node.GetComponent<MeshRenderer>().enabled = false;
        }

        transform.position = new Vector3(nodes[0].transform.position.x, _cameraElevation, nodes[0].transform.position.z);
        _indexNextNode = 1;
    }

    void Update()
    {

        transform.LookAt(players[0].transform);

        Vector3 sum = new Vector3(0, 0, 0);
        float maxSpeed = 0f;
        foreach (GameObject player in players)
        {
            float speed = player.GetComponent<Rigidbody>().velocity.magnitude;
            if (speed > maxSpeed)
            {
                maxSpeed = speed;
            }

            sum += player.transform.position;
        }
        _cameraSpeed = maxSpeed;
        _target = sum / players.Length;

        this.transform.LookAt(_target);

        _targetCameraPosition = nodes[_indexNextNode].transform.position + new Vector3(0, _cameraElevation, 0);
        Vector2 distToNextPos = new Vector2(transform.position.x, transform.position.z) - new Vector2(nodes[_indexNextNode].transform.position.x, nodes[_indexNextNode].transform.position.z);

        if (distToNextPos.magnitude > _camNodeDistanceTolerance) 
        {
            this.transform.position = Vector3.Lerp(this.transform.position, _targetCameraPosition, _cameraSpeed * Time.deltaTime*0.1f);
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
