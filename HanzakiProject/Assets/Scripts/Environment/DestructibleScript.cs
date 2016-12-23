//Made by Sascha Greve

using UnityEngine;
using System.Collections;

public class DestructibleScript : MonoBehaviour
{
    public GameObject particleObject;
    GameObject spawnedParticleObject;
    GameObject self;

    public bool isStoryRelated;

    void Start()
    {
        self = gameObject;
    }

    public void DestroyObject ()
    {
        if(isStoryRelated)
        {
            GameObject.Find("GameManager").GetComponent<QuestManager>().NextTask();
            GameObject.Find("Canvas").GetComponent<UIManager>().SetQuestsText();
        }
        Destroy(spawnedParticleObject = (GameObject)Instantiate(particleObject, transform.position, Quaternion.identity), 3);
        Destroy(gameObject);
    }
}
