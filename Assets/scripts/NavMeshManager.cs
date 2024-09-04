using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;

public class NavMeshManager : MonoBehaviour
{
    [SerializeField] NavMeshSurface navMeshSurface;

    void Start(){
        navMeshSurface.BuildNavMesh();
    }

    public void UpdateNavMesh()
    {
        navMeshSurface.BuildNavMesh();
    }
}
