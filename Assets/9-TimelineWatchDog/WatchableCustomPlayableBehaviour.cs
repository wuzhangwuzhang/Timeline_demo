using UnityEngine.Playables;

public class WatchableCustomPlayableBehaviour : PlayableBehaviour
{
    public string DisplayName { get; set; }
    public double StartTime { get; private set; }
    public double EndTime { get; private set; }
    public bool IsWatchStart = false;
    public bool IsWatchComplete = false;

    public virtual void OnWatchStart()
    {
    }

    public virtual void OnWatchComplete()
    {
    }

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
