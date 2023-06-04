using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayoutGroup : MonoBehaviour
{
    public Vector3 spacing = new Vector3(1f, 1f, 1f); // Spacing between objects
    public Vector3 offset = Vector3.zero; // Offset for the entire group

    [ContextMenu("Space Set")]
    private void DoSpacing()
    {
        Transform[] childObjects = GetComponentsInChildren<Transform>();

        // Ignore the parent object
        foreach (Transform child in childObjects)
        {
            if (child != transform)
            {
                // Set the position of each child object based on the spacing and offset
                child.localPosition = offset;
                offset += spacing;
            }
        }
    }
}
