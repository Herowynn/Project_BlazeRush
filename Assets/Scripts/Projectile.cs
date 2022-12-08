using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float _velocity;

    // Start is called before the first frame update
    private Rigidbody ProjectileRB;

    private void Awake()
    {
        ProjectileRB = GetComponent<Rigidbody>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Init(Vector3 direction)
    {
        ProjectileRB.AddForce(direction * _velocity, ForceMode.Acceleration);
    }
}


