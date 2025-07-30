using UnityEngine;
using System.Collections;

namespace DigitalVisualization.CurveLine
{
    public class TPCL_Pointer : MonoBehaviour
    {
        [HideInInspector] public bool ShowGizmo = true;
        [HideInInspector] public float GizmoSize = 0.1f;
        [HideInInspector] public Color GizmoColor = new Color(1, 0, 0, 0.5f);

        void OnDrawGizmos()
        {
            if (ShowGizmo)
            {
                Gizmos.color = GizmoColor;

                Gizmos.DrawSphere(this.transform.position, GizmoSize);
            }
        }

        void OnDrawGizmosSelected()
        {
            TPCL_Render TPCLRender = this.transform.parent.GetComponent<TPCL_Render>();

            if (TPCLRender != null)
            {
                TPCLRender.Update();
            }
        }
    }
}