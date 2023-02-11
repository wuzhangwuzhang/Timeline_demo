using UnityEngine;
using UnityEngine.Playables;

public class WatchableCustomPlayableBehaviour : PlayableBehaviour
{
    protected string DisplayName { get; set; }
    public double StartTime { get; private set; }
    public double EndTime { get; private set; }
    public bool IsWatchStart = false;
    public bool IsWatchComplete = false;
    protected Playable m_playable;
    
    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        m_playable = playable;
    }
    
    /// <summary>
    /// 进入Clip触发
    /// </summary>
    public virtual void OnWatchStart()
    {
    }

    /// <summary>
    /// 离开Clip触发
    /// </summary>
    public virtual void OnWatchComplete()
    {
    }

    /// <summary>
    /// 注册需要检测的Clip
    /// </summary>
    /// <param name="playableDirector">所属PlayableDirector</param>
    /// <param name="startTime">开始时间</param>
    /// <param name="endTime">结束时间</param>
    /// <param name="displayName"></param>
    public void AddWatch(PlayableDirector playableDirector, double startTime, double endTime,string displayName)
    {
        StartTime = startTime;
        EndTime = endTime;
        DisplayName = displayName;
        PlayableWatcher.Add(playableDirector, this);
    }

    private void RemoveWatch()
    {
        PlayableWatcher.Remove(this);
    }
}
