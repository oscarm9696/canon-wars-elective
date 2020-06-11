using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class LooterAI : MonoBehaviour
{
    public Transform ship;
    public Transform endGoal;

    public Transform[] gotoPoints;
    public Transform[] enemy;

    Transform closestShip;

    public float detectEnemyRange;
    public NavMeshAgent nav;
    public LayerMask layer;
    public ParticleSystem pSImpact;
    public ParticleSystem pSImpact2;
    public ParticleSystem seriousDamage;

    public GameObject checkPointD;
    private CheckPointCol check;



    float distance;
    public float avoidDistance;
    public float speed;
    private int enemyId;

    public float radius1;
    public Vector3 randomPosinRadius;
    public float headStart;
    

    [HideInInspector]
    public int curLoot = 0;

    public float aiHealth;
    public float damgeTaken = 1f;
    public float curHealth;
    public float impactDamage;

    bool noHealth;
    bool reachedLoot;
    bool avoiding;

    public Image healthSlider;


    // Start is called before the first frame update
    void Start()
    {
        curHealth = aiHealth;
        check = checkPointD.GetComponent<CheckPointCol>();
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log(GetClosestEnemy());
        Nav();
       // curGoTo = gotoPoints[curLoot].position;

        if (aiHealth <= 60f)
        {
            seriousDamage.Play();
        }
        Debug.Log("cur loot point = " + gotoPoints[curLoot]);

        if (reachedLoot)
        {
            Debug.Log("Reached loot: " + reachedLoot);
            curLoot = curLoot + 1;

        }
    }

    void CheckIfStuck()
    {
        Vector3 curPos;
        curPos = transform.position;

        if(curPos == transform.position)
        {
            Debug.Log("I am stuck");
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

    IEnumerator GetRandomPos(float time)
    {
        yield return new WaitForSeconds(time);

        Vector3 offset = Random.insideUnitCircle * avoidDistance;
        randomPosinRadius = gotoPoints[curLoot].position + offset;
       // enemyNav.SetDestination(randomPosinRadius * speed * Time.deltaTime);

    }

    void Nav()
    {
       // Debug.Log(curLoot);
        nav.SetDestination(gotoPoints[curLoot].position);

        distance = Vector3.Distance(enemy[0].position, transform.position);
  
        if(distance <= detectEnemyRange)
        {
            avoiding = true;
            nav.speed = 50f; //slight boost
            nav.angularSpeed = 190f;
            nav.SetDestination(AvoidOffset());

            Debug.Log("Offset is: " + AvoidOffset());
        }
    }

    Transform GetClosestEnemy()
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Transform t in enemy)
        {
            float dist = Vector3.Distance(t.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }

    IEnumerator GetAvoidPos(float time)
    {
        yield return new WaitForSeconds(time);

        Vector3 offsetPos = Random.insideUnitCircle * GetClosestEnemy().position;
   

    }

    Vector3 AvoidOffset()
    {
        StartCoroutine(GetAvoidPos(15));
        return AvoidOffset();
        
    }

}
