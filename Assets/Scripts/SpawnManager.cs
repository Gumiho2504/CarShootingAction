using UnityEngine;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour
{
    [Header("Pooling Settings")]
    [SerializeField] private GameObject[] animalPrefabs;
    [SerializeField] private int poolSize = 10; // per prefab

    [Header("Spawn Settings")]
    private float spawnInterval = 1.5f;
    private float spawnDelay = 2f;
    [SerializeField] private float zPos;
    [SerializeField] private float xRange;

    private List<Queue<GameObject>> pools = new List<Queue<GameObject>>();

    void Start()
    {
        CreatePools();
        InvokeRepeating(nameof(SpawnRandomAnimal), spawnDelay, spawnInterval);
    }


    void CreatePools()
    {
        foreach (var prefab in animalPrefabs)
        {
            Queue<GameObject> newPool = new Queue<GameObject>();

            for (int i = 0; i < poolSize; i++)
            {
                GameObject obj = Instantiate(prefab);
                obj.SetActive(false);

                ObjectPooler poolObj = obj.AddComponent<ObjectPooler>();
                poolObj.OnReturnToPool += ReturnObjectToPool;

                newPool.Enqueue(obj);
            }

            pools.Add(newPool);
        }
    }

    GameObject GetFromPool(int index)
    {
        Queue<GameObject> pool = pools[index];

        if (pool.Count == 0)
        {
            Debug.LogWarning("Pool empty, expanding...");
            GameObject newObj = Instantiate(animalPrefabs[index]);
            newObj.SetActive(false);
            pool.Enqueue(newObj);
        }

        GameObject obj = pool.Dequeue();
        obj.SetActive(true);
        return obj;
    }

    void ReturnObjectToPool(ObjectPooler obj)
    {
        obj.gameObject.SetActive(false);

        // Find which pool it belongs to
        for (int i = 0; i < animalPrefabs.Length; i++)
        {
            if (obj.gameObject.name.Contains(animalPrefabs[i].name))
            {
                pools[i].Enqueue(obj.gameObject);
                break;
            }
        }
    }


    private void SpawnRandomAnimal()
    {
        if (FindFirstObjectByType<PlayerController>() == null)
            return;
        if (FindFirstObjectByType<PlayerController>().isDie)
            return;
        int index = Random.Range(0, animalPrefabs.Length);
        Vector3 spawnPos = new Vector3(Random.Range(-xRange, xRange), 0, zPos);

        GameObject obj = GetFromPool(index);
        obj.GetComponent<MoveForward>().speed = FindFirstObjectByType<GameController>().speed;
        obj.transform.position = spawnPos;
        obj.transform.rotation = animalPrefabs[index].transform.rotation;
    }
}
