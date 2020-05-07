using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtEnemy : MonoBehaviour
{
    public Transform enemy;
  
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(enemy);
    }
}
