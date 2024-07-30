using System.Collections.Generic;
using UnityEngine;
using ZP.Villin.Teleport;
using ZP.Villin.World;

public class DoorsAndWindowsController : MonoBehaviour
{
    [SerializeField] DynamicWorldConstructor _dynamicWorldConstructor;
    [SerializeField] TeleportManager _teleportManager;
    private List<GameObject> _trackedObjects = new List<GameObject>();


    private void Awake()
    {
        _teleportManager.OnLeftTeleport += SubscribeOnTeleport;
        _teleportManager.OnRightTeleport += SubscribeOnTeleport;
        _dynamicWorldConstructor.OnDoorsAndWindowsGenerated += SubscribeOnDoorsAndWindowsGenerated;
    }

    private void SubscribeOnDoorsAndWindowsGenerated(GameObject doorsAndWindows)
    {
        _trackedObjects.Add(doorsAndWindows);
    }

    private void SubscribeOnTeleport()
    {
        // find all disabled windows.
        foreach (GameObject doorsAndWindows in _trackedObjects)
        {
            if (doorsAndWindows != null && !doorsAndWindows.activeInHierarchy)
            {
                Debug.Log($"Disabled object found: {doorsAndWindows.name}");
        // activate founded windows.
                doorsAndWindows.SetActive(true);
            }
        // find "windows and doors" which at same floor of player.
            if (doorsAndWindows != null && doorsAndWindows.name == $"floor {_dynamicWorldConstructor.GetTotalFloorCount() - _teleportManager.GetNowRemainTeleportCount()} Doors and Windows")
            {
        // make disable.
                doorsAndWindows.SetActive(false);
            }
        }


    }
}
