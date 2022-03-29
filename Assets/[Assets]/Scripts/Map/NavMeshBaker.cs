using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshBaker : MonoBehaviour
{
    NavMeshSurface[] navmeshsurfaces;
    private void Awake()
    {
        navmeshsurfaces = GetComponentsInChildren<NavMeshSurface>(true);
    }
    public void BakeMavMesh()
    {
        foreach(NavMeshSurface navmeshsurface in navmeshsurfaces)
            navmeshsurface.BuildNavMesh();
    }
}
