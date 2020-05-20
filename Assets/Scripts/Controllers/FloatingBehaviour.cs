using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//behaviour to control the floating effect for the ships on the mesh
//requires rigidby ensures it is alwasy attached 
[RequireComponent(typeof(Rigidbody))]
public class FloatingBehaviour : MonoBehaviour
{
    public GameObject waterLevel;
    public GameObject pivotPoint;

    public float waterDensity;          //higher values here mean the water will behave less dense 
    public float waterDisplacement;     //higher values make the floating effect less springy/wavy
    public float gravity;               //the force applied down toward to water

    public float boatDisplace;
    float force;
    Vector3 forceGravity;

    //public ProceduralGrid procedural;

    // Start is called before the first frame update
    void Start()
    {
        // waterHeight = waterLevel.transform.position.y;
       // procedural = waterLevel.GetComponent<ProceduralGrid>();
    }

    // Update is called once per frame
    void Update()
    {
        waterDensity = Random.Range(1f,4f);

        //creates an acting force towards the water to mimic boat weight
        force = 1.0f - ((transform.position.y - boatDisplace) / waterDensity) * Time.deltaTime;
        
        //gets transform of mesh and checks if force is greater than its height
        //used to push back against the boat, which creates a floating behaviour
        if(force > waterLevel.transform.position.y)
        {
            forceGravity = -Physics.gravity * (force - (GetComponent<Rigidbody>().velocity.y * waterDisplacement) * Time.deltaTime);
            forceGravity += new Vector3(0.0f, -gravity, 0.0f);
            GetComponent<Rigidbody>().AddForceAtPosition(forceGravity, transform.position);

        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "EnemyCannon")
        {
            Debug.Log("Took Damage");
            //impactParticle = Instantiate(impactParticle, transform.position, Quaternion.FromToRotation(Vector3.up, impactNormal)) as ParticleSystem;
            //impactParticle.Play();


            // Destroy(gameObject);
        }
    }

}
