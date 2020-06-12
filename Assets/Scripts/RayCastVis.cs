using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastVis : MonoBehaviour
{
    public float rayDistance = 400f;
    public Transform rayStartPos;
    private RaycastHit detectorRay;

    DefenceAI defAi;
    public GameObject defenceObject;

    // Start is called before the first frame update
    void Start()
    {
        defAi = defenceObject.GetComponent<DefenceAI>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHandler();
    }

    void RaycastHandler()
    {
        Debug.DrawRay(rayStartPos.position, rayStartPos.forward * rayDistance, Color.cyan, 1f);

        if (Physics.Raycast(rayStartPos.position, rayStartPos.forward, out detectorRay, rayDistance))
        {
            if(detectorRay.collider.tag == "Looter")
            {
                Debug.Log(detectorRay.collider.name);
                defAi.Attack();

                
            }
            else
            {
                defAi.Patrol();
            }
        }

    }
}
