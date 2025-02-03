using UnityEngine;

public class HeadAngleToggle : MonoBehaviour
{
    [Header("References")]
    [Tooltip("Z.B. CenterEyeAnchor aus dem OVRCameraRig.")]
    public Transform headTransform;

    [Header("Angle Settings")]
    [Tooltip("Ab welchem Winkel (gegen Vector3.up) wird etwas ausgelöst?")]
    public float angleThreshold = 120f;

    [Header("Optional: GameObject, das bei Kopfneigung aktiviert wird")]
    public GameObject objectToActivate;

    private bool isActive = false;

    void Start()
    {
        if (!headTransform)
        {
            Debug.LogWarning("[HeadAngleToggle] Kein Head Transform gesetzt. Bitte im Inspector zuweisen!");
        }

        // Objekt zu Beginn deaktiviert
        if (objectToActivate)
        {
            objectToActivate.SetActive(false);
        }
    }

    void Update()
    {
        if (!headTransform) return;

        // Winkel zwischen Kopf-Vorwärtsrichtung und Vector3.up
        float angle = Vector3.Angle(headTransform.forward, Vector3.up);

        // Wenn Winkel zu groß -> Kopf stark nach unten
        if (angle > angleThreshold && !isActive)
        {
            isActive = true;
            Debug.Log("[HeadAngleToggle] Kopf stark nach unten geneigt (Winkel: " + angle + "). Aktion ausführen.");

            // Objekt aktivieren
            if (objectToActivate)
            {
                objectToActivate.SetActive(true);
            }
        }
        // Wenn Winkel wieder kleiner -> Kopf hebt sich
        else if (angle <= angleThreshold && isActive)
        {
            isActive = false;
            Debug.Log("[HeadAngleToggle] Kopf wieder weniger geneigt (Winkel: " + angle + "). Aktion rückgängig machen.");

            // Objekt deaktivieren
            if (objectToActivate)
            {
                objectToActivate.SetActive(false);
            }
        }
    }
}
