using UnityEngine;
using UnityEngine.Splines;

public class TrackSecion : MonoBehaviour
{
    public Transform nextSpawnPoint;
    public TrackController controller;
    public SplineContainer splineContainer;


    public void SpawnNext()
    {
        controller.SpawnNext();
    }

}
