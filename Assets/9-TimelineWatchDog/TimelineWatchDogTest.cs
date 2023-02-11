using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TimelineWatchDogTest : MonoBehaviour
{
    public TimelineAsset playableAsset;

    public PlayableDirector playableDirector;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 30;
        PlayableDirectorWatcher.PlayAndWatchTimeline(playableDirector,playableAsset);
        
        PlayableWatcher.SetIsWatching(true);

        var tracks = (playableAsset as TimelineAsset).GetOutputTracks().ToArray();
        foreach (var track in tracks)
        {
            switch (track)
            {
                case CustomTrack.CustomWatchDogTrack customTrack:
                {
                    var timelineClips = customTrack.GetClips();
                    foreach (var timelineClip in timelineClips)
                    {
                        CustomWatchDogClip customWatchDogClip = timelineClip.asset as CustomWatchDogClip;
                        customWatchDogClip.SetPlayableDirector(playableDirector);
                        customWatchDogClip.SetTime(timelineClip.start, timelineClip.end);
                    }
                    break;
                }
                case PathMoveTrack pathMoveTrack:
                {
                    var timelineClips = pathMoveTrack.GetClips();
                    foreach (var timelineClip in timelineClips)
                    {
                        PathMoveClip customWatchDogClip = timelineClip.asset as PathMoveClip;
                        customWatchDogClip.SetPlayableDirector(playableDirector);
                        customWatchDogClip.SetTime(timelineClip.start, timelineClip.end);
                    }
                    break;
                }
            }
        }
    }
}
