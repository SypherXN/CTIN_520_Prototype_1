using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class ObjectSpawner : MonoBehaviour
{

    public GameObject prefab;
    public int maxSpawn = 1;
    public float minDistance = 2.5f;
    public float maxDistance = 7.5f;
    public Camera cam;

    public float checkInterval = 1f;

    void Start()
    {

        if (cam == null) cam = Camera.main;

        StartCoroutine(SpawnPrefabs());

    }

    IEnumerator SpawnPrefabs()
    {
        while (true)
        {

            int prefabCount = countPrefabs();

            int numSpawn = maxSpawn - prefabCount;

            for (int i = 0; i < numSpawn; i++)
            {

                Vector3 spawnPos = Vector3.zero;
                bool validSpawn = false;

                while (!validSpawn)
                {

                    float distance = Random.Range(minDistance, maxDistance);
                    float horizontalAngle = Random.Range(0f, 360f);
                    float verticalAngle = Random.Range(0f, 360f);

                    Vector3 direction = new Vector3(Mathf.Cos(horizontalAngle * Mathf.Deg2Rad) * Mathf.Cos(verticalAngle * Mathf.Deg2Rad),
                                                    Mathf.Sin(verticalAngle * Mathf.Deg2Rad), 
                                                    Mathf.Sin(horizontalAngle * Mathf.Deg2Rad) * Mathf.Cos(verticalAngle * Mathf.Deg2Rad));

                    spawnPos = cam.transform.position + direction * distance;

                    if (!inFOV(spawnPos)) validSpawn = true;

                }

                Instantiate(prefab, spawnPos, Quaternion.identity);

            }

            yield return new WaitForSeconds(checkInterval);

        }

    }

    private bool inFOV(Vector3 pos)
    {

        Vector3 dirToTarget = pos - cam.transform.position;

        float angle = Vector3.Angle(cam.transform.forward, dirToTarget);
        return angle < cam.fieldOfView / 2;

    }

    private int countPrefabs()
    {

        int count = 0;
        GameObject[] currentPrefabs = FindObjectsByType<GameObject>(FindObjectsSortMode.None);

        foreach(GameObject obj in currentPrefabs)
        {

            if(obj.CompareTag(prefab.tag))
            {

                count++;

            }

        }

        return count;

    }

}
