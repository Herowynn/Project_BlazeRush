using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class Booster : MonoBehaviour
{
    [SerializeField] private float _durationAfterActivation;
    [SerializeField] private float _speedAdded;
    [SerializeField] private float _time;
    private float _timeIncrementation;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Boost(Rigidbody carRB)
    {
        StartCoroutine(StartBoost(carRB));
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator StartBoost(Rigidbody carRB)
    {
        _timeIncrementation = 0;
        while (_timeIncrementation < _durationAfterActivation)
        {

            carRB.AddForce(carRB.transform.forward * _speedAdded, ForceMode.VelocityChange);
            _timeIncrementation += Time.deltaTime;
            //Debug.Log("time incrementation " + _timeIncrementation + " duration " + _durationAfterActivation);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        Destroy(gameObject);

    }
}
