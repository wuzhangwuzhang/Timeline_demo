using UnityEngine.Playables;

public abstract class WatchableClip : PlayableAsset
{
    protected PlayableDirector PlayableDirector;
    public double StartTime { get; private set; }
    public double EndTime { get; private set; }

    public string ClipName { get; set; }

    public void SetPlayableDirector(PlayableDirector playableDirector)
    {
        this.PlayableDirector = playableDirector;
    }

    public void SetTime(double startTime, double endTime)
    {
        StartTime = startTime;
        EndTime = endTime;
    }
}
