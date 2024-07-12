using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// AreaManager initiates spawning of area objects, and generates other objects within the area.
/// </summary>
public class AreaManager : MonoBehaviour
{
	public GameObject areaPrefab; // Currently only 1 rectangular object. Rework to a list in the future.
    public GameObject wallDestroyer; // Destroyer will remove walls between areas and connect them.
    public GameObject playerPrefab;
        
    public List<AreaObjectData> areaObjectData;
    public int areaSize; // areaSize is used to limit number of area creation.

    private void Start()
    {
		areaSize = 20;
        areaObjectData = new();

		// Create the first area.
		GameObject thisArea =
			Instantiate(areaPrefab, Vector2.zero, AreaGenerationHelpers.GetRandomQuaternion(), transform);
        
        // Add the position of the first area spawned above to the areaObjectData list.
        AreaGenerationHelpers.AddToAreaObjectData(areaObjectData, thisArea);

        StartCoroutine(AfterInstantiationActions());
    }

    private IEnumerator AfterInstantiationActions()
    {
        // Wait for AreaSpanwer class to finish area instantiation.
        yield return new WaitForSeconds(1);

        foreach (var data in areaObjectData)
        {
            // Wait for the wallDestroyer to remove walls within each area.
            yield return StartCoroutine(WallDestroyer(data.areaObject));
        }

        // Start other actions below.

        // Instantiate(playerPrefab, transform.parent);
    }

    private IEnumerator WallDestroyer(GameObject areaObject)
    {
        // Spawn a prefab wallDestroyer that has a collider.
        GameObject thisWallDestroyer =
            Instantiate(wallDestroyer, areaObject.transform.position, areaObject.transform.rotation, areaObject.transform);

        // Destroy all objects with tag "PlayAreaWall".
        // wallDestroyer has WallDestroyer class script for this.

        // destroy wallDestroyer.
        float destructionDelay = .1f;
        yield return new WaitForSeconds(destructionDelay);
        Destroy(thisWallDestroyer, destructionDelay);

        ////Debug.Log($"Wall destruction within: {areaObject.name}\n");
    }
}
