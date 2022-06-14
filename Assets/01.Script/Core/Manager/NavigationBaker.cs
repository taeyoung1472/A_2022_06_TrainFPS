using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class NavigationBaker : MonoBehaviour
{
    public static NavigationBaker Instance;
    public void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    public void Build(NavMeshSurface surface)
    {
        surface.BuildNavMesh();
    }
}