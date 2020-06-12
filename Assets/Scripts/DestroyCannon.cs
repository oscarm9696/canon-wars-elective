using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCannon : MonoBehaviour
{
    public GameObject thisCannon;

    //destroys cannon ball on collision with sea floor
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SeaBed"))
        {
            Destroy(thisCannon);
           // Debug.Log("destroyed cannon");
        }
    }

}
