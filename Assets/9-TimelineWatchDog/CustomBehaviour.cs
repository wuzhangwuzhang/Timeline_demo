using UnityEngine;
 
public class CustomBehaviour : WatchableCustomPlayableBehaviour
{
    public override void OnWatchStart()
    {
        // 自定义逻辑
        Debug.Log($"OnWatchStart:StartTime,{StartTime:F3} EndTime,{EndTime:F3} IsWatchStart,{IsWatchStart} IsWatchComplete,{IsWatchComplete} DisplayName:{DisplayName} frameCount:{Time.frameCount}, {Application.targetFrameRate}");
    }
    
    public override void OnWatchComplete()
    {
        // 自定义逻辑
        Debug.LogError($"OnWatchComplete:StartTime,{StartTime:F3} EndTime,{EndTime:F3} IsWatchStart,{IsWatchStart} IsWatchComplete,{IsWatchComplete} DisplayName:{DisplayName} frameCount:{Time.frameCount}, {Application.targetFrameRate}");
    }
}