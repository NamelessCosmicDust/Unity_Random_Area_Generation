using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is a static class that includes all other helpful methods to generate game play areas.
/// Non-MonoBehavior class.
/// </summary>
public static class AreaGenerationHelpers
{
    /// <summary>
    /// Takes an area object and add it to a specified instance of AreaObjectData with its position.
    /// </summary>
    /// <param name="instance"></param>
    /// <param name="area"></param>
    public static void AddToAreaObjectData(List<AreaObjectData> instance, GameObject area)
    {
        instance.Add(new AreaObjectData
            { areaObject = area, areaVector = area.transform.position }
        );
    }

    /// <summary>
    /// Returns a random rotation angle.
    /// </summary>
    /// <returns></returns>
    public static Quaternion GetRandomQuaternion()
    {
        int randomAngle= Random.Range(0, 360);

        return Quaternion.Euler(0, 0, randomAngle);
    }

    /// <summary>
    /// Returns a random vector based on the center point as the datum and search distance as the vector size.
    /// </summary>
    /// <param name="centerPoint"></param>
    /// <param name="searchDistance"></param>
    /// <returns></returns>
    public static Vector2 GetRandomVector(Vector2 centerPoint, float searchDistance)
    {
        // Generate a random unit vector.
        Vector2 randomDirection = Random.insideUnitCircle.normalized;

        // Scale the vector by the search distance.
        Vector2 randomVector = centerPoint + randomDirection * searchDistance;

        return randomVector;
    }

}
