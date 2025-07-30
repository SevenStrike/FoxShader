using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace DigitalVisualization.CurveLine
{
    [RequireComponent(typeof(LineRenderer))]
    public class MPCL_LineRenderer : MonoBehaviour
    {
        [Header("Render")]
        public float LineSegmentSize = 0.15f;
        [Range(0, 1)]
        public float LineWidth_Start = 0.15f;
        [Range(0, 1)]
        public float LineWidth_End = 0.15f;

        [Header("Gizmos")]
        public bool ShowGizmos = true;
        public float GizmoSize = 0.1f;
        public Color GizmoColor = new Color(1, 0, 0, 0.5f);

        private MPCL_Pointer[] LinePoints = new MPCL_Pointer[0];
        private Vector3[] LinePositions = new Vector3[0];
        private Vector3[] LinePositionsOld = new Vector3[0];

        public void Update()
        {
            GetPoints();
            SetPointsToLine();
            SetLineWidth(LineWidth_Start, LineWidth_End);
        }

        /// <summary>
        /// 获取点
        /// </summary>
        void GetPoints()
        {
            //find curved points in children
            LinePoints = this.GetComponentsInChildren<MPCL_Pointer>();

            //add positions
            LinePositions = new Vector3[LinePoints.Length];
            for (int i = 0; i < LinePoints.Length; i++)
            {
                LinePositions[i] = LinePoints[i].transform.position;
            }
        }

        /// <summary>
        /// 设置线宽
        /// </summary>
        /// <param name="width"></param>
        void SetLineWidth(float width_start, float width_end)
        {
            LineRenderer line = this.GetComponent<LineRenderer>();
            line.startWidth = width_start;
            line.endWidth = width_end;
        }

        /// <summary>
        /// 设置点到线
        /// </summary>
        void SetPointsToLine()
        {
            //create old positions if they dont match
            if (LinePositionsOld.Length != LinePositions.Length)
            {
                LinePositionsOld = new Vector3[LinePositions.Length];
            }

            //check if line points have moved
            bool moved = false;
            for (int i = 0; i < LinePositions.Length; i++)
            {
                //compare
                if (LinePositions[i] != LinePositionsOld[i])
                {
                    moved = true;
                }
            }

            //update if moved
            if (moved == true)
            {
                LineRenderer line = this.GetComponent<LineRenderer>();
                float val = 1 - LineSegmentSize;

                //get smoothed values
                Vector3[] smoothedPoints = MPCL_LineSmoother.SmoothLine(LinePositions, val);

                //set line settings
                line.positionCount = smoothedPoints.Length;
                line.SetPositions(smoothedPoints);

                //line.SetVertexCount(smoothedPoints.Length);
            }
        }

        void OnDrawGizmosSelected()
        {
            Update();
        }

        void OnDrawGizmos()
        {
            if (LinePoints.Length == 0)
            {
                GetPoints();
            }

            //settings for gizmos
            foreach (MPCL_Pointer linePoint in LinePoints)
            {
                linePoint.ShowGizmo = ShowGizmos;
                linePoint.GizmoSize = GizmoSize;
                linePoint.GizmoColor = GizmoColor;
            }
        }
    }
}