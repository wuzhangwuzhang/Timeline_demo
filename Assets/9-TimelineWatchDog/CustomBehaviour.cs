using UnityEngine;
 
public class CustomBehaviour : WatchableCustomPlayableBehaviour
{
    public override void OnWatchStart()
    {
        // 自定义逻辑
        Debug.Log($"<color=yellow>OnWatchStart:StartTime,{StartTime:F3} EndTime,{EndTime:F3} IsWatchStart,{IsWatchStart} IsWatchComplete,{IsWatchComplete} DisplayName:{DisplayName} frameCount:{Time.frameCount}, {Application.targetFrameRate}</color>");
    }
    
    public override void OnWatchComplete()
    {
        // 自定义逻辑
        Debug.Log($"<color=red>OnWatchComplete:StartTime,{StartTime:F3} EndTime,{EndTime:F3} IsWatchStart,{IsWatchStart} IsWatchComplete,{IsWatchComplete} DisplayName:{DisplayName} frameCount:{Time.frameCount}, {Application.targetFrameRate}</color>");
    }
}