using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public Rigidbody SphereRB;
    public float ForwardAccel = 8f, MaximumSpeed = 50f, TurnStrength = 180f, DragOnGround = 3f, WheelTurnSpeed = 5f;
    public float sideDragForce = 1000f;

    public LayerMask WhatIsGround;
    public float GroundRayLength = .5f;
    public Transform GroundRayPoint;

    public Transform LeftFrontWheel, RightFrontWheel, LeftBackWheel, RightBackWheel;
    public float MaxWheelTurn = 25f;

    public float WheelRotation = 50f;

    public GameObject Arrow;


    private float _speedInput, _turnInput;
    private bool _isGrounded;
    private float _fictiveSideDragForce = 0f;
    private float _turnSense;

    private void Start()
    {
        SphereRB.transform.parent = null;
    }

    private void Update()
    {
        _speedInput = 0f;

        if (Mathf.Abs(Input.GetAxis("Vertical")) > 0)
        {
            _speedInput = Input.GetAxis("Vertical") * ForwardAccel * 1000f;

            LeftFrontWheel.transform.Rotate(0f, WheelRotation, 0f);
            RightFrontWheel.transform.Rotate(0f, WheelRotation, 0f);

            if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
            {
                _fictiveSideDragForce = sideDragForce * 1000f;
                _turnSense = Mathf.Sign(Input.GetAxis("Horizontal"));
            }
            else
            {
                _fictiveSideDragForce = 0f;
            }
        }

        _turnInput = Input.GetAxis("Horizontal");

        if (_isGrounded)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + 
                new Vector3(0f, _turnInput * TurnStrength * Time.deltaTime, 0f));

            if(Mathf.Abs(Input.GetAxis("Vertical")) > 0)
            {
                LeftBackWheel.transform.Rotate(0f, WheelRotation, 0f);
                RightBackWheel.transform.Rotate(0f, WheelRotation, 0f);
            }
        }

        LeftFrontWheel.localRotation = Quaternion.Euler(LeftFrontWheel.localRotation.eulerAngles.x, (_turnInput * MaxWheelTurn) - 90,
              LeftFrontWheel.localRotation.eulerAngles.z);

        RightFrontWheel.localRotation = Quaternion.Euler(RightFrontWheel.localRotation.eulerAngles.x, (_turnInput * MaxWheelTurn) - 90,
             RightFrontWheel.localRotation.eulerAngles.z);

        transform.position = SphereRB.transform.position;
    }

    private void FixedUpdate()
    {
        _isGrounded = false;

        RaycastHit hit;

        if(Physics.Raycast(GroundRayPoint.position, -transform.up, out hit, GroundRayLength, WhatIsGround))
        {
            _isGrounded = true;

            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }

        if(_isGrounded)
        {
            SphereRB.drag = DragOnGround;

            if (Mathf.Abs(_speedInput) > 0f)
            {
                SphereRB.AddForce(transform.forward * _speedInput);
                SphereRB.velocity = Vector3.ClampMagnitude(SphereRB.velocity, MaximumSpeed);
            }

            if (_fictiveSideDragForce > 0f)
            {
                Vector3 sideDragForceDirection = Quaternion.Euler(0, _turnSense * 90, 0) * transform.forward;
                SphereRB.AddForce(sideDragForceDirection * _fictiveSideDragForce);
            }
        }
        else
        {
            SphereRB.drag = 0;
        }
    }
}
