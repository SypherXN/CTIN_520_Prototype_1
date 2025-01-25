using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{

    public GameObject targetPrefab;

    private Controller controller;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        controller = GetComponent<Controller>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            GameObject[] currentPrefabs = FindObjectsByType<GameObject>(FindObjectsSortMode.None);

            foreach(GameObject obj in currentPrefabs)
            {

                if (obj.CompareTag(targetPrefab.tag))
                {

                    if (inFOV(obj))
                    {

                        Destroy(obj);

                        if (controller.timerRunning()) controller.increaseScore();

                    }

                }

            }

        }

    }

    private bool inFOV(GameObject obj)
    {

        Vector3 dirToObject = obj.transform.position - Camera.main.transform.position;

        float angle = Vector3.Angle(Camera.main.transform.forward, dirToObject);

        return angle < Camera.main.fieldOfView / 2;

    }
}
