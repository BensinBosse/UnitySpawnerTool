using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnHandeler : MonoBehaviour
{
    public GameObject enemy;
    public Vector3 cubePos;

    public GameObject player;

    public int maxSpawnCount;
    public int enemyCount;
    public float spawnDistance;
    Bounds bounds;

    private GameObject clone;


    bool enableSpawn = false;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");


        cubePos = this.transform.position;

        bounds = this.GetComponent<Collider>().bounds;
        for (int i = 0; i < maxSpawnCount; i++)
        {
            bounds = CalculateSpawn(bounds);
        }

    }

    private Bounds CalculateSpawn(Bounds bounds)
    {
        float x = Random.Range(-bounds.extents.x, bounds.extents.x);
        float y = Random.Range(-bounds.extents.y, bounds.extents.y);
        float z = Random.Range(-bounds.extents.z, bounds.extents.z);

        Vector3 spawnPos = new Vector3(x, y, z);


        clone = Instantiate(enemy, spawnPos + cubePos, Quaternion.identity);
        return bounds;
    }

    // Update is called once per frame
    void Update()
    {
        

        float distanceToSpawner = Vector3.Distance(cubePos, player.transform.position);

        if (distanceToSpawner < spawnDistance)
        { this.enableSpawn = true; }
        else { this.enableSpawn = false; }
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (enemyCount < maxSpawnCount && this.enableSpawn) 
        {
            bounds = CalculateSpawn(bounds);
        }

    }



}

//https://www.mineex.se
