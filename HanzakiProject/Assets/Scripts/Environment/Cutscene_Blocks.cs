using UnityEngine;
using System.Collections;

public class Cutscene_Blocks : MonoBehaviour
{
    public void Activate()
    {
        GetComponent<Rigidbody>().isKinematic = false;
    }


	
}
