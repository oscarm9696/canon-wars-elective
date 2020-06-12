using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject lootShip;
    private LooterAI looter;
    public Transform camPivot;
    public Transform target;
    public Transform player;

    public Vector3 camOffset;

    public bool offsetValues;

    public float rotSpeed;
    public float minAngle;
    public float maxAngle;

    // Start is called before the first frame update
    void Start()
    {
        looter = lootShip.GetComponent<LooterAI>();
        target = looter.GetClosestEnemy();

        if (!offsetValues)
        {
            camOffset = player.position - transform.position;
        }
        camPivot.transform.position = target.transform.position;

        //camPivot.transform.parent = target.transform;
        
        camPivot.transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        target = looter.GetClosestEnemy(); 

        camPivot.transform.position = target.transform.position;

        //x input affects y values on rotation 
        float xAxis = Input.GetAxis("Mouse X");
        camPivot.Rotate(0, xAxis * rotSpeed, 0);
        //player.Rotate(0, xAxis * rotSpeed, 0);

        //inverting the y axis rotation so up is down and down is up
        float yAxis = Input.GetAxis("Mouse Y");
        camPivot.Rotate(-yAxis, 0, 0);

        //gets the players y and x rotational value and stores it in a float
        float targetYAxis = camPivot.eulerAngles.y;
        float targetXAxis = camPivot.eulerAngles.x;

        //provides the two float values above to a Quaternion(vector3 for rotations) to reposition tthe camera
        Quaternion xCamRotation = Quaternion.Euler(targetXAxis, targetYAxis, 0);
        transform.position = target.position - (xCamRotation * camOffset);

        //limits the up/down rotation of the cam
        if(camPivot.rotation.eulerAngles.x > maxAngle && camPivot.rotation.eulerAngles.x < 180f)
        {
            camPivot.rotation = Quaternion.Euler(maxAngle, 0, 0);
        }
        if(camPivot.rotation.eulerAngles.x > 180 && camPivot.rotation.eulerAngles.x < 360f + minAngle)
        {
            camPivot.rotation = Quaternion.Euler(360f, 0, 0);
        }


        transform.LookAt(target);
        if (transform.position.y < target.position.y)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - .5f, transform.position.z);
        }

    }
}
