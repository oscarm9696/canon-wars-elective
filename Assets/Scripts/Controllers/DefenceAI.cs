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

    public Vector3[] patrolPoints;

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

    public float aiHealth;
    public float damgeTaken = 1f;
    public float curHealth;
    public float aiDifficulty;
    public float ammunition;
    public float shootTime;
    public float impactDamage;
    bool noHealth;

    public Image healthSlider;


    // Start is called before the first frame update
    void Start()
    {
        curHealth = aiHealth;
   //     radius1 = beginFireRadius;

        timer = wanderTime;

    }


    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        origin = transform.position;

        EnemyNav();
        Avoid();

        shootTime = Random.Range(3f, 7f);


        if (aiHealth <= 60f)
        {
            seriousDamage.Play();
            shootTime = Random.Range(1.75f, 3.75f);
            coolDown = 3f;

            //increase enemy difficuty - speed - accuracy - shoot time 
            //reduce damage taken
        }
        if(timer >= wanderTime)
        {
            Debug.Log("wandering");
            Vector3 goTo = RandomNavSphere(transform.position, wanderRad, -1);
            enemyNav.SetDestination(goTo);
            timer = 0f;
        }
        else
        {
            Debug.Log("hunting");
            EnemyNav();
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "EnemyCannon")
        {
            /*aiHealth -= damgeTaken;
            pSImpact.Play();
            SinkShip();
            healthSlider.fillAmount -= .01F;
            Debug.Log(aiHealth);

            if (aiHealth <= 80)
            {
                pSImpact2.Play();
            }          */

        }
    }

    public Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }


    private void HardDifficulty()
    {
        aiHealth = 125;
        damgeTaken = .5f;
        shootTime = Random.Range(.5f, 1.5f);
    }


    private void MediumDifficulty()
    {
        aiHealth = 125;
        damgeTaken = 1f;
        shootTime = Random.Range(1.75f, 3.75f);
    }


    private void EasyDifficulty()
    {
        aiHealth = 125;
        damgeTaken = 2f;
        shootTime = Random.Range(1.75f, 5.75f);
    }


    void SinkShip()
    {

        enemyNav.baseOffset -= impactDamage;
    }


    //for debugging visuals, shows radius projectile range
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, beginFireRadius);
    }


    IEnumerator GetRandomPos(float time)
    {
        // enemyNav.SetDestination(target.position);
        reachedPos = false;


        yield return new WaitForSeconds(time);

        Vector3 offset = Random.insideUnitCircle * radius1;
        randomPosinRadius = target.position + offset;
        enemyNav.SetDestination(randomPosinRadius * speed * Time.deltaTime);

        reachedPos = true;
    }



    //center of gravity for boat, should be between -.5 - 0.0
   void ApplyCOG()
    {
        if (!mCenterOfGrav)
        {
            mCenterOfGrav = new GameObject("COG").transform;
            mCenterOfGrav.SetParent(transform);
        }
        mCenterOfGrav.position = centerOfGrav;
        GetComponent<Rigidbody>().centerOfMass = mCenterOfGrav.position;
    }     
    


    void EnemyNav()
    {
        Vector3 offset = Random.insideUnitCircle * radius1;
        randomPosinRadius = target.position + offset;
    

        if (!reachedPos)
        {
            StartCoroutine(GetRandomPos(50));
        }
        ApplyCOG();

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


    void FireCannon()
    {
        Vector3 calcVelo = CalcVelocity(target.position, transform.position, 1f);
        //transform.rotation = Quaternion.LookRotation(calcVelo);
        Rigidbody rb = Instantiate(cannonBall, ship.position, Quaternion.identity);
        rb.velocity = calcVelo;
        canonSmoke.Play();


    }

    void Avoid()
    {
        distance = Vector3.Distance(target.position, transform.position);
        if(distance <= retreatDistance)
        {
            enemyNav.SetDestination(randomPosinRadius * speed * Time.deltaTime);
        }
    }


    public void Patrol()
    {
        Debug.Log("patrolling");
        enemyNav.isStopped = false;
        if (patrolPoints.Length > 0)
        {
            enemyNav.SetDestination(patrolPoints[curPoint]);
            if (transform.position == patrolPoints[curPoint] || Vector3.Distance(transform.position, patrolPoints[curPoint]) < 0.2f)
            {
                curPoint++;    //use distance if needed(lower precision)
            }
            if (curPoint >= patrolPoints.Length)
            {
                curPoint = 0;
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
