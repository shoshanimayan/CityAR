using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using ARObjects;
using Spawner;

namespace City
{
    [RequireComponent(typeof(SpawnManager))]

    public class CityMakerManger: MonoBehaviour
	{


        ///  PRIVATE VARIABLES         ///
        private SpawnManager _spawnManager;

        ///  PRIVATE METHODS           ///

        private void Awake()
        {
            _spawnManager = GetComponent<SpawnManager>();
        }

        private Vector3 RandomPointInBounds(Collider collider)
        {
            return collider.bounds.center + new Vector3(
               (Random.value - 0.5f) * collider.bounds.size.x,
               (Random.value - 0.5f) * collider.bounds.size.y,
               (Random.value - 0.5f) * collider.bounds.size.z
            );
        }


        ///  PUBLIC API                ///
        public void PopulateCity(ARPlane plane, int maxBuildings, float maxAttempts, GameObject prefab )
		{
            float distance = 0;
            if (prefab.GetComponent<BaseARObject>() != null)
            {
               distance= prefab.GetComponent<BaseARObject>().GetMinPlacementDistance();
            }
            int currentAttempts = 0;
            int buildingsMade = 0;
            List<Vector3> triedPositions = new List<Vector3>();

            while (currentAttempts < maxAttempts && buildingsMade < maxBuildings)
            {
                Vector3 point = RandomPointInBounds(plane.transform.GetComponent<Collider>());
                bool postionFailed=false;
                //check against previous positions
                foreach (Vector3 pos in triedPositions)
                {
                    if (Vector3.Distance(point, pos) <= distance)
                    {
                        triedPositions.Add(point);
                        postionFailed = true;
                        break;
                    }
                }
                if (postionFailed)
                {
                    currentAttempts++;

                    continue;
                }

                //overlapSphere Check

                Collider[] hits = Physics.OverlapSphere(point, distance);
                foreach (Collider hit in hits) 
                {
                    if (hit.transform.GetComponent<BaseARObject>() != null )
                    {
                        triedPositions.Add(point);
                        postionFailed = true;
                        if (!triedPositions.Contains(hit.transform.position))
                        {
                            triedPositions.Add(hit.transform.position);
                        }
                        break;
                    }
                    

                }
                if (postionFailed)
                {
                    currentAttempts++;
                    continue;
                }
                else
                {
                    _spawnManager.SpawnAutomatically(point, plane, prefab);
                    buildingsMade++;

                }
            }

        }

       

    }
}
