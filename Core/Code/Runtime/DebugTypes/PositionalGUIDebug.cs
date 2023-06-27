using System;
using UFlow.Core.Runtime;
using UnityEngine;

namespace UFlow.Addon.DebugGUI.Core.Runtime
{
    public class PositionalGUIDebug : GUIDebug
    {
        public Vector3 Position { get; private set; }
        private readonly bool m_isWorldSpace;
        private readonly bool m_autoUpdatePosition;
        private Camera m_cam;
        private Func<Vector3> m_positionRetriever;

        private const float c_falloff_range = 100;
        private const float c_size_falloff_mult = .25f;
        private const float c_opacity_falloff_mult = .5f;
        
        public PositionalGUIDebug(Func<string> msgFormatter, Func<Vector3> positionRetriever, bool isWorldSpace, int defaultFontSize = -1, float defaultOpacity = 1f,
                                  bool autoUpdateMessage = true, bool autoUpdatePosition = true) : 
            base(msgFormatter, defaultFontSize, defaultOpacity, autoUpdateMessage)
        {
            m_isWorldSpace = isWorldSpace;
            Type = isWorldSpace ? GUIDebugType.WorldSpacePositional : GUIDebugType.ScreenSpacePositional;
            SetPositionRetriever(positionRetriever);
            m_autoUpdatePosition = autoUpdatePosition;
        }
        
        public void SetPositionRetriever(Func<Vector3> positionRetriever)
        {
            m_positionRetriever = positionRetriever;
        }

        public void UpdatePosition()
        {
            Position = m_positionRetriever();
        }

        public override void OnGUI()
        {
            if (m_autoUpdatePosition && m_positionRetriever != null)
                UpdatePosition();

            if (m_cam == null) m_cam = Camera.main;
            if (m_cam == null) return;
            
            Vector3 dirToTarget = Position - m_cam.transform.position;
            
            if (m_isWorldSpace)
                opacity = dirToTarget.magnitude.ClampedRemap(
                    0f, c_falloff_range, DefaultOpacity,
                    DefaultOpacity * c_opacity_falloff_mult);

            base.OnGUI();   
            
            Rect newRect = new Rect(Vector2.zero, new Vector2(Screen.width, Screen.height));

            if (m_isWorldSpace)
            {
                style.fontSize =
                    Mathf.RoundToInt(dirToTarget.magnitude.ClampedRemap(0f, c_falloff_range,
                                                                        DefaultFontSize, DefaultFontSize * c_size_falloff_mult));

                if (Vector3.Dot(dirToTarget.normalized, m_cam.transform.forward) <= 0f) return;
                newRect.center = m_cam.WorldToScreenPoint(Position);
                newRect.center = new Vector2(newRect.center.x, Screen.height - newRect.center.y);
            }
            else
                newRect.center = Position;
            
            GUI.Label(newRect, Message, style);
        }
    }
}