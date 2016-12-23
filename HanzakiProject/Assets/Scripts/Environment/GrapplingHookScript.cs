using UnityEngine;
using System.Collections;

public class GrapplingHookScript : MonoBehaviour
{
    public GrapplingHook hook;
    public StatsManager stats;
    public Transform cameraPos;
    public bool destroyObject;
    public GameObject smokeParticles;

    void Awake()
    {
        stats = GameObject.Find("GameManager").GetComponent<StatsManager>();
        hook = GameObject.Find("Player").GetComponent<GrapplingHook>();
    }

    //Acivate the hook, focus camera on hook if player is in range.
    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player" && stats.grapplingHookUnlocked)
        {
            Debug.Log("HOOK");
            Camera.main.GetComponent<CameraController>().hookObject = gameObject;
            hook.canHook = true;
            hook.hook = transform;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(col.gameObject.tag == "Player" && stats.grapplingHookUnlocked)
        {
            Camera.main.GetComponent<CameraController>().hookObject = null;
            hook.canHook = false;
        }
    }

    public void CreateSmoke()
    {
        GameObject spawnedSmoke = (GameObject)Instantiate(smokeParticles, transform.position, Quaternion.identity);
        Destroy(spawnedSmoke, 2f);
        GetComponent<Cutscene_BlocksInitiate>().Initiate();
    }

}
