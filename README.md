# Unity_Random_Area_Generation
A random play area generator for Unity Engine.

The idea is that an area object is responsible for spawning a new area object. Basically, spanwing the first area will start a chain reaction of area spawning.
Each new area will be spawned in a random direction, but a fixed distance away from the previous area object.
There is also an additional clearance criteria to prevent an area spawn on top of another area.

### How to use:
1. Attach ```AreaManager``` class script to the AreaManager prefab, then spawn the AreaManager prefab.
```AreaManager``` will Instantiate areaPrafab (AreaRectangle), and each AreaRectangle will run ```AreaSpawner``` class to start the chain reaction of area spawning until the total count hits 20 (areaSize = 20)
2. ```AreaObjectData``` class stores info for each spawned area object, ```AreaGenerationHelpers``` class provides static methods for common actions.
3. ```AreaManager``` will use WallDestroyer prefab (has ```WallDestroyer``` class), spawn it for each area object spawned, by using the data from an instance of ```AreaObjectData``` class, to destroy walls between areas.
4. ```WallDestroyer``` class is set to destroy all objects with tag "PlayAreaWall". Make sure only the Wall prefab has this tag in Unity.
