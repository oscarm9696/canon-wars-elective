using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeGenerator : MonoBehaviour
{
    public GameObject cube;

    public float perlinNoise = 0f;
    public float curTime = 0f;
    public float multiplier = 5f;

    public Vector3 offset;

    public Vector3[] spawnLocations;

    public int cubeAmount;
    public int Max, Min;

    // Start is called before the first frame update
    void Start()
    {
        //cube = new GameObject[cubeAmount];
        GenCubes();
    }
    private void Update()
    {
       // GeneratedPosition();
    }

    /*Vector3 GeneratedPosition()
    {
        int x, z;
        x = Random.Range(Min, Max);
        //y = Random.Range(Min, Max);
        z = Random.Range(Min, Max);
        //Debug.Log("generate random pos" + " " + x + " " + y + " " + z);
        return new Vector3(x, 0, z);
    } */

    //instatiating platforms from an array at random ranges(set in inspector) 
    void GenCubes()
    {   
       
       // for (int i = 0; i < cubeAmount; i++)
        //{
            for (int a = 0; a < spawnLocations.Length; a++)
            {
                Instantiate(cube, spawnLocations[a] + offset, Quaternion.identity);
               // i++;
                a++;
            }
        //} 
    }
            

    }

   /* void PerlinCube()
    {
        curTime = Time.time / 2; // slows down the effect by dividing time by 2
        perlinNoise = Mathf.PerlinNoise(0, curTime); //uses 0 and curtime to get a always changing float vlaue from above line

        cube.transform.position = new Vector3(perlinNoise * multiplier, 0f, perlinNoise * multiplier);
    }
    public void ChangePerlin(float newValue)
    {
        multiplier = 10 * newValue;
    }*/
  


