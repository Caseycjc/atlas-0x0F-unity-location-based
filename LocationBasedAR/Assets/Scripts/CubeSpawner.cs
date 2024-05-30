using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    public GameObject cubePrefab;
    public float spawnDistance = 2f;
    public Vector3 spawnScale = new Vector3(0.5f, 0.5f, 0.5f);  // Adjusted scale

    private GameObject currentCube;  // Keep track of the currently spawned cube

    public void SpawnCube()
    {
        // Destroy the current cube if it exists
        if (currentCube != null)
        {
            Destroy(currentCube);
        }

        // Calculate spawn position
        Vector3 spawnPosition = Camera.main.transform.position + Camera.main.transform.forward * spawnDistance;

        // Spawn the new cube
        currentCube = Instantiate(cubePrefab, spawnPosition, Quaternion.identity);
        currentCube.transform.localScale = spawnScale;  // Adjusting the scale after spawning
    }
}
