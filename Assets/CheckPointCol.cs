using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CheckPointCol : MonoBehaviour
{
    public GameObject looter;
    public NavMeshAgent nav;
    private LooterAI looterAi;

    public int lootPoint = 0;

    public Transform[] gotoPoints;

    public bool reachedLoot;

    private void Start()
    {
        looterAi = looter.GetComponent<LooterAI>();
      //  reachedLoot = false;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("LootPoint"))
        {
            looterAi.curLoot = +1;
            lootPoint += 1;

        }
    }
}
