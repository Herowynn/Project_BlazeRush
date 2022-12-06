using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public Rigidbody SphereRB;
    public float ForwardAccel = 8f, MaximumSpeed = 50f, TurnStrength = 180f, GravityForce = 10f, DragOnGround = 3f, WheelTurnSpeed = 5f;

    private float _speedInput, _turnInput;

    private bool _isGrounded;

    public LayerMask WhatIsGround;
    public float GroundRayLength = .5f;
    public Transform GroundRayPoint;

    public Transform LeftFrontWheel, RightFrontWheel, LeftBackWheel, RightBackWheel;
    public float MaxWheelTurn = 25f;

    public float WheelRotation = 50f;

    public GameObject Arrow;

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
            LeftFrontWheel.transform.Rotate(WheelRotation * Time.deltaTime, 0f, 0f);
            RightFrontWheel.transform.Rotate(WheelRotation * Time.deltaTime, 0f, 0f);
        }

        _turnInput = Input.GetAxis("Horizontal");

        if (_isGrounded)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + 
                new Vector3(0f, _turnInput * TurnStrength * Time.deltaTime, 0f));
            if(Mathf.Abs(Input.GetAxis("Vertical")) > 0)
            {
                LeftBackWheel.transform.Rotate(WheelRotation * Time.deltaTime, 0f, 0f);
                RightBackWheel.transform.Rotate(WheelRotation * Time.deltaTime, 0f, 0f);
            }
        }

        LeftFrontWheel.localRotation = Quaternion.Euler(LeftFrontWheel.localRotation.eulerAngles.x,
            (_turnInput * MaxWheelTurn) - 180, LeftFrontWheel.localRotation.eulerAngles.z);

        RightFrontWheel.localRotation = Quaternion.Euler(RightFrontWheel.localRotation.eulerAngles.x,
            (_turnInput * MaxWheelTurn), RightFrontWheel.localRotation.eulerAngles.z);

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
            }
        }
        else
        {
            SphereRB.drag = 0.1f;

            SphereRB.AddForce(Vector3.up * -GravityForce * 100f);
        }
    }
}
