using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CannoBallDetector : MonoBehaviour
{
   /* public NavMeshAgent nav;
    public Mesh waterArea;
    public Transform thisShip;
    public Transform[] targetShip;
    [HideInInspector]
    public Transform target;


    Vector3 shipPos;
    Vector3 avoidPos;

    public float radius = 300f;
    public float speed = 25f;

    // Start is called before the first frame update
    void Start()
    {
        //shipPos = thisShip.position; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //had intention to use this as a secondry avoid behaviour except
    //for in closer combat siutations
    Vector3 GetPositionAroundObject()
    {
        Vector3 offset = Random.insideUnitCircle * radius;
        Vector3 pos = transform.position + offset;
        Debug.Log(pos);
     //   avoidPos = pos;
        return pos;
    }

    //detects cannon and sets nav to random pos generated below
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerCannon")
        {
            Debug.Log("Incoming!!");
            nav.speed = 20f;
            nav.angularSpeed = 40f;
            nav.SetDestination(GetARandomPos() * speed * Time.deltaTime);
  
            
        }

    }

    //gets a random pos within the water area mesh
    public Vector3 GetARandomPos()
    {

        Bounds bounds = waterArea.bounds;

        float minX = gameObject.transform.position.x - gameObject.transform.localScale.x * bounds.size.x * 0.5f;
        float minZ = gameObject.transform.position.z - gameObject.transform.localScale.z * bounds.size.z * 0.5f;

        Vector3 newVec = new Vector3(Random.Range(minX, -minX),
                                     gameObject.transform.position.y,
                                     Random.Range(minZ, -minZ));
        return newVec;
    }

    //for debugging visuals, shows enemy radius projectile range
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
            
        */

}
