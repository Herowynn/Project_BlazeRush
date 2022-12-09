using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using UnityEngine;

public class BonusManager : MonoBehaviour
{
    public GameObject BoostPrefab;
    public GameObject BoostParent;
    public GameObject AttackPrefab;
    public GameObject AttackParent;
    public GameObject CarPrefab;
    private float _spawnTimer;
    private float _timeIncrementation;
    [SerializeField] private int _maxTimeBetweenBonusSpawn;
    [SerializeField] private int _minTimeBetweenBonusSpawn;
    // Start is called before the first frame update
    void Start()
    {
        _spawnTimer = 4.0f;
    }

    // Update is called once per frame
    void Update()
    {
        _timeIncrementation += Time.deltaTime;

        if (_timeIncrementation >= _spawnTimer)
        {
            _timeIncrementation = 0;
            SpawnBonus();
            _spawnTimer = Random.Range(_minTimeBetweenBonusSpawn, _maxTimeBetweenBonusSpawn);
        }
    }
    private void SpawnBonus()
    {
        int randomX = Random.Range(-5, 5);
        int randomY = Random.Range(3, 10);
        int randomZ = Random.Range(-5, 5);
        int rndBonusType = Random.Range(0, 2);

        Vector3 SpawnPosition = new(randomX, randomY, randomZ);
        SpawnPosition += CarPrefab.transform.forward * 3;
        SpawnPosition += CarPrefab.transform.position;
        GameObject go = null;
        switch (rndBonusType)
        {
            case 0:
                go = Instantiate(AttackPrefab, SpawnPosition, Quaternion.identity);
                go.transform.parent = AttackParent.transform;
                break;
            case 1:
                go = Instantiate(BoostPrefab, SpawnPosition, Quaternion.identity);
                go.transform.parent = BoostParent.transform;
                break;
            default:
                break;
        }

    }
}
