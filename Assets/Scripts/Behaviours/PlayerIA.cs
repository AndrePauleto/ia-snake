using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AIBehaviours/PlayerIA")]
public class PlayerIA : AIBehaviour
{
    private bool debug = true;
    public override void Execute()
    {
        if (debug)
        {
            getBestOrb();
            debug = false;
        }
        
    }

    private GameObject getBestOrb()
    {
        GameObject[] orbs = GameObject.FindGameObjectsWithTag("orb");
        int indexSmallerOrb = 0;
        float smallerDistance = Mathf.Infinity;

        for (int i = 0; i < orbs.Length; i++)
        {
            float distanceOrb = Vector3.Distance(orbs[i].transform.position, this.owner.transform.position);

            if (distanceOrb < smallerDistance)
            {
                indexSmallerOrb = i;
                smallerDistance = distanceOrb;
            }
        }

        Debug.Log(orbs[indexSmallerOrb].name);
        return orbs[indexSmallerOrb];
    }
}
