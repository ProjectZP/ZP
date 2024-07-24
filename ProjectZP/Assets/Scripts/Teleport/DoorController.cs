using System;
using System.Collections;
using UnityEngine;
using ZP.SJH.Player;

namespace ZP.Villin.Teleport
{
    public enum DoorStateList
    {
        None,
        DoorClose,
        DoorOpen,
        Length
    }


    public abstract class DoorController : MonoBehaviour
    {
        [SerializeField] private GameObject _collision;
        [SerializeField] private Animator _animator;
        [SerializeField] private DoorStateList _state = DoorStateList.None;
        protected PlayerManager _playerManager;
        protected TeleportManager _teleportManager;
        protected const float _animationTimeout = 10f;
        protected bool _isPlayerOnEndStageRegion;


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

            if (_animator == default)
            {
                _animator = GetComponent<Animator>();
            }
        }

        /// <summary>
        /// Set <see cref="Action"/>s using in <see cref="StairDoorController"/>
        /// </summary>
        protected virtual void SetActionSubscribers()
        {
            _playerManager.OnEnterEndStageRegion += SubscribeOnEnterEndStageRegion;
            _playerManager.OnExitEndStageRegion += SubscribeOnExitEndStageRegion;
            //_playerManager.OnInteractDoor.AddListener(UpdateAnimation);
        }

        /// <summary>
        /// Set <see cref="_isPlayerOnEndStageRegion"/> true when OnEnterEndStageRegion Invoked.
        /// </summary>
        /// <param name="transform">Get player transform from <see cref="PlayerManager.OnEnterEndStageRegion"/></param>
        private void SubscribeOnEnterEndStageRegion(Transform transform)
        {

            _isPlayerOnEndStageRegion = true;

            Debug.Log($"_isPlayerOnEndStageRegion {_isPlayerOnEndStageRegion}");
        }

        /// <summary>
        /// Set <see cref="_isPlayerOnEndStageRegion"/> false when OnExitEndStageRegion Invoked.
        /// </summary>
        protected virtual void SubscribeOnExitEndStageRegion()
        {
            _isPlayerOnEndStageRegion = false;
            Debug.Log($"_isPlayerOnEndStageRegion {_isPlayerOnEndStageRegion}");
        }

        protected virtual void OnEnable()
        {

        }

        private IEnumerator SetStateCoroutine(DoorStateList newState)
        {
            _state = newState;
            if (_animator == null)
            {
#if UNITY_EDITOR
                Debug.Log("Animator is null!");
#endif
                yield break;
            }

            _animator.SetInteger("DoorState", (int)_state);

            AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
            float animationLength = stateInfo.length;

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
            _collision.GetComponent<BoxCollider>().enabled = true;
            yield return StartCoroutine(SetStateCoroutine(DoorStateList.DoorClose));
        }

        /// <summary>
        /// Start Coroutine to Deactivate Collision.
        /// </summary>
        protected virtual void DeactivateCollision()
        {
            StartCoroutine(DeactivateCollisionCoroutine());
        }

        protected virtual IEnumerator DeactivateCollisionCoroutine()
        {
#if UNITY_EDITOR
            Debug.Log("SetStateCoroutine Start");
#endif
            _collision.GetComponent<BoxCollider>().enabled = false;
            yield return StartCoroutine(SetStateCoroutine(DoorStateList.DoorOpen));
        }
    }
}
