using System;
using UnityEngine;

namespace UFlow.Addon.DebugGUI.Core.Runtime
{
    public class TransformGUIDebug : PositionalGUIDebug
    {
        public TransformGUIDebug(Func<string> msgFormatter, Transform targetTransform, int defaultFontSize = -1, float defaultOpacity = 1f, 
                                 bool autoUpdateMessage = true) : 
            base(msgFormatter, () => targetTransform.position, true, defaultFontSize, defaultOpacity, 
                 autoUpdateMessage)
        {
            Type = GUIDebugType.Transform;
        }
    }
}