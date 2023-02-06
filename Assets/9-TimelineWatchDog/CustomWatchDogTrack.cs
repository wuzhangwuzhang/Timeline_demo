using CustomMarkerTrack;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
public class CustomTrack : PlayableTrack
{
    [TrackClipType(typeof(CustomWatchDogClip))]
    public class CustomWatchDogTrack : TrackAsset
    {
        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            // var director = go.GetComponent<PlayableDirector>();
            //获取轨道上的所有Clip,并将clip名字作为参数传过去
            foreach (var clip in GetClips())
            {
                var playableAsset = clip.asset as CustomWatchDogClip;
                if (playableAsset)
                {
                    if (!(playableAsset is null))
                    {
                        playableAsset.ClipName = clip.displayName;
                    }
                }
            }
            var scriptPlayable = ScriptPlayable<CustomBehaviour>.Create(graph, inputCount);
            return scriptPlayable;
        }
    }
}
