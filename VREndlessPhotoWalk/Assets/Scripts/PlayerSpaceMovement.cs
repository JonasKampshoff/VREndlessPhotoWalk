using Unity.Collections;
using UnityEngine;
using UnityEngine.Splines;

public class PlayerSpaceMovement : MonoBehaviour
{
    [SerializeField] private float maxAcceleration = 1; // in m/s^2
    [SerializeField] public float aimedSpeed = 0; // in m/s
    [SerializeField] private float currentSpeed = 0; // in m/s
    [SerializeField] private SplineAnimate animate;
    [SerializeField] private TrackController trackController;

    private float currentProgress = 0;

    private void Start()
    {
        NextTrack();
    }

    void Update()
    {
        float speedDif = Mathf.Max(aimedSpeed,0) - currentSpeed;
        currentSpeed += Mathf.Min(Mathf.Abs(speedDif),maxAcceleration * Time.deltaTime) * Mathf.Sign(speedDif);
        currentProgress += currentSpeed * Time.deltaTime;
        if (currentProgress > animate.Duration)
        {
            float deltaProgress = currentProgress - animate.Duration;
            currentProgress = deltaProgress;
            NextTrack();
        }
        animate.ElapsedTime = currentProgress;
    }

    private void NextTrack()
    {
        animate.Container = trackController.NextTrackController();
    }



}
