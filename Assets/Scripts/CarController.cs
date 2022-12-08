using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private int _speedBoost;
    private float _moveInput;
    private float _turnInput;
    private bool _isCarGrounded;
    private bool _hasBoost = false;
    private bool _hasOffense = false;

    public int gravity;
    public float TurnSpeed;
    public float FwdSpeed;
    public float BwdSpeed;
    public float alignToGroundTime;

    public GameObject AttackBoost;
    public Rigidbody CarRB;
    public Rigidbody SphereRB;
    public LayerMask GroundLayer;
    // Start is called before the first frame update
    void Start()
    {
        SphereRB.transform.parent = null;
        CarRB.transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        _moveInput = Input.GetAxisRaw("Vertical");
        _turnInput = Input.GetAxisRaw("Horizontal");

        _moveInput *= _moveInput > 0 ? FwdSpeed : BwdSpeed;

        transform.position = SphereRB.transform.position;

        float newRotation = _turnInput * TurnSpeed * Time.deltaTime /** Input.GetAxisRaw("Vertical")*/;
        transform.Rotate(0, newRotation, 0, Space.World);

        RaycastHit hit;
        _isCarGrounded = Physics.Raycast(transform.position, -transform.up, out hit, 1f, GroundLayer);

        Quaternion toRotateTo = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, toRotateTo, alignToGroundTime * Time.deltaTime);

        if (Input.GetButton("Fire1"))
        {
            if (_hasOffense)
            {
               Vector3 position = transform.position;
                position += transform.forward*3;
                GameObject go = Instantiate(AttackBoost, position, Quaternion.identity);
                go.GetComponent<Projectile>().Init(transform.forward);
                
                _hasOffense = false;
            }
            else
            {
                return;
            }
        }
        if (Input.GetButton("Fire3"))
        {
            if (_hasBoost)
            {

                SphereRB.AddForce(transform.forward * _speedBoost, ForceMode.VelocityChange);
                _hasBoost = false;
            }
            else
            {
                return;
            }
        }
    }

    private void FixedUpdate()
    {
        if (_isCarGrounded)
        {
            SphereRB.AddForce(transform.forward * _moveInput, ForceMode.Acceleration);
        }
        else
        {
            SphereRB.AddForce(transform.up * -gravity);
        }
        CarRB.MovePosition(transform.position);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.parent.parent.name == "Bonus")
        {
            if (collision.transform.parent.name == "Boost")
            {
                if (_hasBoost)
                {
                    return;
                }
                else
                {
                    _hasBoost = true;
                    Destroy(collision.gameObject);
                }
            }
            if (collision.transform.parent.name == "Offense")
            {
                if (_hasOffense)
                {
                    return;
                }
                else
                {
                    _hasOffense = true;
                    Destroy(collision.gameObject);
                }
            }
        }
    }
}
