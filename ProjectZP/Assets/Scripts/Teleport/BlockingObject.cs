using UnityEngine;
using ZP.Villin.Teleport;

public class BlockingObject : MonoBehaviour
{
    [SerializeField] private TeleportManager _teleportManager;
    [SerializeField] private GameObject _leftBlockingObject;
    [SerializeField] private GameObject _rightBlockingObject;
    private void Awake()
    {
        if (_teleportManager == default)
        {
            _teleportManager = FindFirstObjectByType<TeleportManager>();
        }
        _teleportManager.OnLeftTeleport += EnableLeftCollision;
        _teleportManager.OnRightTeleport += EnableRightCollision;

    }

    private void EnableLeftCollision()
    {
        _leftBlockingObject.SetActive(true);
        _rightBlockingObject.SetActive(false);
    }

    private void EnableRightCollision()
    {
        _rightBlockingObject.SetActive(true);
        _leftBlockingObject.SetActive(false);
    }
}
