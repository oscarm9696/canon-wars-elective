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
    public float rotSpeed;

    public float minT;
    public float maxT;

    public float[] gusts;

    private bool isGusting = false;
    public ParticleSystem impactParticle;

    FloatingBehaviour floating;
    PerlinAnimator wave;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        floating = GetComponent<FloatingBehaviour>();
        wave = gameObject.GetComponent<PerlinAnimator>();
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
        rb.transform.Rotate(Vector3.back, hor * rotSpeed * Time.deltaTime);

        if (!isGusting)
        {
            StartCoroutine(ApplyWindGusts(Random.Range(minT, maxT)));
        }

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

    IEnumerator ApplyWindGusts(float time)
    {
        isGusting = true;
        Debug.Log(turnSpeed);
        Debug.Log(accSpeed);
        yield return new WaitForSeconds(time);
        accSpeed = Random.Range(250f, 450f);
        turnSpeed = Random.Range(2f, 3f);
        //floating.waterDensity = Random.Range(0f, 4f);
        //wave.waveHeight = Random.Range(10f, 20f);
       
        rb.AddForce(transform.forward * accSpeed * Time.deltaTime);


        isGusting = false;

    }
}
                                                                                                  