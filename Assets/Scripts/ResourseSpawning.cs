using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.AI;

public class ResourseSpawning : MonoBehaviour
{
    public GameObject CopperNode;
    public Transform planeTransform;
    public int maxNumOfNodes = 5;
    public float spawnDelay = 1f;

    private List<GameObject> activeNodes = new List<GameObject>();
    public static ResourseSpawning instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnLoop());
    }

    private IEnumerator spawnLoop()
    {
        while (true)
        {
            if (activeNodes.Count < maxNumOfNodes)
            {
                SpawnNode();
            }
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private void SpawnNode()
    {
        float planeWidth = planeTransform.localScale.x * 10f;
        float planeDepth = planeTransform.localScale.z * 10f;

        float randomX = Random.Range(-planeWidth / 2, planeWidth / 2);
        float randomZ = Random.Range(-planeDepth / 2, planeDepth / 2);
        float yPosition = 0.5f;

        Vector3 spawnPostion = new Vector3(randomX, yPosition, randomZ);

        GameObject newNode = Instantiate(CopperNode, spawnPostion + planeTransform.position, Quaternion.identity);
        activeNodes.Add(newNode);

    }

    public void destroyNode(GameObject node)
    {
        if (activeNodes.Contains(node))
        {
            activeNodes.Remove(node);
        }
    }
}
