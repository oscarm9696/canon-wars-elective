using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGeneration : MonoBehaviour
{
    public GameObject[] platformPrefabs;
    public GameObject[] terrain;
    public int arrayAmmount;

    // Start is called before the first frame update
    void Start()
    {
        GenPlatforms();
    }

    Vector3 GeneratedPosition()
    {
        int x, z;
        x = Random.Range(0, 30);
        z = Random.Range(0, 30);
        return new Vector3(x, 0, z);
    }

    void GenPlatforms()
    {
        //transform.position = Random.insideUnitCircle * 15;
        for(int i = 0; i < arrayAmmount; i++)
        {
            Quaternion rotation = Quaternion.Euler(90f, 0f, 0f);
            Instantiate(platformPrefabs[0], GeneratedPosition(), rotation);
        }
    }

    /*void GenTerrain()
    {
        for(int t = 0; t < terrain.Length; t++)
        {
            
        }
    } */
}
