using System;
using System.Threading;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class GPUInstancing : MonoBehaviour
{
    [Serializable]
    public struct SpawnCondition
    {
        public Vector3 position;
        public int min;
        public int max;
        public float spawnAreaSize;
    }

    [SerializeField] private Mesh mesh;
    [SerializeField] private float meshScaling = 1;

    [SerializeField] private Material material;
    [SerializeField] public SpawnCondition[] spawnConditions;


    private Matrix4x4[] matrices;
    private bool render = false;
    private Matrix4x4 localToWorldMatrix;
    private int seed;


    private void Start()
    {
        seed = UnityEngine.Random.Range(0, 100000000);
        localToWorldMatrix = transform.localToWorldMatrix;
        Thread thread = new Thread(CreateMatrix);
        thread.Start();
    }

    private void CreateMatrix()
    {
        System.Random random = new System.Random(seed);
        List<Matrix4x4> list = new List<Matrix4x4>();

        foreach(SpawnCondition x in spawnConditions)
        {
            int amount = random.Next(x.min,x.max);
            for (int i = 0; i < amount; i++)
            {
                Vector2 spawnPosition = RandomInsideUnitCircle(random) * x.spawnAreaSize;
                list.Add(localToWorldMatrix * Matrix4x4.TRS(new Vector3(spawnPosition.x, 0, spawnPosition.y) + x.position, Quaternion.Euler(-90, random.Next(0, 360), 0), Vector3.one * meshScaling));

            }
        }

        matrices = list.ToArray();
        render = true;
    }

    private Vector2 RandomInsideUnitCircle(System.Random random)
    {
        float x;
        float y;
        while (true)
        {
            x = (float)random.NextDouble() * 2 - 1;
            y = (float)random.NextDouble() * 2 - 1;
            if (x*x+y*y < 1)
            {
                break;
            }
        }
        return new Vector2(x,y);
    }

    // Update is called once per frame
    void Update()
    {
        if (render)
        {
            Graphics.DrawMeshInstanced(mesh, 0, material, matrices);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        foreach (SpawnCondition x in spawnConditions)
        {
            Gizmos.DrawWireSphere(transform.position + x.position,x.spawnAreaSize);
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

        if (gPUInstancing.spawnConditions == null ||
            gPUInstancing.spawnConditions.Length == 0)
            return;

        for (int i = 0; i < gPUInstancing.spawnConditions.Length; i++)
        {
            Vector3 position = gPUInstancing.transform.position + gPUInstancing.spawnConditions[i].position;
            Handles.Label(position, "  " + i);
            gPUInstancing.spawnConditions[i].position = Handles.DoPositionHandle(position, Quaternion.identity) - gPUInstancing.transform.position;
        }

  
    }
}
#endif
