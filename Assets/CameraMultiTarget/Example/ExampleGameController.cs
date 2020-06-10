using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExampleGameController : MonoBehaviour
{
    public CameraMultiTarget multiTarget;
    public GameObject[] targets;

    private void Start() {
        
        multiTarget.SetTargets(targets);
        multiTarget.SetTargets(targets);
    }
}
