using System;
using UnityEngine;

namespace UFlow.Addon.DebugGUI.Core.Runtime
{
    public abstract class GUIDebug
    {
        protected GUIStyle style;
        protected float opacity;
        private readonly bool m_autoUpdate;
        private Func<string> m_msgFormatter;

        public GUIDebugType Type { get; protected set; }
        protected string Message { get; private set; }
        protected int DefaultFontSize { get; private set; }
        protected float DefaultOpacity { get; }

        public GUIDebug(Func<string> msgFormatter, int defaultFontSize = -1, float defaultOpacity = 1f, bool autoUpdateMessage = true)
        {
            m_autoUpdate = autoUpdateMessage;
            DefaultFontSize = defaultFontSize;
            DefaultOpacity = defaultOpacity;
            SetMessageFormat(msgFormatter);
        }

        public void SetMessageFormat(Func<string> format)
        {
            m_msgFormatter = format;
        }

        public void UpdateMessage()
        {
            Message = m_msgFormatter();
        }

        protected virtual void AssignStyle(GUIStyle newStyle)
        {
            style = new GUIStyle(newStyle);
            DefaultFontSize = DefaultFontSize < 0 ? GUIDebugModule.defaultStyle.fontSize : DefaultFontSize;
            style.fontSize = DefaultFontSize;
            opacity = DefaultOpacity;
        }

        public virtual void OnGUI()
        {
            GUI.color = new Color(1, 1, 1, opacity);
            
            if (m_autoUpdate && m_msgFormatter != null)
                UpdateMessage();
            
            if (style == null)
                AssignStyle(new GUIStyle(GUIDebugModule.defaultStyle));
        }
    }
}