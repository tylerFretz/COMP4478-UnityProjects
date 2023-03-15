using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonSpawnerScript : MonoBehaviour
{
    public GameObject cannonPrefab;

    private float spawnInterval = 2f;
    private float spawnTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        cannonPrefab.layer = LayerMask.NameToLayer("Attacking");
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTimer < spawnInterval)
        {
            spawnTimer += Time.deltaTime;
        }
        else
        {
            spawnInterval = Random.Range(11.5f, 15.5f);
            spawnTimer = 0;
            spawnCannon();
        }
    }

    void spawnCannon()
    {
        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Instantiate(cannonPrefab, spawnPosition, Quaternion.Euler(0, 0, 0));
    }
}
