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

    [SerializeField] private float angle;

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
        newPosition += new Vector3(0, 0.9f, 0);
        Vector3 newTangent = currentSplineContainer.EvaluateTangent(currentSplineProgress / currentSplineLength);
        characterController.Move(newPosition-lastPosition);

        float newAngle = Vector3.SignedAngle(Vector3.forward, newTangent, Vector3.up);
        transform.Rotate(Vector3.up, newAngle - angle);
        lastPosition = newPosition;
        lastTangent = newTangent;
        angle = newAngle;

    }

    private void NextTrack()
    {
        currentSplineContainer = trackController.NextTrackController();
        currentSplineLength = currentSplineContainer.CalculateLength();
    }



}
