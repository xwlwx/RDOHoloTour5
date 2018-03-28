using HoloToolkit.Unity;
using UnityEngine;

public class Speech : MonoBehaviour
{
    public SpatialUnderstandingCustomMesh SpatialUnderstandingMesh;

    public void ToggleMesh()
    {
        SpatialUnderstandingMesh.DrawProcessedMesh = !SpatialUnderstandingMesh.DrawProcessedMesh;
    }

    public ObjectCollectionBoard board;

    public void ToggleCollectionBoard()
    {
        board.ToggleBoard();
    }

    public HologramManager holo;
    public void CreateCube()
    {
        holo.Create3DCube();
    }

    public void CreateSphere()
    {
        holo.Create3DSphere();
    }
}