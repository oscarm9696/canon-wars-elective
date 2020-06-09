using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class LooterAI : MonoBehaviour
{
    public Transform ship;
    public Transform endGoal;
    public GameObject player;

    public Transform[] enemy;

    public float beginFireRadius;
    public float detectEnemyRange;
    public Transform target;
    public NavMeshAgent enemyNav;
    public LayerMask layer;
    public ParticleSystem pSImpact;
    public ParticleSystem pSImpact2;
    public ParticleSystem canonSmoke;
    public ParticleSystem seriousDamage;

   // public Mesh waterArea;


    public float coolDown = 0f;
    bool cooledDown = false;

    float distance;
    public float avoidDistance;
    public float speed = 500f;

    public float radius1;
    public Vector3 randomPosinRadius;
    public float headStart;
    float leftToPos; //how much is left t random pos
    bool reachedPos;
    bool avoiding;

    public float aiHealth;
    public float damgeTaken = 1f;
    public float curHealth;
    public float impactDamage;
    bool noHealth;

    public Image healthSlider;


    // Start is called before the first frame update
    void Start()
    {
        curHealth = aiHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(randomPosinRadius);
        Debug.Log(leftToPos);
        EnemyNav();

        if(aiHealth <= 60f)
        {
            seriousDamage.Play();
            coolDown = 3f; 

            //increase enemy difficuty - speed - accuracy - shoot time 
            //reduce damage taken
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
            Debug.Log(aiHealth);

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
     
          enemyNav.baseOffset -= impactDamage;
    }

    //for debugging visuals, shows detect enemy range
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, detectEnemyRange);
    }

    IEnumerator GetRandomPos(float time)
    {
       // enemyNav.SetDestination(target.position);
        reachedPos = false;


        yield return new WaitForSeconds(time);

        Vector3 offset = Random.insideUnitCircle * beginFireRadius;
        randomPosinRadius = endGoal.position + offset;
        enemyNav.SetDestination(randomPosinRadius * speed * Time.deltaTime);

        reachedPos = true;
    }

  /*  public Vector3 GetARandomTreePos()
    {

        Bounds bounds = waterArea.bounds;

        float minX = gameObject.transform.position.x - gameObject.transform.localScale.x * bounds.size.x * 0.5f;
        float minZ = gameObject.transform.position.z - gameObject.transform.localScale.z * bounds.size.z * 0.5f;

        Vector3 newVec = new Vector3(Random.Range(minX, -minX),
                                     gameObject.transform.position.y,
                                     Random.Range(minZ, -minZ));
        return newVec;
    } */


    void EnemyNav()
    {
        //Vector3 offset = Random.insideUnitCircle * radius1;
        //randomPosinRadius = target.position + offset;
        enemyNav.SetDestination(target.position * speed * Time.deltaTime);

        distance = Vector3.Distance(target.position, transform.position);
        if (!cooledDown)
        {
            coolDown += Time.deltaTime;

            if (distance <= beginFireRadius && coolDown > 5f)
            {
                cooledDown = true;
                coolDown = 0f;
                Debug.Log("shot");
            }
        }
        cooledDown = false;

        if(distance <= detectEnemyRange)
        {
            Debug.Log("avodiing");
            avoiding = true;
            reachedPos = false;
         //   enemyNav.SetDestination(AvoidOffset());
            enemyNav.speed = 15f;
            enemyNav.angularSpeed = 35f;
        }
    }

    Vector3 AvoidOffset()
    {
        Vector3 offsetPos = enemy[0].position + enemy[1].position;
        return offsetPos;
        
    }

}
