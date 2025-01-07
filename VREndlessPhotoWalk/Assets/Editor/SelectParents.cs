using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class SelectParentAndSiblings : MonoBehaviour
{
    [MenuItem("Tools/Select Parent and All Children")]
    static void SelectParentAndAllChildren()
    {
        // Holt die aktuell ausgewählten Kinder
        GameObject[] selectedChildren = Selection.gameObjects;
        if (selectedChildren.Length == 0)
        {
            Debug.LogWarning("Keine Kinder ausgewählt!");
            return;
        }

        // HashSet für die endgültige Auswahl (vermeidet Duplikate)
        HashSet<GameObject> finalSelection = new HashSet<GameObject>();

        foreach (GameObject child in selectedChildren)
        {
            // Finde den Parent des aktuellen Kindes
            Transform parentTransform = child.transform.parent;

            if (parentTransform != null)
            {
                GameObject parent = parentTransform.gameObject;

                // Füge den Parent hinzu
                finalSelection.Add(parent);

                // Füge alle Kinder dieses Parents hinzu
                foreach (Transform sibling in parentTransform)
                {
                    finalSelection.Add(sibling.gameObject);
                }
            }
            else
            {
                // Falls kein Parent vorhanden ist, füge das Kind selbst hinzu
                finalSelection.Add(child);
            }
        }

        // Setze die neue Auswahl
        Selection.objects = finalSelection.ToArray();
        Debug.Log($"{finalSelection.Count} Objekte ausgewählt (inkl. Parent und alle Kinder).");
    }
}
