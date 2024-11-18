using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using City;
using ARObjects;

namespace Spawner
{
	public class SpawnManager: MonoBehaviour
	{

		///  INSPECTOR VARIABLES       ///
		[SerializeField] private GameObject _touchSpawnObjPrefab;

     
        ///  PRIVATE VARIABLES         ///
        private CityManager _cityManager;
        ///  PRIVATE METHODS           ///
        private void Awake()
        {
            _cityManager = GetComponent<CityManager>();
        }

        ///  PUBLIC API                ///
        public void SpawnOnTouch(Pose pose,ARPlane arPlane)
        {
            Vector3 cameraPostition= Camera.main.transform.position;
            var direction = cameraPostition - pose.position;
            Vector3 ObjRotation = Quaternion.LookRotation(direction).eulerAngles;
            GameObject obj=  Instantiate(_touchSpawnObjPrefab, pose.position, Quaternion.Euler(pose.rotation.x,ObjRotation.y,pose.rotation.z));
            BaseARObject baseARObj= obj.GetComponent<BaseARObject>();

            if (baseARObj != null && arPlane != null)
            {
                _cityManager.AddObjectToQueue(arPlane, baseARObj, true);
            }

        }

        public void SpawnAutomatically(Vector3 position, ARPlane arPlane, GameObject prefab)
        {
            Vector3 cameraPostition = Camera.main.transform.position;
            var direction = cameraPostition - position;
            Vector3 ObjRotation = Quaternion.LookRotation(direction).eulerAngles;
            GameObject obj = Instantiate(prefab, position, Quaternion.Euler(Quaternion.identity.x, ObjRotation.y, Quaternion.identity.z));
            BaseARObject baseARObj = obj.GetComponent<BaseARObject>();
           
            if (baseARObj != null && arPlane != null)
            {
                _cityManager.AddObjectToQueue(arPlane, baseARObj);
            }
            

        }

    }
}
