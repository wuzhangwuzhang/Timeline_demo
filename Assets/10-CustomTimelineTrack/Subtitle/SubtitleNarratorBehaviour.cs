using UnityEngine.Playables;

public class SubtitleNarratorBehaviour : PlayableBehaviour
{    
    public string Id;
    private bool isFirstFrame = true;
    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        if (!isFirstFrame)
        {
            return;
        }
        isFirstFrame = false;
        float duration = (float)playable.GetDuration();
        //GameFramework.Tool.Util.CallLuaEvent(LuaNotification.TIMELINE_EVENT_SUBTITLE_NARRATOR, Id, duration);
    }
    
    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
       
    }

    public override void OnPlayableDestroy(Playable playable)
    {
        Id = null;
    }
}