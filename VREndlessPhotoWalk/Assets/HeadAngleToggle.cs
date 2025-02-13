using UnityEngine;

public class HeadAngleToggle : MonoBehaviour
{
    [Header("References")]
    [Tooltip("Z.B. CenterEyeAnchor aus dem OVRCameraRig.")]
    public Transform headTransform;

    private OVRPassthroughLayer passthroughLayer;

    [Header("Angle Settings")]
    [Tooltip("Ab welchem Winkel (gegen Vector3.up) wird etwas ausgel�st?")]
    public float angleThreshold = 120f;

    [Tooltip("Ab welchem Winkel (gegen Vector3.up) wird etwas ausgel�st?")]
    public float fadeAngle = 5f;

    void Start()
    {
        if (!headTransform)
        {
            Debug.LogWarning("[HeadAngleToggle] Kein Head Transform gesetzt. Bitte im Inspector zuweisen!");
        }

        if(!TryGetComponent<OVRPassthroughLayer>(out passthroughLayer))
        {
            Debug.LogWarning("[HeadAngleToggle] Kein OVRPassthroughLayer vorhanden. Bitte im Inspector diesem Objekt zuweisen!");
        }
    }

    void Update()
    {
        if (!headTransform) return;

        // Winkel zwischen Kopf-Vorw�rtsrichtung und Vector3.up
        float angle = Vector3.Angle(headTransform.forward, Vector3.up);

        // Wenn Winkel zu gro� -> Kopf stark nach unten
        if (angle > angleThreshold)
        {
            passthroughLayer.hidden = false;

            if (angle < angleThreshold + fadeAngle)
                passthroughLayer.textureOpacity = (angle - angleThreshold) / fadeAngle;
            else
                passthroughLayer.textureOpacity = 1;

        }
        // Wenn Winkel wieder kleiner -> Kopf hebt sich
        else { 
            passthroughLayer.hidden = true;
        }
        
    }
}
