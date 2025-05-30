using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject objectToDuplicate;
    public Vector3 spawnOffset = Vector3.zero;
    public float spawnInterval = 1.0f;
    public Vector3 resetPosition = Vector3.zero; // 다음 라운드에 이동할 위치

    private float nextSpawnTime = 1f;
    private List<GameObject> spawnedObjects = new List<GameObject>();

    void Update()
    {
        if (Time.time >= nextSpawnTime && NEWUI.isBuyPanelInteractable == false)
        {
            DuplicateObject();
            nextSpawnTime = Time.time + spawnInterval;
        }

        if (NEWUI.isBuyPanelInteractable)
        {
            DestroyAllSpawnedObjectsAndReset();
        }
    }

    void DuplicateObject()
    {
        if (objectToDuplicate != null)
        {
            Vector3 spawnPosition = transform.position + spawnOffset;
            GameObject newObject = Instantiate(objectToDuplicate, spawnPosition, objectToDuplicate.transform.rotation);
            spawnedObjects.Add(newObject);
        }
    }

    void DestroyAllSpawnedObjectsAndReset()
    {
        foreach (GameObject obj in spawnedObjects)
        {
            if (obj != null)
                Destroy(obj);
        }

        spawnedObjects.Clear();

        if (objectToDuplicate != null)
        {
            // 오브젝트 위치 리셋
            objectToDuplicate.transform.position = resetPosition;

            // 다시 활성화 (SetActive true)
            if (!objectToDuplicate.activeSelf)
                objectToDuplicate.SetActive(true);
        }
    }
}