using UnityEngine;
 
public class CustomBehaviour : WatchableCustomPlayableBehaviour
{
    public override void OnWatchStart()
    {
        // 自定义逻辑
        Debug.Log($"OnWatchStart:StartTime,{StartTime} EndTime,{EndTime} IsWatchStart,{IsWatchStart} IsWatchComplete,{IsWatchComplete} DisplayName,{DisplayName} {Application.targetFrameRate}");
    }
    
    public override void OnWatchComplete()
    {
        // 自定义逻辑
        Debug.LogError($"OnWatchComplete:StartTime,{StartTime} EndTime,{EndTime} IsWatchStart,{IsWatchStart} IsWatchComplete,{IsWatchComplete} DisplayName,{DisplayName} {Application.targetFrameRate}");
    }
}