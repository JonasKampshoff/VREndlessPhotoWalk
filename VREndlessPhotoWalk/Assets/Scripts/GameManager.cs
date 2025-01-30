using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private TrackController trackController;
    [SerializeField] private PlayerSpaceMovement movement;


    void Start()
    {
        trackController.SetUpTrack();
        movement.StartRun();
    }
}
