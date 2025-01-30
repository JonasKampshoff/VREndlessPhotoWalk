using Unity.Collections;
using UnityEngine;
using UnityEngine.Splines;

public class PlayerSpaceMovement : MonoBehaviour
{
    [SerializeField] private float maxAcceleration = 1; // in m/s^2
    [SerializeField] private TrackController trackController;
    [SerializeField] private CharacterController characterController;

    [Header("Speed")]
    [SerializeField] public float aimedSpeed = 0; // in m/s
    [SerializeField] private float currentSpeed = 0; // in m/s

    [Header("CurrentSpline")]
    [SerializeField] private SplineContainer currentSplineContainer;
    [SerializeField] private float currentSplineLength = 0; // in m
    [SerializeField] private float currentSplineProgress = 0; // in m

    [SerializeField] private Vector3 lastPosition;
    [SerializeField] private Vector3 lastTangent;

    public void StartRun()
    {
        lastPosition = transform.position;
        NextTrack();
        lastTangent = currentSplineContainer.EvaluateTangent(0);
    }

    void Update()
    {
        //Calculate Speed
        float speedDif = aimedSpeed - currentSpeed;
        currentSpeed += Mathf.Min(Mathf.Abs(speedDif), maxAcceleration * Time.deltaTime) * Mathf.Sign(speedDif);

        //Calculate Progress
        currentSplineProgress += Time.deltaTime * currentSpeed;
        if (currentSplineProgress > currentSplineLength)
        {
            float deltaProgress = currentSplineProgress - currentSplineLength;
            currentSplineProgress = deltaProgress;
            NextTrack();
        }

        //Move CharactetController
        Vector3 newPosition = currentSplineContainer.EvaluatePosition(currentSplineProgress/currentSplineLength);
        Vector3 newTangent = currentSplineContainer.EvaluateTangent(currentSplineProgress / currentSplineLength);
        characterController.Move(newPosition-lastPosition);
        transform.Rotate(Vector3.up, Vector3.SignedAngle(lastTangent,newTangent,Vector3.up));
        lastPosition = newPosition;
        lastTangent = newTangent;

    }

    private void NextTrack()
    {
        currentSplineContainer = trackController.NextTrackController();
        currentSplineLength = currentSplineContainer.CalculateLength();
    }



}
