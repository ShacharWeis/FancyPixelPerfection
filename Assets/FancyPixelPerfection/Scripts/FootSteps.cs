using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class FootSteps : MonoBehaviour
{
    [SerializeField] private MaterialFade LeftFoot;
    [SerializeField] private MaterialFade RightFoot;
    [SerializeField] private Color color;
    [SerializeField] private AudioClip SFXSound;
    [SerializeField] private String tag = "Foot";
    [SerializeField] private float timeFadeOut = 3f;
    public UnityEvent EventStepedOn; 
    private AudioSource _audioSource;
    private int Footcount = 0;
    private bool beenTriggered = false;
    void Start()
    {
        LeftFoot.Renderer.material.color = color;
        RightFoot.Renderer.material.color = color;
        _audioSource = GetComponent<AudioSource>(); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if(beenTriggered)
            return;
        
        if (other.gameObject.CompareTag(tag))
        {
            Footcount++;
            if (Footcount >= 2)
            {
                beenTriggered = true;
         //       _audioSource.PlayOneShot(SFXSound);
                LeftFoot.StartFade(timeFadeOut, -1f);
                RightFoot.StartFade(timeFadeOut, -1f);
                EventStepedOn.Invoke();
                Destroy(this.gameObject,timeFadeOut );
            }
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        
        if(beenTriggered)
            return;
        
        
        if (other.gameObject.CompareTag(tag))
        {
            Footcount--;
            
        }
    }
}
