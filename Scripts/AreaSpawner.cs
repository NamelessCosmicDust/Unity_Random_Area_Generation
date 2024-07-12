using System.Linq.Expressions;
using UnityEngine;

/// <summary>
/// AreaSpawner is attached to a spawnable area object prefab.
/// Responsible for spawning a next area.
/// </summary>
public class AreaSpawner : MonoBehaviour
{
    // centerPoint.transform should be the same as gameObject.transform,
    // if the area object is a square or a circle.
    [SerializeField] private GameObject centerPoint;

    private bool spawned = false; // To ensure that a spawner is used only once.

    private AreaManager areaManager;
    private GameObject areaObject;
    private Vector2 nextSpawnPosition;
    private float spawnDistance; // Distance to the next area spawn point.
    private float searchDistance; // search criteria for cleaarance before spawning an area object.
	
    private void Start()
	{
        // Assign existing AreaManager gameObject in the scene to this instance
        // to keep all the changes in the same instance of AreaManager.
        areaManager = GameObject.FindGameObjectWithTag("PlayArea").GetComponent<AreaManager>();
        areaObject = areaManager.areaPrefab;

        spawnDistance = 15f; // Currently area prefab is 20 x 20, so this allows 25% overlap.
        searchDistance = spawnDistance - 1f; // Needs to be less than spawnDistance, otherwise clearance will always be false.

		SpawnArea();
    }

	private void SpawnArea()
	{
        // Run the lines below until area found clear, or 20 time max.
        bool isAreaClear = false;
        int maxAttempts = 20;
        for (int i = 0; i < maxAttempts && !isAreaClear; i++)
        {
            // Get random vector for the next spawn area.
            nextSpawnPosition = AreaGenerationHelpers.GetRandomVector(centerPoint.transform.position, spawnDistance);
            // See if the surrounding is clear, if not, get another vector.
            isAreaClear = IsAreaClear(nextSpawnPosition, searchDistance);
        }


        // Spawn an area object only if there is enough space.
        if (isAreaClear && spawned == false)
		{
            // Spawn an area if the total number of areas is less than the AreaManager.areaSize.
            if (areaManager.areaObjectData.Count < areaManager.areaSize)
            {
                GameObject newArea = 
                    Instantiate(areaObject, nextSpawnPosition, AreaGenerationHelpers.GetRandomQuaternion(), areaManager.transform);
                newArea.name = areaObject.name + (areaManager.areaObjectData.Count + 1);
                
                // Add coord of this object to AreaManager.areaCoordinates.
                AreaGenerationHelpers.AddToAreaObjectData(areaManager.areaObjectData, newArea);
            }

		}
        spawned = true; // This spawner has been used.
	}

    private bool IsAreaClear(Vector2 position, float distance)
    {
        foreach (var data in areaManager.areaObjectData)
        {
            // If any area location is found to be within the distance,
            // the next spawn position is not clear.
            if (Vector2.Distance(position, data.areaVector) <= distance)
            {
                return false;
            }
        }
        return true;
    }

}
