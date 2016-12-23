using UnityEngine;
using System.Collections;

public class IntroCutsceneScript : MonoBehaviour
{
    float timer;
    public float movieLengthInSeconds;

    public LoadController load;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        Renderer r = GetComponent<Renderer>();
        MovieTexture movie = (MovieTexture)r.material.mainTexture;

        timer += Time.deltaTime;

        if (timer > movieLengthInSeconds)
        {
            //load.LoadScene etc...
        }

    }
}
