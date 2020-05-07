using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FloatingBehaviour))]
public class PlayerMovement : MonoBehaviour
{
    public Vector3 centerOfGrav;
    Transform mCenterOfGrav;

    public float speed;
    public float foward;
    public float steer;
    public float drag;
 

    float fMovement;
    float fInput;
    float hMovement;
    float hInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ApplyCOG();
        MoveBoat();
        SteerBoat();
    }

    void MoveBoat()
    {
        fInput = Input.GetAxis("Vertical");
        fMovement = Mathf.Lerp(fMovement, fInput, Time.deltaTime / drag);
        transform.Translate(0.0f, 0.0f, fMovement * speed);
        
    }

    void SteerBoat()
    {
        hInput = Input.GetAxis("Horizontal");
        hMovement = Mathf.Lerp(hMovement, hInput, Time.deltaTime / drag);
        transform.Translate(0.0f, hMovement * speed, 0.0f);
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
}
