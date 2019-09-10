using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoDrawer : MonoBehaviour
{
    public enum GizmoShape
    {
        WireSphere = 0,
        Cube = 1
    }
    public enum GizmoColour
    {
        Blue,
        Green,
        Red
    }

    public bool showGizmo = false;
    public GizmoShape shape = GizmoShape.Cube;
    public GizmoColour gizmoColour = GizmoColour.Blue;
    public float radius = 1.0f;
    public Vector3 scale = Vector3.one;

    private void OnDrawGizmos()
    {
        if(!showGizmo)
        {
            return;
        }
        switch(gizmoColour)
        {
            case GizmoColour.Blue:
                Gizmos.color = Color.blue;
                break;
            case GizmoColour.Green:
                Gizmos.color = Color.green;
                break;
            case GizmoColour.Red:
                Gizmos.color = Color.red;
                break;
        }

        switch(shape)
        {
            case GizmoShape.Cube:
                Gizmos.DrawCube(transform.position,scale);
                break;
            case GizmoShape.WireSphere:
                Gizmos.DrawWireSphere(transform.position,radius);
                break;
        }
    }
}
