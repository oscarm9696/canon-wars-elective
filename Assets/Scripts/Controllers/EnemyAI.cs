using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
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

    public float coolDown = 0f;
    bool cooledDown = false;

    float distance;

    public float aiHealth;
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
        //ship.GetComponent<ParticleSystem>();
        curHealth = aiHealth;
        
    }

    // Update is called once per frame
    void Update()
    {
        EnemyNav();
        shootTime = Random.Range(3f, 7f);

        if(aiHealth <= 60f)
        {
            seriousDamage.Play();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "PlayerCannon")
        {
            aiHealth -= 1;
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

    private void IncreaseDifficulty()
    {
        aiHealth = 125;
        shootTime = Random.Range(1.75f, 3.75f);
    }

    void SinkShip()
    {
     
          enemyNav.baseOffset -= impactDamage;
    }

    //for debugging visuals, shows enemy radius projectile range in editor
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, beginFireRadius);
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
        enemyNav.SetDestination(target.position);
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
        Vector3 calcVelo = calcVelocity(target.position, transform.position, 1f);
        //transform.rotation = Quaternion.LookRotation(calcVelo);
        Rigidbody rb = Instantiate(cannonBall, ship.position, Quaternion.identity);
        rb.velocity = calcVelo;
        canonSmoke.Play();

      
    }

    //calculating velocity based on x, y, z values in relation to time
    Vector3 calcVelocity(Vector3 playerTarget, Vector3 originP, float time)
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
