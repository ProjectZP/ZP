using System;
using System.Collections;
using TMPro;
using UnityEngine;
using ZP.SJH.Player;

namespace ZP.Villin.Teleport
{
    public enum DoorStateList
    {
        None,
        DoorOpen,
        DoorClose,
        Length
    }


    public abstract class DoorController : MonoBehaviour
    {
        [SerializeField] private GameObject _transparentCollision;
        [SerializeField] private Animator _leftDoorAnimator;
        [SerializeField] private Animator _rightDoorAnimator;
        [SerializeField] private DoorStateList _state = DoorStateList.None;
        protected PlayerManager _playerManager;
        protected TeleportManager _teleportManager;
        protected const float _animationTimeout = 10f;
        protected bool _isPlayerOnEndStageRegion;
        protected bool _isRightDoorActivated;
        protected bool _isDoorOpened;


        protected virtual void Awake()
        {
            CheckAwakeException();
            SetActionSubscribers();
        }

        /// <summary>
        /// Check Exception When <see cref="StairDoorController"/> Awake.
        /// </summary>
        protected virtual void CheckAwakeException()
        {
            if (_teleportManager == default)
            {
                _teleportManager = FindFirstObjectByType<TeleportManager>();
            }

            if (_playerManager == default)
            {
                _playerManager = FindFirstObjectByType<PlayerManager>();
            }

            if (_leftDoorAnimator == default || _rightDoorAnimator == default)
            {
                Debug.Log("right or left door animator is default!");
            }
        }

        /// <summary>
        /// Set <see cref="Action"/>s using in <see cref="StairDoorController"/>
        /// </summary>
        protected virtual void SetActionSubscribers()
        {
            //_playerManager.OnInteractDoor.AddListener(UpdateAnimation);
        }

        /// <summary>
        /// Set <see cref="_isPlayerOnEndStageRegion"/> true when OnEnterEndStageRegion Invoked.
        /// </summary>
        /// <param name="transform">Get player transform from <see cref="PlayerManager.OnEnterEndStageRegion"/></param>
        protected virtual void SubscribeOnEnterEndStageRegion(Transform transform)
        {

            _isPlayerOnEndStageRegion = true;

#if UNITY_EDITOR
            //Debug.Log($"_isPlayerOnEndStageRegion {_isPlayerOnEndStageRegion}");
#endif
        }

        /// <summary>
        /// Set <see cref="_isPlayerOnEndStageRegion"/> false when OnExitEndStageRegion Invoked.
        /// </summary>
        protected virtual void SubscribeOnExitEndStageRegion()
        {
            _isPlayerOnEndStageRegion = false;
#if UNITY_EDITOR
            //Debug.Log($"_isPlayerOnEndStageRegion {_isPlayerOnEndStageRegion}");
#endif
        }

        protected virtual void OnEnable()
        {

        }

        private IEnumerator SetStateCoroutine(DoorStateList newState)
        {
            if (_state == newState)
            {
                yield break;
            }
            _state = newState;
            float animationLength;
            Animator activeAnimator = null;
            if (_rightDoorAnimator && _leftDoorAnimator)
            {
                _rightDoorAnimator.SetInteger("DoorState", (int)_state);
                activeAnimator = _leftDoorAnimator;
            }
            else if (_leftDoorAnimator != null)
            {
                activeAnimator = _leftDoorAnimator;
            }
            else if (_rightDoorAnimator != null)
            {
                activeAnimator = _rightDoorAnimator;
            }
            activeAnimator.SetInteger("DoorState", (int)_state);
#if UNITY_EDITOR
            Debug.Log($"now active animator is {activeAnimator.name}");
#endif

            if (activeAnimator != null)
            {
                AnimatorStateInfo stateInfo = activeAnimator.GetCurrentAnimatorStateInfo(0);
                animationLength = stateInfo.length;
            }
            else
            {
#if UNITY_EDITOR
                Debug.Log("All Animator is null!");
#endif
                yield break;
            }

            float elapsedTime = 0f;
            while (elapsedTime < animationLength && elapsedTime < _animationTimeout)
            {
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            if (elapsedTime >= _animationTimeout)
            {
                Debug.LogWarning("Animation timeout occurred!");
            }
        }

        /// <summary>
        /// Start Coroutine to Activate Collision.
        /// </summary>
        public virtual void ActivateCollision()
        {
            StartCoroutine(ActivateCollisionCoroutine());
        }

        /// <summary>
        /// Activate collision when DoorClose Aninmation is ended.
        /// </summary>
        /// <returns><see cref="SetStateCoroutine"/></returns>
        protected virtual IEnumerator ActivateCollisionCoroutine()
        {
            _transparentCollision.GetComponent<BoxCollider>().enabled = true;
            yield return StartCoroutine(SetStateCoroutine(DoorStateList.DoorClose));
        }

        /// <summary>
        /// Start Coroutine to Deactivate Collision.
        /// </summary>
        public virtual void DeactivateCollision()
        {
            StartCoroutine(DeactivateCollisionCoroutine());
        }

        protected virtual IEnumerator DeactivateCollisionCoroutine()
        {
#if UNITY_EDITOR
            Debug.Log("SetStateCoroutine Start");
#endif
            _transparentCollision.GetComponent<BoxCollider>().enabled = false;
            yield return StartCoroutine(SetStateCoroutine(DoorStateList.DoorOpen));
        }

        protected virtual IEnumerator CloseDoorCoroutine()
        {
            _isDoorOpened = false;
            yield return StartCoroutine(SetStateCoroutine(DoorStateList.DoorClose));
        }

        public virtual void InteractDoor()
        {
            if (_isDoorOpened == false)
            {
                StartCoroutine(OpenDoorCoroutine());
            }
            else
            {
                StartCoroutine(CloseDoorCoroutine());
            }
        }

        protected virtual IEnumerator OpenDoorCoroutine()
        {
            _isDoorOpened = true;
            yield return StartCoroutine(SetStateCoroutine(DoorStateList.DoorOpen));
        }
    }
}
