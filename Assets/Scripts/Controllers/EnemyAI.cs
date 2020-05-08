using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform ship;
    public Rigidbody cannonBall;
    public GameObject player;
    public float shootTime;

    public float beginFireRadius;
    public Transform target;
    public NavMeshAgent enemyNav;
    public LayerMask layer;

    public float coolDown = 0f;
    bool cooledDown = false;

    float distance;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        enemyNav.SetDestination(target.position);

        distance = Vector3.Distance(target.position, transform.position);
        if (!cooledDown)
        {
            coolDown += Time.deltaTime;

            if (distance <= beginFireRadius && coolDown > 5f)
            {
                FireCannon();
                cooledDown = true;
                coolDown = 0f;
                Debug.Log("shot");
            }
        }
        cooledDown = false;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, beginFireRadius);
    }


    void FireCannon()
    {
        //calculates position in the world of mouse cursor 

        Vector3 calcVelo = calcVelocity(target.position, transform.position, 1f);
        //transform.rotation = Quaternion.LookRotation(calcVelo);
        Rigidbody rb = Instantiate(cannonBall, ship.position, Quaternion.identity);
        rb.velocity = calcVelo;

        Debug.Log("");
      
    }

    //calculating velocity based on x, y, z values in relation to time
    Vector3 calcVelocity(Vector3 playerTarget, Vector3 originP, float time)
    {
        time = shootTime;

        Vector3 dist = playerTarget - originP;
        Vector3 distXZ = dist;
        Vector3 calculation;
        distXZ.y = 0.0f;

        float projectileHeight = dist.y;
        float projectileDist = distXZ.magnitude;
        float velocityDist = projectileDist / time;
        float velocityVert = projectileHeight / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;

        calculation = distXZ.normalized;
        calculation *= velocityDist;
        calculation.y = velocityVert;

        return calculation;
    }

    Vector3 CalculateTragectory(Vector3 vo, float time)
    {
        Vector3 Vxz = vo;
        Vxz.y = 0.0f;
        Vector3 result = ship.position + vo * time;
        float sY = (-0.5f * Mathf.Abs(Physics.gravity.y) * (time * time)) + (vo.y * time) + ship.position.y;

        result.y = sY;

        return result;
    }
}
