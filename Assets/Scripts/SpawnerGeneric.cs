using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerGeneric : MonoBehaviour
{
    //List of currently available objects to spawn
    public List<GameObject> spawnableObjects;

    private void Start()
    {
        //Starts the timer
        StartCoroutine(SpawnTimer());
    }

    /// <summary>
    /// Generates rnd obj from available list, generates a rnd location and restarts timer
    /// </summary>
    private void SpawnThing()
    {
        //Randomly picks object and location, sets y axis manually
        int objIndexToSpawn = Random.Range(0, spawnableObjects.Count);
        Vector3 spawnPos = transform.position = Random.insideUnitSphere * 15;
        spawnPos.y = 0.5f;

        //Creates object and restarts timer
        GameObject obj = Instantiate(spawnableObjects[objIndexToSpawn], spawnPos, Quaternion.identity);
        StartCoroutine(SpawnTimer());
    }

    /// <summary>
    /// Starts the Spawn Timer
    /// </summary>
    /// <returns>IENumerator WaitForSeconds</returns>
    private IEnumerator SpawnTimer()
    {
        yield return new WaitForSeconds(5f);

        SpawnThing();
    }

}
