using UnityEngine;
using UnityEngine.Playables;

namespace CustomMarkerTrack
{
    public class CustomMarkerClip : PlayableAsset
    {
        public string ClipName { get; set; }
        private readonly CustomMarkerBehaviour m_Template = new CustomMarkerBehaviour();
        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<CustomMarkerBehaviour>.Create(graph,m_Template);
            CustomMarkerBehaviour behaviour = playable.GetBehaviour();
            behaviour.ClipName = ClipName;
            behaviour.OwnerName = owner.name;
            return playable;
        }
    }
}
