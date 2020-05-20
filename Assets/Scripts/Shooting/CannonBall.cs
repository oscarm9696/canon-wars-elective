using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    //public Rigidbody cannonBall;
     public ParticleSystem impactParticle;
    public Vector3 impactNormal;

    // Start is called before the first frame update
    void Start()
    {
        //cannonBall = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
    //    CheckForHit();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "EnemyCannon")
        {
            Debug.Log("Took Damage");
            impactParticle = Instantiate(impactParticle, transform.position, Quaternion.FromToRotation(Vector3.up, impactNormal)) as ParticleSystem;
            impactParticle.Play();
          

           // Destroy(gameObject);
        }
    }

    void CheckForHit()
    {

    }  
}
