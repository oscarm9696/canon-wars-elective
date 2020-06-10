using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidPointCheck : MonoBehaviour
{
    Vector3 midPoint;
    public Transform[] transPos;

    private void Start()
    {
 //       for (int i = 0; i < transPos.Length; i++)
 //       {
 //           transPos[i].position = boatPos[i].normalized;
 //           Debug.Log(transPos[i].position);
 //           i++;
 //       }
    }

    // Update is called once per frame
    void Update()
    {
        CheckForMidPoint();

    }

    void CheckForMidPoint()
    {
        midPoint = transPos[0].position + transPos[1].position + transPos[2].position / 3;
    }

}
