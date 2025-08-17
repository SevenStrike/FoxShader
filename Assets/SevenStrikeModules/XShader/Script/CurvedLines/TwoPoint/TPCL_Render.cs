using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DigitalVisualization.CurveLine
{
    [RequireComponent(typeof(LineRenderer))]
    public class TPCL_Render : MonoBehaviour
    {
        [Header("Render")]
        public LineRenderer LineRenderer;
        public TPCL_Pointer StartPoint;
        public TPCL_Pointer EndPoint;
        [Range(2, 200)]
        public int LineSegments = 10;
        public Vector3 Offset;
        public float LineWidth_Start = 0.1f;
        public float LineWidth_End = 0.1f;

        [Header("Gizmos")]
        public bool ShowGizmos = true;
        public float GizmoSize = 0.1f;
        public Color GizmoColor = new Color(1, 0, 0, 0.5f);

        [Header("Flow")]
        public Material LineRendererMat;
        public float FlowPos;
        public float FlowPos_start;
        public float FlowPos_end;
        public float FlowSpeed_min = 1;
        public float FlowSpeed_max = 0.5f;
        public AnimationCurve EaseCurvePulsePos;
        public float FlowInterval = 2;
        public bool FlowOk = true;

        private void OnEnable()
        {
            TPCL_CreatePoints();
            TPCL_CreateLine();
        }

        void Start()
        {
            TPCL_FlowingInitia();
        }

        public void Update()
        {
            TPCL_UpdateLine();

            if (Application.isPlaying)
            {
                TPCL_UpdateFlow();
            }
        }

        /// <summary>
        /// 流水光效材质初始化
        /// </summary>
        void TPCL_FlowingInitia()
        {
            LineRendererMat = new Material(LineRenderer.material);
            LineRenderer.material = LineRendererMat;
        }

        /// <summary>
        /// 更新显示流水光效值
        /// </summary>
        void TPCL_UpdateFlow()
        {
            LineRendererMat.SetFloat("_fw_Position", FlowPos);
        }

        /// <summary>
        /// 创建线的分段
        /// </summary>
        void TPCL_CreateLine()
        {
            if (LineRenderer == null)
                LineRenderer = GetComponent<LineRenderer>();
            LineRenderer.positionCount = LineSegments;
        }

        /// <summary>
        /// 创建起始点和结束点
        /// </summary>
        void TPCL_CreatePoints()
        {
            if (StartPoint == null)
            {
                GameObject obj = new GameObject();
                obj.name = "StartPoint";
                obj.transform.SetParent(transform);
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localEulerAngles = Vector3.zero;
                obj.transform.localScale = Vector3.one;
                StartPoint = obj.AddComponent<TPCL_Pointer>();
            }
            if (EndPoint == null)
            {
                GameObject obj = new GameObject();
                obj.name = "EndPoint";
                obj.transform.SetParent(transform);
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localEulerAngles = Vector3.zero;
                obj.transform.localScale = Vector3.one;
                EndPoint = obj.AddComponent<TPCL_Pointer>();
            }
        }

        /// <summary>
        /// 更新曲线显示
        /// </summary>
        void TPCL_UpdateLine()
        {
            Vector3 start = StartPoint.transform.position;
            Vector3 end = EndPoint.transform.position;

            float step = 1f / LineSegments;

            for (int i = 0; i < LineSegments; i++)
            {
                float t = step * i;
                Vector3 pos = TPCL_CalculateParabola(start, end, t);
                if (LineRenderer.positionCount >= LineSegments)
                    LineRenderer.SetPosition(i, pos);
            }
        }

        /// <summary>
        /// 计算贝塞尔公式
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        Vector3 TPCL_CalculateParabola(Vector3 start, Vector3 end, float t)
        {
            Vector3 height = Vector3.up * (end - start).magnitude + Offset;
            Vector3 midpoint = (start + end) * 0.5f + height;
            Vector3 P0 = start;
            Vector3 P1 = midpoint;
            Vector3 P2 = end;
            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;
            Vector3 p = uu * P0;
            p += 2 * u * t * P1;
            p += tt * P2;
            return p;
        }

        void OnDrawGizmosSelected()
        {
            Update();
        }

        void OnDrawGizmos()
        {
            TPCL_CreatePoints();
            TPCL_CreateLine();

            if (ShowGizmos)
            {
                StartPoint.GizmoColor = GizmoColor;
                StartPoint.GizmoSize = GizmoSize;
                StartPoint.ShowGizmo = ShowGizmos;

                EndPoint.GizmoColor = GizmoColor;
                EndPoint.GizmoSize = GizmoSize;
                EndPoint.ShowGizmo = ShowGizmos;
            }

            if (LineRenderer != null)
            {
                LineRenderer.startWidth = LineWidth_Start;
                LineRenderer.endWidth = LineWidth_End;
            }
        }
    }
}