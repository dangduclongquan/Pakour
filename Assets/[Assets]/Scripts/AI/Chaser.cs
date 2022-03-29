using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

public class Chaser : MonoBehaviour
{
    [SerializeField] NavMeshAgent navMeshAgent;
    [SerializeField] PhotonView photonview;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        // if(!photonview.AmController)
        // {
        //     navMeshAgent.enabled = false;
        //     return;
        // }
        // else
        // {
        //     navMeshAgent.enabled = true;
        // }

        // if (Survivor.survivors.Length == 0) return;

        
        // Survivor target= Survivor.survivors[0];
        // foreach (Survivor survivor in Survivor.survivors)
        // {
        //     if(Vector3.Distance(target.transform.position, transform.position)< Vector3.Distance(target.transform.position, transform.position))
        //     {
        //         target = survivor;
        //     }
        // }
        

        // if (Vector3.Distance(transform.position, target.transform.position)>=2)
        //     navMeshAgent.SetDestination(target.transform.position);
        // else
        //     PhotonNetwork.DestroyPlayerObjects(target.controller.ActorNumber, false);
    }
}
