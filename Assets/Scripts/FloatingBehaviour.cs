using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//behaviour to control the floating effect for the ships on the mesh
//requires rigidby ensures it is alwasy attached 
[RequireComponent(typeof(Rigidbody))]
public class FloatingBehaviour : MonoBehaviour
{
    public GameObject waterLevel;

    public float waterDensity;          //higher values here mean the water will behave less dense 
    public float waterDisplacement;     //higher values make the floating effect less springy/wavy
    public float gravity;               //the force applied down toward to water

    float waterHeight;
    float force;
    Vector3 forceGravity;

    public ProceduralGrid procedural;

    // Start is called before the first frame update
    void Start()
    {
        // waterHeight = waterLevel.transform.position.y;
        procedural = waterLevel.GetComponent<ProceduralGrid>();

        Debug.Log(waterHeight);
    }

    // Update is called once per frame
    void Update()
    {
        force = 1.0f - ((transform.position.y - waterHeight) / waterDensity);
        
        //gets transform of mesh and checks if force is greater than its height
        if(force > waterLevel.transform.position.y)
        {
            forceGravity = -Physics.gravity * (force - (GetComponent<Rigidbody>().velocity.y * waterDisplacement));
            forceGravity += new Vector3(0.0f, -gravity, 0.0f);
            GetComponent<Rigidbody>().AddForceAtPosition(forceGravity, transform.position);

        }
    }
}
