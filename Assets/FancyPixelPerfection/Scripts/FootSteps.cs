using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

[RequireComponent(typeof(AudioSource))]
public class FootSteps : MonoBehaviour
{
     [SerializeField] private MaterialFade Foot;
    
    [SerializeField] private Color color;
    [SerializeField] private Color colorfadeOut = Color.yellow;
    [SerializeField] private AudioClip SFXSound;
    [SerializeField] private String ObjectTag = "Foot";
    [SerializeField] private float timeFadeOut = 3f;
    public UnityEvent EventStepedOn;
    private AudioSource _audioSource;
    public int Footcount = 0;
    private bool beenTriggered = false;
    void Start()
    {
        Foot.MatRender.material.color = color;
        _audioSource = GetComponent<AudioSource>(); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if(beenTriggered)
            return;
        
        if (other.gameObject.CompareTag(ObjectTag))
        {
            Footcount++;
            if (Footcount >= 2)
            {
                beenTriggered = true;
                _audioSource.PlayOneShot(SFXSound);
                Foot.MatRender.material.color = colorfadeOut;
                Foot.StartFade(timeFadeOut, -1f);
                EventStepedOn.Invoke();
                Destroy(this.gameObject,timeFadeOut );
            }
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        
        if(beenTriggered)
            return;
        
        
        if (other.gameObject.CompareTag(ObjectTag))
        {
            Footcount--;

        }
    }
}
