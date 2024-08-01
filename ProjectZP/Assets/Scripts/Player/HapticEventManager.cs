using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace ZP.SJH.Player
{
    public class HapticEventManager : MonoBehaviour
    {
        private const float DEFAULT_AMPLITUDE = 1f;
        private const float DEFAULT_DURATION = 0.1f;

        private int _doorLayer;

        private void Awake()
        {
            _doorLayer = LayerMask.NameToLayer("Door");
        }

        public void ActivateVibration(XRRayInteractor interactor, float amplitude = DEFAULT_AMPLITUDE, float duration = DEFAULT_DURATION)
        {
            interactor.SendHapticImpulse(amplitude, duration);
        }

        public void RayOnDoor(HoverEnterEventArgs args)
        {
            if (args.interactableObject.transform.gameObject.layer == _doorLayer)
            {
                ActivateVibration(args.interactorObject as XRRayInteractor);
            }
        }
    }
}
