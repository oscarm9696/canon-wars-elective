using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SumRotate : MonoBehaviour
{
    public GameObject sun;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        sun.transform.Rotate(0f, 0f, .1f);
    }
}
