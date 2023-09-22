using System;
using UnityEngine;
using UnityEngine.Events;

namespace MacounDemo.Logic
{
    public class SmartArrow : MonoBehaviour
    {
        public UnityEvent playerHit;
        public UnityEvent audienceHit;

        private readonly RaycastHit[] _hitResults = new RaycastHit[1];

        [SerializeField]
        private LayerMask layerMask;

        [SerializeField]
        private float maxRayDistance;

        private Transform _myTransform;

        private void Start()
        {
            _myTransform = transform;
        }

        private void Update()
        {
            var hitsCount =
                Physics.RaycastNonAlloc(_myTransform.position, _myTransform.forward, _hitResults, maxRayDistance,
                    layerMask);

            if (hitsCount <= 0)
            {
                return;
            }

            try
            {
                CheckCollidersAndTriggerEvents();
            }
            catch (NullReferenceException ex)
            {
                Debug.LogException(ex);
            }
        }

        private void CheckCollidersAndTriggerEvents()
        {
            var isAudience = _hitResults[0].collider.gameObject.GetComponent<Audience>() != null;

            if (isAudience)
            {
                audienceHit.Invoke();

                // We don't want to trigger two events in the same frame
                return;
            }

            var isPlayer = _hitResults[0].collider.gameObject.GetComponent<CharacterController>() != null;

            if (isPlayer)
            {
                playerHit.Invoke();
            }
        }

        private void OnDrawGizmosSelected()
        {
            var myTransform = transform;
            Debug.DrawRay(myTransform.position, myTransform.forward * maxRayDistance, Color.red);
        }
    }
}