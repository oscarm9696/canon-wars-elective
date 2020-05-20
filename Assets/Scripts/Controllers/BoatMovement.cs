using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    private Rigidbody rb;

    public Vector3 centerOfGrav;
    Transform mCenterOfGrav;

    public float accSpeed;
    public float turnSpeed;

    public ParticleSystem impactParticle;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(ApplyWindGusts());
    }

    // Update is called once per frame
    void Update()
    {
        move();
        ApplyCOG();   //center of gravity
    }

    void move()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");


        rb.AddTorque(0f, hor * turnSpeed * Time.deltaTime, 0f);
        rb.AddForce(transform.forward * accSpeed * Time.deltaTime);

    }

    
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

    IEnumerator ApplyWindGusts()
    {
   
            Debug.Log("Gust1");
            accSpeed += 40.0f;
            yield return new WaitForSeconds(Random.Range(0.0f, 30.0f));
        
        
    }
}
