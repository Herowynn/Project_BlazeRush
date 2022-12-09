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

    public static CarController Instance;


    private float _speedInput, _turnInput;
    private float _arrayRayLength;
    private bool _isGrounded;
    private bool _canMove;

    private void Start()
    {
        SphereRB.transform.parent = null;

        Vector3 distArrowRayPoint = Arrow.transform.position - RayPoint.position;
        _arrayRayLength = distArrowRayPoint.magnitude + 1f;
    }

    private void Update()
    {
        Debug.Log(SphereRB.velocity);

        _speedInput = 0f;

        if (Mathf.Abs(Input.GetAxis("Vertical")) > 0)
        {
            _speedInput = Input.GetAxis("Vertical") * ForwardAccel * 1000f;

            LeftFrontWheel.transform.Rotate(0f, WheelRotation, 0f);
            RightFrontWheel.transform.Rotate(0f, WheelRotation, 0f);
        }

        _turnInput = Input.GetAxis("Horizontal");

        if (_isGrounded)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, _turnInput * TurnStrength * Time.deltaTime, 0f));
            ArrowRotationCenter.transform.rotation = Quaternion.Euler(ArrowRotationCenter.transform.rotation.eulerAngles + new Vector3(0f, _turnInput * TurnStrength * 1.5f * Time.deltaTime, 0f));

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
        _canMove = false;

        RaycastHit hit;

        if (Physics.Raycast(RayPoint.position, -transform.up, out hit, GroundRayLength, groundMask))
        {
            _isGrounded = true;

/*            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;*/
        }

        if (Physics.Raycast(RayPoint.position, transform.forward, out hit, _arrayRayLength, arrowMask))
        {
            _canMove = true;
        }

        if (_isGrounded)
        {
            SphereRB.drag = DragOnGround;

            if (Mathf.Abs(_speedInput) > 0f)
            {
                SphereRB.AddForce(transform.forward * _speedInput);
                SphereRB.velocity = Vector3.ClampMagnitude(SphereRB.velocity, MaximumSpeed);
            }
        }
        else
        {
            SphereRB.drag = 0;
        }
    }
}
