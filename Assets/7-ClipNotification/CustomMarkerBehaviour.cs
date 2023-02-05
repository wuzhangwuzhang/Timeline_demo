using System;
using UnityEngine;
using UnityEngine.Playables;
using Object = System.Object;

namespace CustomMarkerTrack
{
    public class CustomMarkerBehaviour : PlayableBehaviour
    {
        #region 自定义参数
        private double m_PreviousTime;
        private bool _isEnter;
        public string ClipName;
        public string OwnerName;
        #endregion
       
        public override void OnGraphStart(Playable playable)
        {
            m_PreviousTime = 0;
        }
        
        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            if (string.IsNullOrEmpty(ClipName))
            {
                return;
            }
            if (!_isEnter)
            {
                if (m_PreviousTime <= playable.GetTime())
                {
                    info.output.PushNotification(playable, new CustomMarkerNotification(ClipName,OwnerName));
                    Debug.LogError($"开始调用：{ClipName}");
                }
                m_PreviousTime = playable.GetTime();
                _isEnter = true;
            }
        }
    }
}
