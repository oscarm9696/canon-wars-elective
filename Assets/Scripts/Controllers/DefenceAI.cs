using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class DefenceAI : MonoBehaviour
{
    public Transform ship;
    public Rigidbody cannonBall;
    public GameObject player;
    public Rigidbody rb;


    public float beginFireRadius;
    public Transform target;
    public NavMeshAgent enemyNav;
    public LayerMask layer;
    public ParticleSystem pSImpact;
    public ParticleSystem pSImpact2;
    public ParticleSystem canonSmoke;
    public ParticleSystem seriousDamage;

    public Vector3 centerOfGrav;
    Transform mCenterOfGrav;
    Vector3 origin;

    public Transform[] patrolPoints;

    public float coolDown = 0f;
    bool cooledDown = false;

    float distance;
    public float speed = 500f;

    private float timer;
    private int curPoint = 0;

    public float radius1;
    public float retreatDistance;
    public Vector3 randomPosinRadius;
    bool reachedPos;

    //for AI sight
    public float wanderRad;
    public float wanderTime;

    public float aiDifficulty;
    public float ammunition;
    public float shootTime;
    public float impactDamage;

    public float minShootTime;
    public float maxShootTime;
    bool noHealth;

    public Image healthSlider;


    // Start is called before the first frame update
    void Start()
    {
        Patrol();
        timer = wanderTime;
    }


    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        origin = transform.position;

        CheckIfStuck();

        EnemyNav();
       // Vector3 goTo = patrolPoints[curPoint].position;
       // enemyNav.SetDestination(goTo);


        shootTime = Random.Range(3f, 10f);


        if(timer <= wanderTime)
        {
            //Debug.Log("wandering");
           // Vector3 goTo = patrolPoints[curPoint].position;
           // enemyNav.SetDestination(goTo);
            timer = 0f;
        }
        else
        {
            // Debug.Log("hunting");
            
        }
    }


    void CheckIfStuck()
    {
        Vector3 vel = rb.velocity;
        if (vel.magnitude <= .5)
        {
            //Debug.Log("Stuck");
            enemyNav.SetDestination(patrolPoints[Random.Range(0, 7)].position);
        }
    }

    private void HardDifficulty()
    {
 
        shootTime = Random.Range(.5f, 1.5f);
    }


    private void MediumDifficulty()
    {

        shootTime = Random.Range(1.75f, 3.75f);
    }


    private void EasyDifficulty()
    {
     
        shootTime = Random.Range(1.75f, 5.75f);
    }




    //for debugging visuals, shows radius projectile range
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, beginFireRadius);
    }


    IEnumerator GetRandomPos(float time)
    {

        yield return new WaitForSeconds(time);

        Vector3 offset = Random.insideUnitCircle * radius1;
        randomPosinRadius = ship.position + offset;
        enemyNav.SetDestination(randomPosinRadius * speed * Time.deltaTime);

    }


    void EnemyNav()
    {

       // Vector3 offset = Random.insideUnitCircle * radius1;
        //randomPosinRadius = target.position + offset;
        Patrol();

        if (!reachedPos)
        {
            Vector3 offset = Random.insideUnitCircle * radius1;
            randomPosinRadius = ship.position + offset;
            //enemyNav.SetDestination(randomPosinRadius * speed * Time.deltaTime);
        }
        else
        {
            //Attack();
        }

        distance = Vector3.Distance(target.position, origin);
        if(distance <= beginFireRadius)
        {
            Attack();
            enemyNav.speed = 20f;
        }

        if (!cooledDown)
        {
            
            coolDown += Time.deltaTime;

            if (distance <= beginFireRadius && coolDown > 5f)
            {
                FireCannon();
                Attack();
                cooledDown = true;
                coolDown = 0f;
                Debug.Log("shot");
            }
        }
        cooledDown = false;
    }


    void FireCannon()
    {
        Vector3 calcVelo = CalcVelocity(target.position, transform.position, 1f);
       // transform.rotation = Quaternion.LookRotation(calcVelo);
        Rigidbody rb = Instantiate(cannonBall, ship.position, Quaternion.identity);
        rb.velocity = calcVelo;
       //enemyNav.speed = 0;
       //Attack();
        canonSmoke.Play();


    }

    public void Attack()
    {
        Debug.Log("Attacking");
        enemyNav.SetDestination(target.position);
        float distanceTemp = Vector3.Distance(target.position, transform.position);
        if (distanceTemp <= 40f)
        {
            enemyNav.isStopped = true;
        }
    }

    public void Patrol()
    {
      //  Debug.Log("patrolling");
        enemyNav.isStopped = false;
       // Debug.Log("curpoint= " + curPoint);

        if (patrolPoints.Length > 0)
        {
            enemyNav.SetDestination(patrolPoints[curPoint].position); //ensure defense ships alwasy start randomly

            if (transform.position == patrolPoints[curPoint].position || Vector3.Distance(transform.position, patrolPoints[curPoint].position) < 20f)
            {
                reachedPos = true;
                curPoint++;

                if (curPoint >= 8)
                {
                    curPoint = 0;
                    Debug.Log("reset curpoint" + curPoint);
                   // enemyNav.SetDestination(patrolPoints[curPoint].position);
                }

            }
           
        }

    }


    //calculating velocity of cannon ball based on x, y, z values in relation to time
    //using DST (distance / speed = time, distance / time = speed, time x speed = distance)
    //vert distance = max height (5f) / time * gravity 
    Vector3 CalcVelocity(Vector3 playerTarget, Vector3 originP, float time)
    {
        time = shootTime;

        Vector3 dist = playerTarget - originP;
        Vector3 distXZ = dist;
        Vector3 calculation;
        distXZ.y = 0.0f;

        float projectileHeight = dist.y + 5;
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
