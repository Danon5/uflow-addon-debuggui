using System;
using UnityEngine;

namespace UFlow.Addon.DebugGUI.Core.Runtime
{
    public class ListGUIDebug : GUIDebug
    {
        public ListGUIDebug(Func<string> msgFormatter, int fontSize = -1, float defaultOpacity = 1f, bool autoUpdateMessage = true) : 
            base(msgFormatter, fontSize, defaultOpacity, autoUpdateMessage)
        {
            Type = GUIDebugType.List;
        }

        public override void OnGUI()
        {
            base.OnGUI();
            
            GUILayout.Label(Message, style);
        }

        protected override void AssignStyle(GUIStyle newStyle)
        {
            newStyle.alignment = TextAnchor.MiddleLeft;
            base.AssignStyle(newStyle);
        }
    }
}