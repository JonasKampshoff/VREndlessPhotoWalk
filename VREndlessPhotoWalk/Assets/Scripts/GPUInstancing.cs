using UnityEditor;
using UnityEngine;

public class GPUInstancing : MonoBehaviour
{

    [SerializeField] private Mesh mesh;
    [SerializeField] private float meshScaling = 1;
    [SerializeField] private float spawnAreaSize = 2;
    [SerializeField] private int amountOfObjects = 5;

    [SerializeField] private Material material;
    [SerializeField] public Vector3[] positions;


    private Matrix4x4[] matrices;




    private void Start()
    {
        matrices = new Matrix4x4[amountOfObjects * positions.Length];


        for(int i = 0; i < positions.Length; i++)
        {
            for(int j = 0; j < amountOfObjects; j++)
            {
                Vector2 vector2 = Random.insideUnitCircle * spawnAreaSize;


                matrices[i* amountOfObjects + j] = transform.localToWorldMatrix * Matrix4x4.TRS(new Vector3(vector2.x, 0, vector2.y) + positions[i], Quaternion.Euler(-90, Random.Range(0,360), 0), Vector3.one * meshScaling);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Graphics.DrawMeshInstanced(mesh,0,material,matrices);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        for(int i = 0;i < positions.Length; i++)
        {
            Gizmos.DrawWireSphere(transform.position + positions[i],spawnAreaSize);
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(GPUInstancing))]
public class DrawHandles : Editor
{
    void OnSceneGUI()
    {
        Debug.Log("Draw");
        GPUInstancing gPUInstancing = target as GPUInstancing;

        if (gPUInstancing.positions == null ||
            gPUInstancing.positions.Length == 0)
            return;

        for (int i = 0; i < gPUInstancing.positions.Length; i++)
        {
            Vector3 x = gPUInstancing.positions[i];
            gPUInstancing.positions[i] = Handles.DoPositionHandle(gPUInstancing.transform.position+x, Quaternion.identity) - gPUInstancing.transform.position;
        }

  
    }
}
#endif
