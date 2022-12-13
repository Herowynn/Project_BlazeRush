using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class Booster : MonoBehaviour
{
    public float DurationAfterActivation;
    public float SpeedAdded;
    public float AccelerationDuration;
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
        while (_timeIncrementation < DurationAfterActivation)
        {

            carRB.AddForce(carRB.transform.forward * SpeedAdded, ForceMode.VelocityChange);
            _timeIncrementation += 0.01f;
            //Debug.Log("time incrementation " + _timeIncrementation + " duration " + _durationAfterActivation);
            yield return new WaitForSeconds(0.01f);
        }
        Destroy(gameObject);

    }
}
