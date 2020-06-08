using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CannoBallDetector : MonoBehaviour
{
    public NavMeshAgent nav;
    public Transform thisShip;
    public Transform targetShip;

    Vector3 shipPos;
    Vector3 avoidPos;

    public float radius = 100f;
    public float speed = 25f;

    // Start is called before the first frame update
    void Start()
    {
        shipPos = thisShip.position; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*private void Avoid()
    {
        Vector3 pos = GetPositionAroundObject();
    
    }  */

    Vector3 GetPositionAroundObject()
    {
        Vector3 offset = Random.insideUnitCircle * radius;
        Vector3 pos = targetShip.position + offset;
        Debug.Log(pos);
     //   avoidPos = pos;
        return pos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyCannon")
        {
            Debug.Log("Incoming!!");
            nav.speed = 5f;
            nav.angularSpeed = 20f;
            nav.SetDestination(GetPositionAroundObject() * speed * Time.deltaTime);
            
        }
        else
        {
           // nav.SetDestination(targetShip.position * speed * Time.deltaTime);
        }
    }

    //for debugging visuals, shows enemy radius projectile range
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }


}
