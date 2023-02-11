using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

/// <summary>
/// 监视PlayableBehaviour是否播放开始、结束
/// </summary>
public static class PlayableWatcher
{
    private static readonly HashSet<WatchableCustomPlayableBehaviour> PlayableBehavioursToRemove =
        new HashSet<WatchableCustomPlayableBehaviour>();

    private static readonly Dictionary<WatchableCustomPlayableBehaviour, PlayableDirector> PlayableBehaviourToDirector =
        new Dictionary<WatchableCustomPlayableBehaviour, PlayableDirector>();

    private static readonly PlayableWatcherUpdater PlayableDirectorWatcherUpdater;

    static PlayableWatcher()
    {
        var obj = new GameObject("PlayableWatcherObj") {hideFlags = HideFlags.HideAndDontSave};
        PlayableDirectorWatcherUpdater = obj.AddComponent<PlayableWatcherUpdater>();
    }

    public static void SetIsWatching(bool isWatching)
    {
        PlayableDirectorWatcherUpdater.enabled = isWatching;
    }

    public static void Reset()
    {
        PlayableBehavioursToRemove.Clear();
        PlayableBehaviourToDirector.Clear();
    }

    public static void Add(PlayableDirector playableDirector, WatchableCustomPlayableBehaviour customPlayableBehaviour)
    {
        PlayableBehaviourToDirector.Add(customPlayableBehaviour, playableDirector);
    }

    public static void Remove(WatchableCustomPlayableBehaviour customPlayableBehaviour)
    {
        PlayableBehavioursToRemove.Remove(customPlayableBehaviour);
    }
    

    [DefaultExecutionOrder(9999)]
    class PlayableWatcherUpdater : MonoBehaviour
    {
        private void Update()
        {
            // 先删除待删除的
            foreach (var playableDirector in PlayableBehavioursToRemove)
            {
                PlayableBehaviourToDirector.Remove(playableDirector);
            }

            PlayableBehavioursToRemove.Clear();

            // 更新现有的
            foreach (var pair in PlayableBehaviourToDirector)
            {
                var playableBehaviour = pair.Key;
                var playableDirector = pair.Value;

                if (null == playableDirector)
                {
                    continue;
                }
                if (playableDirector.time >= playableBehaviour.StartTime)
                {
                    if (!playableBehaviour.IsWatchStart)
                    {
                        playableBehaviour.IsWatchStart = true;
                        playableBehaviour.OnWatchStart();
                    }
                }

                if (playableDirector.time >= playableBehaviour.EndTime)
                {
                    if (!playableBehaviour.IsWatchComplete)
                    {
                        playableBehaviour.IsWatchComplete = true;
                        playableBehaviour.OnWatchComplete();
                        Remove(playableBehaviour);
                    }
                }
            }
        }
    }
}