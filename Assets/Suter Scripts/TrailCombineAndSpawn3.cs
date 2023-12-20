using UnityEngine;

public class TrailCombineAndSpawn3 : MonoBehaviour
{
    public GameObject trailPrefab;
    public Transform spawnPoint;
    [SerializeField] Transform originalPoint;
    [SerializeField] float sphereScaleFactor = 0.2f;

    void Start()
    {
        // Add an onClick listener to your UI button
        // Example: GetComponent<Button>().onClick.AddListener(CombineTrailsAndSpawn);
    }

    // 2023-12-14 AI-Tag 
    // This was created with assistance from Muse, a Unity Artificial Intelligence product

    public void CombineTrailsAndSpawn()
    {
        GameObject[] trailObjects = GameObject.FindGameObjectsWithTag("Trail");
        CombineInstance[] combineInstances = new CombineInstance[trailObjects.Length];

        for (int i = 0; i < trailObjects.Length; i++)
        {
            TrailRenderer trailRenderer = trailObjects[i].GetComponent<TrailRenderer>();
            CombineInstance combineInstance = new CombineInstance();

            // Create a new mesh for combining
            Mesh meshToCombine = new Mesh();
            trailRenderer.BakeMesh(meshToCombine, true);

            combineInstance.mesh = meshToCombine;
            combineInstance.transform = originalPoint.transform.localToWorldMatrix; //trailRenderer.transform.localToWorldMatrix;
            combineInstances[i] = combineInstance;
        }

        // Instantiate the prefab
        GameObject prefabInstance = Instantiate(trailPrefab, Vector3.zero, Quaternion.identity);

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

        // Scale down the prefab instance to fit the smaller sphere
        prefabInstance.transform.localScale = Vector3.one * sphereScaleFactor;

        // Move the prefab instance to the spawn point
        prefabInstance.transform.position = spawnPoint.position;
    }

    /*
        public void CombineTrailsAndSpawn2()
        {
            GameObject[] trailObjects = GameObject.FindGameObjectsWithTag("Trail");
            CombineInstance[] combineInstances = new CombineInstance[trailObjects.Length];

            for (int i = 0; i < trailObjects.Length; i++)
            {
                TrailRenderer trailRenderer = trailObjects[i].GetComponent<TrailRenderer>();
                CombineInstance combineInstance = new CombineInstance();

                // Create a new mesh for combining
                Mesh meshToCombine = new Mesh();
                trailRenderer.BakeMesh(meshToCombine, true);

                // Scale and position each vertex of the mesh
                Vector3[] vertices = meshToCombine.vertices;
                for (int j = 0; j < vertices.Length; j++)
                {
                    // Transform the vertex position from local space to world space
                    Vector3 worldSpaceVertexPosition = trailRenderer.transform.TransformPoint(vertices[j]);

                    // Calculate distance and direction from the center of the original sphere
                    Vector3 directionFromCenter = worldSpaceVertexPosition - originalPoint.position;
                    float distFromCenter = directionFromCenter.magnitude;

                    // Scale the distance
                    float scaledDist = distFromCenter * sphereScaleFactor;

                    // Calculate the scaled position for the vertex in world space
                    Vector3 scaledWorldSpaceVertexPosition = originalPoint.position + directionFromCenter.normalized * scaledDist;

                    // Transform the scaled position from world space back to the local space of the mesh
                    //vertices[j] = meshToCombine.transform.InverseTransformPoint(scaledWorldSpaceVertexPosition);
                    vertices[j] = spawnPoint.transform.InverseTransformPoint(scaledWorldSpaceVertexPosition);
                }

                // Assign the new vertices back to the mesh
                meshToCombine.vertices = vertices;

                combineInstance.mesh = meshToCombine;
                combineInstance.transform = Matrix4x4.identity;
                combineInstances[i] = combineInstance;
            }

            // Instantiate the prefab
            GameObject prefabInstance = Instantiate(trailPrefab, spawnPoint.position, spawnPoint.rotation);

            // Get or add a MeshFilter component to the prefab
            MeshFilter prefabMeshFilter = prefabInstance.GetComponent<MeshFilter>();
            if (prefabMeshFilter == null)
            {
                prefabMeshFilter = prefabInstance.AddComponent<MeshFilter>();
            }

            // Combine all the individual meshes into one
            Mesh combinedMesh = new Mesh();
            combinedMesh.CombineMeshes(combineInstances, true);
            prefabMeshFilter.mesh = combinedMesh;
        } */

}
