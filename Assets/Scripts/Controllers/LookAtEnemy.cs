using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtEnemy : MonoBehaviour
{
    public List<Transform> targets;
    public Transform player;
    public Transform enemy;
    public Camera cam;


    public Vector3 origCamOffset;
    public Vector3 offset;
    Vector3 originalPos;

    private void Start()
    {
        originalPos = cam.transform.position;
    }
    // checks if player renderer is visible
    void LateUpdate()
    {
        transform.position = player.position + offset;
        transform.LookAt(enemy);
 
   
    }

    Vector3 GetPlayerPos()
    {
        if(targets.Count == 1)  //target 1 is the player
        {
            return targets[0].position;
        }

        var camBounds = new Bounds(targets[0].position, Vector3.zero);
        for(int i = 0; i <targets.Count; i++)
        {
            camBounds.Encapsulate(targets[i].position);
        }
        return camBounds.center;
            
    }
}
