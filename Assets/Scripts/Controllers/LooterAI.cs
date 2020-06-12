using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class LooterAI : MonoBehaviour
{
    public Transform ship;
    public Transform endGoal;

    public Rigidbody rb;

    public Transform[] gotoPoints;
    public Transform[] enemy;
    public Transform centre;

    Transform closestShip;
    Vector3 curPos;

    public float detectEnemyRange;
    public NavMeshAgent nav;
    public LayerMask layer;
    public ParticleSystem pSImpact;
    public ParticleSystem pSImpact2;
    public ParticleSystem seriousDamage;

    public GameObject checkPointD;
    public GameObject cannonD;
    private CheckPointCol check;
    private CannoBallDetector cannonDetector;

    public Transform rayStartPos;
    private RaycastHit detectorRay;
    public float rayDistance;

    float distance;
    float time = 0f;
    public float avoidDistance;
    public float allowedMoveTime;
    public float waitTime;
    public float avoidTime;
    public float speed;
    private int enemyId;
    public bool isAvoiding;

    public float radius1;
    Vector3 randomPosinRadius;
    Vector3 tempAvoidPos;
    public float headStart;
    

    [HideInInspector]
    public int curLoot = 0;

    public float aiHealth;
    public float damgeTaken = 1f;
    public float curHealth;
    public float impactDamage;

    bool noHealth;
    bool reachedLoot;
    //bool avoiding;

    public Image healthSlider;


    // Start is called before the first frame update
    void Start()
    {
        tempAvoidPos = GetRandomPoint(centre.position, 500f);
        isAvoiding = false;
      //  curLoot = Random.Range(0, 3);
        curHealth = aiHealth;
        check = checkPointD.GetComponent<CheckPointCol>();
        cannonDetector = cannonD.GetComponent<CannoBallDetector>();
        allowedMoveTime = Random.Range(10f, 20f);
        waitTime = Random.Range(1f, 5f);

   
    }

    void Update()
    {
        isAvoiding = cannonDetector.isAvoidingCannon;
        curPos = transform.position;
        CheckIfStuck();
       // RaycastHandler();
        Nav();
       // curGoTo = gotoPoints[curLoot].position;

        if (aiHealth <= 60f)
        {
            seriousDamage.Play();
        }
       // Debug.Log("cur loot point = " + gotoPoints[curLoot]);

        if (reachedLoot)
        {
         //   Debug.Log("Reached loot: " + reachedLoot);
            curLoot = curLoot + 1;

        }
    }

    void CheckIfStuck()
    {
        Vector3 vel = rb.velocity;
        if (vel.magnitude <= .5)
        {
            Debug.Log("Stuck");
            nav.SetDestination(randomPosinRadius);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "PlayerCannon")
        {
            aiHealth -= damgeTaken;
            pSImpact.Play();                                                                                                                                          
            SinkShip();
            healthSlider.fillAmount -= .01F;
            isAvoiding = true;
            //Debug.Log(aiHealth);

            if(aiHealth <= 80)
            {
                pSImpact2.Play();
            }
        }

    }


    private void HardDifficulty()
    {
        aiHealth = 125;
        damgeTaken = .5f;
    }
    private void MediumDifficulty()
    {
        aiHealth = 125;
        damgeTaken = 1f;
    }
    private void EasyDifficulty()
    {
        aiHealth = 125;
        damgeTaken = 2f; 
    }

    void SinkShip()
    {
     
          nav.baseOffset -= impactDamage;
    }

    //for debugging visuals, shows detect enemy range
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, detectEnemyRange);
    }


    public IEnumerator GetRandomPos(float time)
    {
        
        yield return new WaitForSeconds(time);

        Vector3 tempGoTo = GetRandomPoint(gotoPoints[curLoot].position, 300f);
        nav.SetDestination(tempGoTo);
        //Vector3 offset = Random.insideUnitCircle * 600;
        //randomPosinRadius = ship.position + offset;
        //nav.SetDestination(randomPosinRadius * speed * Time.deltaTime);

    }

    void Nav()
    {

        distance = Vector3.Distance(GetClosestEnemy().position, transform.position);
        if(distance >= detectEnemyRange && !isAvoiding && cannonDetector.isAvoidingCannon == false)
        {
            time += Time.deltaTime;
            Debug.Log("Is not avoiding anything");
            nav.SetDestination(gotoPoints[curLoot].position);

            if(time >= allowedMoveTime && !isAvoiding)
            {
                time = 0f;
                time += Time.deltaTime;
                nav.isStopped = true;
                Debug.Log("Looter: waiting");

                
            }
            if (time >= waitTime && nav.isStopped && !isAvoiding)
            {
                time = 0f;
                Debug.Log("finished waiting");
                nav.isStopped = false;
                StartCoroutine(GetRandomPos(15f));
            }
        }

        else  
        {
            nav.SetDestination(tempAvoidPos);
            nav.speed = 45f;
   
            Debug.Log("Is avoiding");

        }
    }

    // Get Random Point on a Navmesh surface
    public static Vector3 GetRandomPoint(Vector3 center, float maxDistance)
    {
        // Get Random Point inside Sphere which position is center, radius is maxDistance
        Vector3 randomPos = Random.insideUnitSphere * maxDistance + center;

        NavMeshHit hit; // NavMesh Sampling Info Container

        // from randomPos find a nearest point on NavMesh surface in range of maxDistance
        NavMesh.SamplePosition(randomPos, out hit, maxDistance, NavMesh.AllAreas);

        return hit.position;
    }

    //loops through array enemy to find clos
    public Transform GetClosestEnemy()
    {
        Transform closestShip = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Transform t in enemy)
        {
            float dist = Vector3.Distance(t.position, currentPos);
            if (dist < minDist)
            {
                closestShip = t;
                minDist = dist;
            }
        }
        return closestShip;
    }


}
