using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UIElements;

public class MapGenerator : MonoBehaviour
{
    public int loadRadius = 1000;
    public int distanceBetweenTiles = 1000;
    public GameObject tilePrefab;
    public GameObject testPrefab;
    public Transform playerTransform;
    private Vector3 lastCiclePosition;
    private float density = 0.2f;

    void Start()
    {
        lastCiclePosition = playerTransform.position;
        fillProximityBox(); 
    }

    // Update is called once per frame
    void Update()
    {
        cicleX(checkForCicleX());
    }

    void fillProximityBox()
    {
        int dimention = (int)(loadRadius / distanceBetweenTiles / 2f);
        for (int i = -dimention; i < dimention; i++)
        {
            for(int j = -dimention; j < dimention; j++)
            {
                for(float k = -dimention; k < dimention; k++)
                {
                    Vector3 tileLocation = new Vector3(i * distanceBetweenTiles, j * distanceBetweenTiles, k * distanceBetweenTiles);
                    GameObject newTile = Instantiate(tilePrefab, tileLocation, Quaternion.identity);
                    newTile.GetComponent<TileChunk>().fillTile(density);
                }
            }
        }
    }

    int checkForCicleX()
    {
        float deltaToLastCycle = playerTransform.position.x - lastCiclePosition.x;
        if (Mathf.Abs(deltaToLastCycle) > distanceBetweenTiles)
        {
            lastCiclePosition.x = playerTransform.position.x;
            return (int)((deltaToLastCycle) / distanceBetweenTiles);
            
        }
        else return 0;
    }
    void cicleX(int direction)
    {
        if (direction != 0)
        {
            int dimention = (int)(loadRadius / distanceBetweenTiles / 2f);
            for (int i = -dimention; i <= dimention; i++)
            {
                for (int j = -dimention; j <= dimention; j++)
                {
                    Vector3 tileLocation = new Vector3(playerTransform.position.x + loadRadius * direction, distanceBetweenTiles * i, distanceBetweenTiles * j);
                    //GameObject newSphere = Instantiate(testPrefab, tileLocation, Quaternion.identity);
                    GameObject newTile = Instantiate(tilePrefab, tileLocation, Quaternion.identity);
                    newTile.GetComponent<TileChunk>().fillTile(density);
                    
                }
            }
            recicleX(direction, dimention);
        }
    }
    void recicleX(int direction, int dimention)
    {
        List<GameObject> gameObjectsInRecicleZone = Physics.OverlapBox(playerTransform.position - Vector3.right * direction * (loadRadius + distanceBetweenTiles * 2f)
            , new Vector3(distanceBetweenTiles, loadRadius, loadRadius)).Select(c => c.gameObject).ToList();
        foreach (GameObject gameObject in gameObjectsInRecicleZone)
        {
            if (gameObject.tag == "tileChunk")
            {
                gameObject.GetComponent<TileChunk>().destroyTileChunk();
                Destroy(gameObject);
            }
        }
    }
    

    

    public static float getPerlin3D(float x, float y, float z)
    {
        float noiseSampleDistance = 1f / 10;
        float AB = Mathf.PerlinNoise(x * noiseSampleDistance, y * noiseSampleDistance);
        float BC = Mathf.PerlinNoise(y * noiseSampleDistance, z * noiseSampleDistance);
        float AC = Mathf.PerlinNoise(x * noiseSampleDistance, z * noiseSampleDistance);

        float BA = Mathf.PerlinNoise(y * noiseSampleDistance, x * noiseSampleDistance);
        float CB = Mathf.PerlinNoise(z * noiseSampleDistance, y * noiseSampleDistance);
        float CA = Mathf.PerlinNoise(z * noiseSampleDistance, x * noiseSampleDistance);

        float ABC = AB + BC + AC + BA + CB + CA;
        return ABC / 6f ;
    }
    public static float getPerlin3D(Vector3 position)
    {
        return getPerlin3D(position.x, position.y, position.z);
    }
}
