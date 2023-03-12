using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class SpawnTool : EditorWindow
{

    private GameObject spawnContainer;
    private string objectBaseName = "Spawner";
    private int objectID = 1;
    private Vector3 containerPosition = Vector3.zero;
    
    public GameObject enemyPrefab;
    public int maxSpawnCount;
    public float spawnDistance;
    public float destroyDistance;


    [MenuItem("Tools/SpawnerTool (powered by MineEX)")]
    public static void ShowWindow()
    {
        GetWindow(typeof(SpawnTool));
    }

    private void OnGUI()
    {
        GUILayout.Label("Spawner Tool", EditorStyles.boldLabel);

        GUILayoutOption width = GUILayout.Width(300);

        spawnContainer = EditorGUILayout.ObjectField("Spawn Area",spawnContainer, typeof(GameObject), false , width) as GameObject;
        objectBaseName = EditorGUILayout.TextField("Spawn Area Name", objectBaseName, width);
        objectID = EditorGUILayout.IntField("Spawn Area Counter", objectID, width);
        EditorGUILayout.Space(5);
        containerPosition = EditorGUILayout.Vector3Field("Spawn Area Position", containerPosition, width);

        EditorGUILayout.Space(10);
        enemyPrefab = EditorGUILayout.ObjectField("Enemy", enemyPrefab, typeof(GameObject), false, width) as GameObject;
        //ändra 50 här under för möjlighet att spawna in fler fiender. alternativt byt ut till "IntField"
        maxSpawnCount = EditorGUILayout.IntSlider("Max Spawn Count", maxSpawnCount, 0, 50, width);
        spawnDistance = EditorGUILayout.FloatField("Enemy Spawn Distance", spawnDistance );

        bool spawnButton = GUILayout.Button("SpawnMesh", width);

        if (spawnButton) { SpawnMeshCube(); }


    }

    private void SpawnMeshCube()
    {
        if (spawnContainer != null)
        {
            

            GameObject clone = Instantiate(spawnContainer, containerPosition, Quaternion.identity);
            clone.name = objectBaseName + objectID;
            objectID++;
            clone.GetComponent<SpawnHandeler>().enemy = enemyPrefab;
            clone.GetComponent<SpawnHandeler>().maxSpawnCount = maxSpawnCount;
            clone.GetComponent<SpawnHandeler>().spawnDistance = spawnDistance;
            
        }
        else
        {
            Debug.Log("<color=#FF00E0>Error: Mesh Container Not Set</color>");
        }
    }
}

//https://www.mineex.se