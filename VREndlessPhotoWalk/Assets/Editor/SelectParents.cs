using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class SelectParentAndSiblings : MonoBehaviour
{
    [MenuItem("Tools/Select Parent and All Children")]
    static void SelectParentAndAllChildren()
    {
        // Holt die aktuell ausgew�hlten Kinder
        GameObject[] selectedChildren = Selection.gameObjects;
        if (selectedChildren.Length == 0)
        {
            Debug.LogWarning("Keine Kinder ausgew�hlt!");
            return;
        }

        // HashSet f�r die endg�ltige Auswahl (vermeidet Duplikate)
        HashSet<GameObject> finalSelection = new HashSet<GameObject>();

        foreach (GameObject child in selectedChildren)
        {
            // Finde den Parent des aktuellen Kindes
            Transform parentTransform = child.transform.parent;

            if (parentTransform != null)
            {
                GameObject parent = parentTransform.gameObject;

                // F�ge den Parent hinzu
                finalSelection.Add(parent);

                // F�ge alle Kinder dieses Parents hinzu
                foreach (Transform sibling in parentTransform)
                {
                    finalSelection.Add(sibling.gameObject);
                }
            }
            else
            {
                // Falls kein Parent vorhanden ist, f�ge das Kind selbst hinzu
                finalSelection.Add(child);
            }
        }

        // Setze die neue Auswahl
        Selection.objects = finalSelection.ToArray();
        Debug.Log($"{finalSelection.Count} Objekte ausgew�hlt (inkl. Parent und alle Kinder).");
    }
}
