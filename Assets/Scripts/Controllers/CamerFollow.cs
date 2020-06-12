using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerFollow : MonoBehaviour
{
    public float lookSpeed = 3;
    private Vector3 rotation;

    private void Update()
    {
        Look();
    }
    public void Look() // Look rotation (UP down is Camera) (Left right is Transform rotation)
    {
       // rotation.y += Input.GetAxis("Mouse X") * lookSpeed * Time.deltaTime;
      //  rotation.y -= Input.GetAxis("Mouse X") * lookSpeed * Time.deltaTime;
      //  transform.rotation = Quaternion.Euler(rotation.x, rotation.y, 0);
        //rotation.x = Mathf.Clamp(rotation.x, -280f, 280f);
        //rotation.x = Mathf.Clamp(-280, rotation.y, 280f);
        // transform.eulerAngles = new Vector2(0, rotation.y) * lookSpeed;
        //Camera.main.transform.localRotation = Quaternion.Euler(0, rotation.y * lookSpeed, 0);
        //Camera.main.transform.localRotation = Quaternion.Euler(rotation.x * lookSpeed, 0, 0);
    }
}
