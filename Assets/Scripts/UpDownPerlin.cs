using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownPerlin : MonoBehaviour
{ 
    public float perlinNoise = 0f;
    public float curTime = 0f;
    public float multiplier = 5f;

    // Update is called once per frame
    void Update()
    {
        //ChangePerlin();
        ApplyPerlin();
    }
    void ApplyPerlin()
    {
        curTime = Time.time / 2; // slows down the effect by dividing time by 2
        perlinNoise = Mathf.PerlinNoise(0, curTime); //uses 0 and curtime to get a always changing float vlaue from above line

        //transform.position = new Vector3(0f, perlinNoise * multiplier, 0f);
        //transform.localPosition = new Vector3(0, perlinNoise * multiplier, 0);

    }

    public void ChangePerlin(float newValue)
    {
        multiplier = 10 * newValue;
    }
}
