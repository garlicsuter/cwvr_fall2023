using UnityEngine;

public class TrailCombineAndSpawn2 : MonoBehaviour
{
    public GameObject trailPrefab;
    public Transform spawnPoint;

    void Start()
    {
        // Add an onClick listener to your UI button
        // Example: GetComponent<Button>().onClick.AddListener(CombineTrailsAndSpawn);
    }

    public void CombineTrailsAndSpawn()
    {
        // Find all GameObjects with the "Trail" tag
        GameObject[] trailObjects = GameObject.FindGameObjectsWithTag("Trail");

        // Create a list to store CombineInstance objects
        CombineInstance[] combineInstances = new CombineInstance[trailObjects.Length];

        // Iterate through each trail object
        for (int i = 0; i < trailObjects.Length; i++)
        {
            // Get the TrailRenderer component
            TrailRenderer trailRenderer = trailObjects[i].GetComponent<TrailRenderer>();

            // Create a CombineInstance and set its transform and mesh
            CombineInstance combineInstance = new CombineInstance();
            combineInstance.transform = trailRenderer.transform.localToWorldMatrix;
            combineInstance.mesh = new Mesh();
            trailRenderer.BakeMesh(combineInstance.mesh, true);

            // Store the CombineInstance in the list
            combineInstances[i] = combineInstance;
        }

        // Create a new GameObject for the prefab
        GameObject prefabInstance = Instantiate(trailPrefab, spawnPoint.position, spawnPoint.rotation);

        // Get or add a MeshFilter component to the prefab
        MeshFilter prefabMeshFilter = prefabInstance.GetComponent<MeshFilter>();
        if (prefabMeshFilter == null)
        {
            prefabMeshFilter = prefabInstance.AddComponent<MeshFilter>();
        }

        // Combine all the meshes into one
        Mesh combinedMesh = new Mesh();
        combinedMesh.CombineMeshes(combineInstances, true);

        // Set the combined mesh to the MeshFilter
        prefabMeshFilter.mesh = combinedMesh;
    }
}