using UnityEngine.EventSystems;
using UnityEngine;
using UnityEditor;


public interface IMyHandler : IEventSystemHandler
{
    void Trigger();
}
