using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace ARObjects
{
	public abstract class BaseARObject: MonoBehaviour
	{

        ///  INSPECTOR VARIABLES       ///  

        [Tooltip("min distance for the is object to have to other objects when spawned")]

        [Min(0f)] [SerializeField] private float _minDistance=.5f;

        [Tooltip("show debug visual for _minDistance")]

        [SerializeField] private bool _debugDistance = false;
		///  PRIVATE VARIABLES         ///

		///  PRIVATE METHODS           ///


		///  PUBLIC API                ///
		public float GetMinPlacementDistance()
		{ 
			return _minDistance;
		}

		public virtual void OnTouch()
		{
			KillSafely();
		}

		public void KillSafely()
		{
            Destroy(gameObject);

        }

        private void OnDrawGizmos()
        {
			Gizmos.color = Color.red;
			if (_debugDistance)
			{
				Gizmos.DrawWireSphere(transform.position, GetMinPlacementDistance());
			}
        }

    }
}
