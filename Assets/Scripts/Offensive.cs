using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Offensive : MonoBehaviour
{
    [SerializeField]private float _speed;
    private Rigidbody ProjectileRB;
    // Start is called before the first frame update

    private void Awake()
    {
        ProjectileRB = GetComponent<Rigidbody>();
    }
    public void Shoot(Vector3 direction)
    {
        ProjectileRB.AddForce(direction * _speed, ForceMode.Acceleration);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
