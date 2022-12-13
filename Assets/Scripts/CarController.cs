using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private float _moveInput;
    private float _turnInput;
    private bool _isCarGrounded;
    private GameObject _attackBoost;
    private GameObject _speedBoost;

    public int gravity;
    public float TurnSpeed;
    public float FwdSpeed;
    public float BwdSpeed;
    public float alignToGroundTime;

    public GameObject[] AttackList;
    public GameObject[] BoostList;
    public Transform AttackObject;
    public Transform BoostObject;
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

        if (_attackBoost != null && Input.GetButton("Fire1"))
        {
            if (AttackObject.transform.childCount != 0)
            {
                if (AttackObject.transform.GetComponentInChildren<MachineGun>())
                {
                    Vector3 position = transform.position;
                    position += transform.forward * 3;

                    AttackObject.transform.GetChild(0).GetComponent<MachineGun>().Shoot();
                    _attackBoost = null;
                    //Destroy(AttackObject.gameObject.GetComponent<MachineGun>());
                  
                }
            }
            else
            {
                return;
            }
        }
        if (_speedBoost != null && Input.GetButton("Fire3"))
        {
            if (BoostObject.transform.childCount != 0)
            {
                BoostObject.transform.GetChild(0).GetComponent<Booster>().Boost(SphereRB);
                _speedBoost = null;
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

        if (collision.gameObject.GetComponent<Bonus>() != null)
        {
            switch (collision.gameObject.GetComponent<Bonus>().Type)
            {
                case BonusType.Attack:
                    if (AttackObject.transform.childCount != 0) return;
                    else
                    {
                        Destroy(collision.gameObject);


                        _attackBoost = Instantiate(AttackList[0], AttackObject);
                        //AttackObject.transform.GetChild(0).gameObject.AddComponent<MachineGun>();
                    }
                    break;
                case BonusType.Boost:
                    if (BoostObject.transform.childCount != 0) return;
                    else
                    {
                        Destroy(collision.gameObject);
                        _speedBoost = Instantiate(BoostList[0], BoostObject);
                    }
                    break;
                default:
                    break;
            }

        }
    }
}

