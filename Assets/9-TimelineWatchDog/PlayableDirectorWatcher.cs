using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

/// <summary>
/// 监视并让PlayableDirector逐帧播放TimelineAsset，确保不会帧率过低（deltaTime过大）跳过任何clip
/// </summary>
public static class PlayableDirectorWatcher
{
    private static float frameRate = 60f;
    private static float interval = 1f / frameRate;
    private const float timelineAssetDefaultFrameRate = 30f;
    private static HashSet<PlayableDirector> playableDirectors = new HashSet<PlayableDirector>();
    private static HashSet<PlayableDirector> playableDirectorsToRemove = new HashSet<PlayableDirector>();

    static PlayableDirectorWatcher()
    {
        frameRate = Application.targetFrameRate;
        // 不低于TimelineAsset的默认帧率
        if (frameRate < timelineAssetDefaultFrameRate) 
            frameRate = timelineAssetDefaultFrameRate;
        interval = 1f / frameRate;
        var obj = new GameObject("PlayableDirectorWatcher") {hideFlags = HideFlags.HideAndDontSave};
        obj.AddComponent<PlayableDirectorWatcherUpdater>();
    }

    public static void PlayAndWatchTimeline(PlayableDirector playableDirector, TimelineAsset timelineAsset = null)
    {
        playableDirector.timeUpdateMode = DirectorUpdateMode.Manual;
        if (timelineAsset)
            playableDirector.Play(timelineAsset);
        else
            playableDirector.Play();
        Add(playableDirector);
    }

    private static void Add(PlayableDirector playableDirector)
    {
        playableDirectors.Add(playableDirector);
    }

    private static void Remove(PlayableDirector playableDirector)
    {
        playableDirectors.Remove(playableDirector);
    }

    class PlayableDirectorWatcherUpdater : MonoBehaviour
    {
        private void Update()
        {
            // 先删除待删除的
            foreach (var playableDirector in playableDirectorsToRemove)
            {
                Remove(playableDirector);
            }

            playableDirectorsToRemove.Clear();

            // 更新现有的
            foreach (var playableDirector in playableDirectors)
            {
                if (playableDirector.time >= playableDirector.playableAsset.duration)
                {
                    playableDirector.Stop();
                }

                // playableDirector已经播放完毕并停止（仅适用于WrapMode为None的Director）
                if (playableDirector.state == PlayState.Paused && playableDirector.time == 0d)
                {
                    playableDirectorsToRemove.Add(playableDirector);
                    continue;
                }

                var previousTime = playableDirector.time;
                while (true)
                {
                    playableDirector.time += interval;
                    if (playableDirector.time > previousTime + Time.deltaTime)
                    {
                        playableDirector.time = previousTime + Time.deltaTime;
                        playableDirector.Evaluate();
                        break;
                    }

                    playableDirector.Evaluate();
                }
            }
        }
    }
}