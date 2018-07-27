using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    [SerializeField]
    private GameObject spawnObj;
    [SerializeField]
    private float spawnInterval = 1f;
    [SerializeField]
    private bool isLimitedSpawn;
    [SerializeField]
    private int spawnNumber = 10;

    private int counter;
    private float timer;

    // Use this for initialization
    void Start () {
        counter = 0;
        timer = 0f;
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer >= spawnInterval && counter < spawnNumber) CreateObj();
	}

    private void CreateObj()
    {
        GameObject obj = Instantiate(spawnObj);
        obj.transform.position = gameObject.transform.position;
        timer -= spawnInterval;
        if(!isLimitedSpawn) counter++;
    }
}
