using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class RandomCubeSpawner : MonoBehaviour
{
    public int cubeAmount;
    public Vector3 spawnDimentions;
    public Vector3 cubeDimentions;
    public GameObject spawnObject;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < cubeAmount; i++)
        {
            Vector3 randomPosition = new Vector3(Random.value * spawnDimentions.x, Random.value * spawnDimentions.y, Random.value * spawnDimentions.z);
            Instantiate(spawnObject, randomPosition, Random.rotation);
        }
    }

}
