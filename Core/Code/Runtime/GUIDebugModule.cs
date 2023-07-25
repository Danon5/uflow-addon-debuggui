using System.Collections.Generic;
using UFlow.Core.Runtime;
using UnityEngine;

namespace UFlow.Addon.DebugGUI.Core.Runtime
{
    public class GUIDebugModule : BaseBehaviourModule<GUIDebugModule>
    {
        internal static GUIStyle defaultStyle;

        private readonly List<GUIDebug> m_debugs = new();
        private readonly List<ListGUIDebug> m_listDebugs = new();

        private const float c_list_side_inner_border = 15;
        private const float c_list_top_inner_border = 10;
        
        public override void OnGUI()
        {
            if (defaultStyle == null)
            {
                defaultStyle = GUI.skin.label;
                defaultStyle.alignment = TextAnchor.MiddleCenter;
                defaultStyle.richText = true;
            }

            foreach (GUIDebug debug in m_debugs)
                debug.OnGUI();

            using (var areaScope = new GUILayout.AreaScope(Rect.MinMaxRect(c_list_side_inner_border, c_list_top_inner_border,
                                                                           Screen.width - c_list_side_inner_border, 
                                                                           Screen.height - c_list_top_inner_border)))
            {
                foreach (ListGUIDebug debug in m_listDebugs)
                    debug.OnGUI();
            }
        }

        public override void UnloadDirect()
        {
            defaultStyle = null;
        }

        public void AddEntry(GUIDebug debug)
        {
            if (debug.Type != GUIDebugType.List)
                m_debugs.Add(debug);
            else
                m_listDebugs.Add((ListGUIDebug)debug);
        }

        public void RemoveEntry(GUIDebug debug)
        {
            if (debug.Type != GUIDebugType.List)
                m_debugs.Remove(debug);
            else
                m_listDebugs.Remove((ListGUIDebug)debug);
        }
    }
}