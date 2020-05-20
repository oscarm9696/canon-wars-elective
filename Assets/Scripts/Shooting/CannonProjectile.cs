using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonProjectile : MonoBehaviour
{
    public LineRenderer trajectory;
    public Transform ship;
    public Rigidbody cannonBall;
    public LayerMask layer;
    public GameObject cursorImpactP;
    public float shootTime;
    public int line;
    public Transform pivotP;
    public ParticleSystem pS;
    public ParticleSystem shootEffect;
    Vector3 pivotPoint;

    private Camera mCam;
    
    // Start is called before the first frame update
    void Start()
    {
        mCam = Camera.main;
       // trajectory.positionCount = line;
    }

    // Update is called once per frame
    void Update()
    {
        FireCannon();
    
    }

    //void Visualise(Vector3 vo)
   // {
       // for(int i = 0; i < line; i++)
       // {
     //       Vector3 pos = CalculateTragectory(vo, i / (float)line);
            //trajectory.SetPosition(i, pos);
     //   }
   // }

    /*public IEnumerator CamShake(float intensity, float time)
    {
        Quaternion origPos = ship.transform.rotation;
        float timer = 0.0f;
        float x;
        float y;

        while (timer < time)
        {
            ship.transform.Rotate(0.5f, 0f, -.05f);
            timer += Time.deltaTime;
            yield return null;  
        }
        ship.transform.rotation = origPos;
    } */

    void FireCannon()
    {
        //calculates position in the world of mouse cursor 
        Ray impactRay = mCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(impactRay, out hit, 1000f, layer))
        {
            cursorImpactP.SetActive(true);
            cursorImpactP.transform.position = hit.point + Vector3.up * 0.1f;

            Vector3 calcVelo = calcVelocity(hit.point, transform.position, 1f);
          //  Visualise(calcVelo);

            transform.rotation = Quaternion.LookRotation(calcVelo);
            if (Input.GetMouseButtonDown(0))
            {
                Rigidbody rb = Instantiate(cannonBall, ship.position, Quaternion.identity);
                rb.velocity = calcVelo;
                //pS.Play();
                shootEffect.Play();
               // ship.transform.RotateAround(pivotPoint, 5f);
            }
        }
        else
        {
            cursorImpactP.SetActive(false);
        }
    }
    
    //calculating velocity based on x, y, z values in relation to time
    Vector3 calcVelocity(Vector3 impactP, Vector3 originP, float time)
    {
        time = shootTime;

        Vector3 dist = impactP - originP;
        Vector3 distXZ = dist;
        Vector3 calculation;
        distXZ.y = 0.0f;

        float projectileHeight = dist.y;
        float projectileDist = distXZ.magnitude;
        float velocityDist = projectileDist / time;
        float velocityVert = projectileHeight / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;

        calculation = distXZ.normalized;
        calculation *= velocityDist;
        calculation.y = velocityVert;

        return calculation;
    }

    Vector3 CalculateTragectory(Vector3 velocity1, float time)
    {
        Vector3 Vxz = velocity1;
        Vxz.y = 0.0f;
        Vector3 result = ship.position + velocity1 * time;
        float sY = (-0.5f * Mathf.Abs(Physics.gravity.y) * (time * time)) + (velocity1.y * time) + ship.position.y;

        result.y = sY;

        return result;
    }
}
