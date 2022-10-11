using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TileChunk : MonoBehaviour
{
    public GameObject spawner;
    public GameObject smallCube;
    public GameObject bigCube;
    public GameObject smallPlane;
    public GameObject bigPlane;
    public int distanceBetweenTiles = 1000;
    public float densityQuotient = 60000000;
    private List<GameObject> boxesInTile = new List<GameObject>();
    private Dictionary<Enum, float> boxProbabilityes = new Dictionary<Enum, float>()
    {
        {TileChunkEnum.SMALL_CUBE, 0.8f},
        {TileChunkEnum.BIG_CUBE, 0.3f},
        {TileChunkEnum.SMALL_PLANE, 0.4f},
        {TileChunkEnum.BIG_PLANE, 0.3f},

    };

    private void Start()
    {
        smallCube = Resources.Load("prefabs/SmallCube") as GameObject;
        bigCube = Resources.Load("prefabs/BigCube") as GameObject;
        smallPlane = Resources.Load("prefabs/SmallPlane") as GameObject;
        bigPlane = Resources.Load("prefabs/BigPlane") as GameObject;
        
}


    public void fillTile(float density)
    {
        float volume = Mathf.Pow(distanceBetweenTiles, 2);
        int boxAmount = (int)(volume * Mathf.Pow( 2 * density, 3) / densityQuotient);
        for (int i = 0; i < boxAmount; i++)
        {
            foreach (Enum box_enum in boxProbabilityes.Keys)
            {
                Vector3 randomPoint = transform.position + Random.insideUnitSphere * distanceBetweenTiles;
                if (Random.value < boxProbabilityes.GetValueOrDefault(box_enum) * MapGenerator.getPerlin3D(randomPoint / 10f))
                {
                    boxesInTile.Add(Instantiate(getBoxFromEnum(box_enum), randomPoint, Random.rotation));
                }
            }
        }

    }
    GameObject getBoxFromEnum(Enum box_enum)
    {
        switch (box_enum)
        {
            case TileChunkEnum.SMALL_CUBE:
                return smallCube;

            case TileChunkEnum.BIG_CUBE:
                return bigCube;

            case TileChunkEnum.SMALL_PLANE:
                return smallPlane;

            case TileChunkEnum.BIG_PLANE:
                return bigPlane;
        }
        return null;
    }

    public void destroyTileChunk()
    {
        foreach(GameObject box in boxesInTile)
        {
            Destroy(box);
        }
        Destroy(this);
    }

    enum TileChunkEnum
    {
        SMALL_CUBE,
        BIG_CUBE,
        SMALL_PLANE,
        BIG_PLANE,
        TILE_CHUNK_AMOUNT
    }
}
