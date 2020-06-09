using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CannoBallDetector : MonoBehaviour
{
    public NavMeshAgent nav;
    public Mesh waterArea;
    public Transform thisShip;
    public Transform[] targetShip;
    public Transform target;


    Vector3 shipPos;
    Vector3 avoidPos;

    public float radius = 300f;
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
        Vector3 pos = transform.position + offset;
        Debug.Log(pos);
     //   avoidPos = pos;
        return pos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerCannon")
        {
            Debug.Log("Incoming!!");
            nav.speed = 20f;
            nav.angularSpeed = 40f;
            nav.SetDestination(GetARandomTreePos() * speed * Time.deltaTime);
            OnDrawGizmos(GetARandomTreePos(), 50f);
            
        }
        else
        {
           // nav.SetDestination(targetShip.position * speed * Time.deltaTime);
        }
    }

    public Vector3 GetARandomTreePos()
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

    private void OnDrawGizmos(Vector3 t, float radius)
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(t, radius);
    }


}
