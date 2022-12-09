using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MachineGun : Offensive
{
    [SerializeField] private float _durationAfterActivation;
    [SerializeField] private float _fireRate;
    [SerializeField] private Transform _bulletSpawnPoint;
    private float _timeIncrementation ;

    public GameObject ProjectilePrefab;
    // Start is called before the first frame update
    void Start()
    {

    }

    public override void Shoot(Vector3 direction)
    {
       
        StartCoroutine(WaitTime(_fireRate, direction));
    }
    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator WaitTime(float time, Vector3 direction)
    {
        _timeIncrementation = 0;
        while(_timeIncrementation < _durationAfterActivation)
        {
            GameObject go = Instantiate(ProjectilePrefab, _bulletSpawnPoint);
            go.GetComponent<Projectile>().Init(direction);
            _timeIncrementation += time;
            Debug.Log("time incrementation " + _timeIncrementation + " duration " + _durationAfterActivation);
            yield return new WaitForSeconds(time);
        }
        Destroy(gameObject);
        
    }
}
