/* LoopBGM by Jordi

MainTheme loopEnd: 273
MainTheme loopDuration: 133

*/

using UnityEngine;
using System.Collections;

public class LoopBGM : MonoBehaviour
{
    AudioSource _sound;
    public float loopEnd;
    public float loopDuration;
    AudioClip audioClip;


    void Awake ()
    {
        _sound = GetComponent<AudioSource>();
        audioClip = _sound.clip;
    }

    void Update()
    { 
        if (_sound.timeSamples > loopEnd * audioClip.frequency)
        {
            _sound.timeSamples -= Mathf.RoundToInt(loopDuration * audioClip.frequency);
        }
    }
}
