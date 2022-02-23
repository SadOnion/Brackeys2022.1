using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(BoxCollider2D))]
public class Checkpoint : MonoBehaviour
{
    [SerializeField] bool showGizmo = true;
    BoxCollider2D boxCollider2D;

    private void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (!showGizmo)
            return;

        boxCollider2D = GetComponent<BoxCollider2D>();

        Gizmos.color = Color.green;
        Vector3 boxCenter = boxCollider2D.transform.position + (Vector3)boxCollider2D.offset;
        Gizmos.DrawWireCube(boxCenter, boxCollider2D.size);

        Gizmos.DrawWireSphere(transform.position, 0.2f);

        GUIStyle labelStyle = new GUIStyle();
        labelStyle.alignment = TextAnchor.LowerCenter;
        labelStyle.fixedWidth = boxCollider2D.size.x;
        Vector3 labelPos = boxCenter + Vector3.up * boxCollider2D.size.y / 2f;
        labelStyle.fontSize = (int)(20f / HandleUtility.GetHandleSize(labelPos));
        labelStyle.normal.textColor = Color.green;
        Handles.Label(labelPos, "Checkpoint", labelStyle);
    }
#endif
}
