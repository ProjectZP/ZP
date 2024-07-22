using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class HandAnimator : MonoBehaviour
{
    [SerializeField] private InputActionProperty _pinchAnimationAction;
    [SerializeField] private InputActionProperty _gripAnimationAction;
    [SerializeField] private Animator _animator;

    private void Awake()
    {
        if (_animator == null)
            _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float triggerValue = _pinchAnimationAction.action.ReadValue<float>();
        _animator.SetFloat("Trigger", triggerValue);

        float gripValue = _gripAnimationAction.action.ReadValue<float>();
        _animator.SetFloat("Grip", gripValue);
    }
}
