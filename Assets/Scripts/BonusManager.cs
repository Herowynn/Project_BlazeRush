using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using UnityEngine;

public class BonusManager : MonoBehaviour
{
    private System.Timers.Timer _timer;
    public GameObject BoostPrefab;
    public GameObject BoostParent;
    public GameObject AttackPrefab;
    public GameObject AttackParent;
    public GameObject CarPrefab;
    // Start is called before the first frame update
    void Start()
    {
        //_timer = new System.Timers.Timer(2000);
        //_timer.Start();
        //_timer.Elapsed += SpawnBonus;
        //_timer.Enabled = true;
        //_timer.AutoReset = true;
    }

    // Update is called once per frame
    void Update()
    {
        int randomX = Random.Range(5, 10);
        int randomY = Random.Range(5, 10);
        int randomZ = Random.Range(-5, 5);


        Vector3 SpawnPosition = new(randomX, randomY, randomZ);
        SpawnPosition += CarPrefab.transform.forward*3;
        SpawnPosition += CarPrefab.transform.position;

        GameObject go = Instantiate(AttackPrefab, SpawnPosition, Quaternion.identity);
        go.transform.parent = AttackParent.transform;
    }   
    //private void SpawnBonus(object source, ElapsedEventArgs e)
    //{
    //    int randomX = Random.Range(5, 10);
    //    int randomY = Random.Range(5, 10);
    //    int randomZ = Random.Range(-5, 5);


    //    Vector3 SpawnPosition = new(randomX, randomY, randomZ);
    //    SpawnPosition += CarPrefab.transform.position;

    //    GameObject go = Instantiate(AttackPrefab, SpawnPosition, Quaternion.identity);

    //}
}
