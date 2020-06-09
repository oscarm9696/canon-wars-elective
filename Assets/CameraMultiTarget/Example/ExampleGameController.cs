using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExampleGameController : MonoBehaviour
{
    public CameraMultiTarget cameraMultiTarget;
    public Transform[] targets;

    private void Start() {
        
        cameraMultiTarget.SetTargets(targets);
        cameraMultiTarget.SetTargets(targets);
        }
}
