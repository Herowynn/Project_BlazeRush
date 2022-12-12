using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public Rigidbody SphereRB;
    public float ForwardAccel = 8f, MaximumSpeed = 50f, TurnStrength = 180f, DragOnGround = 3f, WheelTurnSpeed = 5f;

    public LayerMask groundMask;
    public LayerMask arrowMask;
    public float GroundRayLength = .5f;
    public Transform RayPoint;

    public Transform LeftFrontWheel, RightFrontWheel, LeftBackWheel, RightBackWheel;
    public float MaxWheelTurn = 25f;

    public float WheelRotation = 50f;

    public GameObject Arrow;
    public GameObject ArrowRotationCenter;


    private float _speedInput, _turnInput;
    private float _arrayRayLength;
    private bool _isGrounded;
    private bool _canMove;
    private Vector3 _distArrowRayPoint;
    private Vector3 _wantedDirection;

    private void Start()
    {
        SphereRB.transform.parent = null;

        _distArrowRayPoint = Arrow.transform.position - RayPoint.position;
        _arrayRayLength = _distArrowRayPoint.magnitude + 1f;
    }

    private void Update()
    {
        _speedInput = 0f;
        _distArrowRayPoint = Arrow.transform.position - RayPoint.position;

        if (Mathf.Abs(Input.GetAxis("Vertical")) > 0 || Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
        {
            _wantedDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            _speedInput = ForwardAccel * 1000f;

            LeftBackWheel.transform.Rotate(0f, WheelRotation, 0f);
            RightBackWheel.transform.Rotate(0f, WheelRotation, 0f);
            LeftFrontWheel.transform.Rotate(0f, WheelRotation, 0f);
            RightFrontWheel.transform.Rotate(0f, WheelRotation, 0f);

            if (_isGrounded)
            {
                Vector3 cross1 = Vector3.Cross(transform.forward, _wantedDirection);
                Vector3 cross2 = Vector3.Cross(_distArrowRayPoint, _wantedDirection);
                float carSignRotation = Mathf.Sign(cross1.y);
                float arrowSignRotation = Mathf.Sign(cross2.y);

                if (Mathf.Abs(Mathf.Acos(Vector3.Dot(transform.forward.normalized, _wantedDirection.normalized))) > Mathf.Deg2Rad * 1f)
                {
                    transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, carSignRotation * TurnStrength * Time.deltaTime, 0f));
                }

                if (Mathf.Abs(Mathf.Acos(Vector3.Dot(_distArrowRayPoint.normalized, _wantedDirection.normalized))) > Mathf.Deg2Rad * 1f)
                {
                    ArrowRotationCenter.transform.rotation = Quaternion.Euler(ArrowRotationCenter.transform.rotation.eulerAngles + new Vector3(0f, arrowSignRotation * TurnStrength * Time.deltaTime, 0f));
                }

            }
        }

        transform.position = SphereRB.transform.position;
    }

    private void FixedUpdate()
    {
        _isGrounded = false;

        RaycastHit hit;

        if (Physics.Raycast(RayPoint.position, -transform.up, out hit, GroundRayLength, groundMask))
        {
            _isGrounded = true;

            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }

        if (Physics.Raycast(RayPoint.position, transform.forward, out hit, _arrayRayLength, arrowMask))
        {
            _canMove = true;
        }
        else
        {
            _canMove = false;
        }

        if (_isGrounded)
        {
            SphereRB.drag = DragOnGround;

            if (Mathf.Abs(_speedInput) > 0f && _canMove) 
            {
                SphereRB.AddForce(_wantedDirection * _speedInput); ;
                SphereRB.velocity = Vector3.ClampMagnitude(SphereRB.velocity, MaximumSpeed);
            }
        }
        else
        {
            SphereRB.drag = 0;
        }
    }
}
