using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using ARObjects;
using Spawner;

namespace City
{


    public class CityManager: MonoBehaviour
	{

		///  INSPECTOR VARIABLES       ///
		[SerializeField] ARPlaneManager _arPlaneManager;


        [Header("customizable int values")]

        [Tooltip("max amount objects to try to add when plane is found or updated")]
        [Min(0)][SerializeField] private int _maxAmountOfBuildingsToAdd = 5;

        [Tooltip("max amount of times to try to find a location to spawn a object")]
        [Min(0)][SerializeField] private int _maxAmountOfAttemptsToFindPosition = 5;

        [Tooltip("max amount objects to try to add when plane is found or updated")]
        [Min(0)][SerializeField] private int _maxAmountOfARObjectsAllowed = 100;

        [Header("customizable boolean values")]

        [Tooltip("delete oldest object after the amount of object surpasses _maxAmountOfARObjectsAllowed ")]
        [SerializeField] private bool _cullOldArObjectsAfterLimit=true;

        [Tooltip("spawn additional buildings as planes are updates")]
        [SerializeField] private bool _updateCityOnPlaneUpdated=false;

        [Tooltip("determines if the added ar object on touch counts towards the total count of AR objects")]
        [SerializeField] private bool _touchObjectsCountTowardsTotalARObjectCount;

        [Header("prefabs")]

        [Tooltip("prefab to try to spawn when floor plane found")]
        [SerializeField] private GameObject _floorObjectPrefab;

        ///  PRIVATE VARIABLES         ///

        private CityMakerManger _cityMakerManger;
        private Queue<BaseARObject> _arObjectQueue;

        private bool _updating;
        ///  PRIVATE METHODS           ///
       
        private void Awake()
        {
            _arObjectQueue = new Queue<BaseARObject>();
            _cityMakerManger=gameObject.AddComponent< CityMakerManger>();
            _arPlaneManager.trackablesChanged.AddListener(PlaneChanged);
        }

        private void PlaneChanged(ARTrackablesChangedEventArgs<ARPlane> args)
        {
            if (args.added != null )
            {

                foreach (ARPlane plane in args.added) 
                {
                    _cityMakerManger.PopulateCity(plane,  _maxAmountOfBuildingsToAdd, _maxAmountOfAttemptsToFindPosition,_floorObjectPrefab);
                }
            }
            if (args.updated != null && _updateCityOnPlaneUpdated && _updating==false && _arObjectQueue.Count<_maxAmountOfARObjectsAllowed)
            {     

                _updating = true;
                foreach (ARPlane plane in args.updated)
                {

                    _cityMakerManger.PopulateCity(plane, _maxAmountOfBuildingsToAdd, _maxAmountOfAttemptsToFindPosition, _floorObjectPrefab);

                }
                _updating = false;
            }

            
        }

        

        ///  PUBLIC API                ///
        public void AddObjectToQueue(ARPlane plane, BaseARObject obj, bool isTouchObject=false)
        {
            if (isTouchObject && !_touchObjectsCountTowardsTotalARObjectCount)
            {
                return;
            }
            _arObjectQueue.Enqueue(obj);
            if (_arObjectQueue.Count > _maxAmountOfARObjectsAllowed && _cullOldArObjectsAfterLimit)
            {
                _arObjectQueue.Dequeue().KillSafely();
            }
        }

    }
}
