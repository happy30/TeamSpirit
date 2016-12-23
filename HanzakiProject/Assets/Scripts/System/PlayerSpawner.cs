using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PlayerSpawner : MonoBehaviour
{
    public List<Vector3> spawnPositions;
    public ProgressionManager progression;

    void Start()
    {
        progression = GameObject.Find("GameManager").GetComponent<ProgressionManager>();
        transform.position = progression.spawnPoints[progression.spawnPointNumber];
    }

}
