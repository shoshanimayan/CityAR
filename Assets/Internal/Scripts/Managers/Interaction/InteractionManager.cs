using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.InputSystem;
using Spawner;

using ARObjects;

namespace Interaction
{
    [RequireComponent(typeof(SpawnManager))]
    public class InteractionManager: MonoBehaviour
	{

        ///  INSPECTOR VARIABLES       ///
        [SerializeField] private ARRaycastManager _ARraycastManager;
		///  PRIVATE VARIABLES         ///
		private SpawnManager _spawnManager;
        private List<ARRaycastHit> hits = new List<ARRaycastHit>();
        private TouchControls _controls;

        ///  PRIVATE METHODS           ///
        private void Awake()
        {
            _spawnManager = GetComponent<SpawnManager>();
            _controls= new TouchControls();
            _controls.Control.Touch.started += ctx =>
            {
                if (ctx.control.device is Pointer touch)
                {
                    OnPress(touch.position.ReadValue());
                }
            };

        }

        private void OnPress(Vector3 postion)
        {
            

                Ray ray = Camera.main.ScreenPointToRay(postion);
                RaycastHit hitObj;

                if (Physics.Raycast(ray, out hitObj))
                {
                    if (hitObj.transform.GetComponent<BaseARObject>()!=null)
                    { 
                        BaseARObject baseARObject= hitObj.transform.GetComponent<BaseARObject>();
                        baseARObject.OnTouch();
                        return;
                    }
                }

                if (_ARraycastManager.Raycast(postion, hits, TrackableType.PlaneWithinPolygon)) {
                    var hitPose= hits[0].pose;
                    ARPlane hitPlane = hits[0].trackable.gameObject.GetComponent<ARPlane>();
                    _spawnManager.SpawnOnTouch(hitPose,hitPlane);
                }

            
        }

        private void OnEnable()
        {
            _controls.Control.Enable();
        }

        private void OnDisable()
        {
            _controls.Control.Disable();
        }



    }
}
