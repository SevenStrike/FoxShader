using UnityEngine;
using System.Collections;

namespace DigitalVisualization.CurveLine
{
    public class MPCL_Pointer : MonoBehaviour
    {
        [HideInInspector] public bool ShowGizmo = true;
        [HideInInspector] public float GizmoSize = 0.1f;
        [HideInInspector] public Color GizmoColor = new Color(1, 0, 0, 0.5f);

        void OnDrawGizmos()
        {
            if (ShowGizmo == true)
            {
                Gizmos.color = GizmoColor;

                Gizmos.DrawSphere(this.transform.position, GizmoSize);
            }
        }

        //update parent line when this point moved
        void OnDrawGizmosSelected()
        {
            MPCL_LineRenderer curvedLine = this.transform.parent.GetComponent<MPCL_LineRenderer>();

            if (curvedLine != null)
            {
                curvedLine.Update();
            }
        }
    }
}