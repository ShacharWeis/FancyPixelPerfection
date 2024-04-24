using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootPrint : MonoBehaviour
{

    
    [SerializeField] private float fadeStartTime = 1.0f;
    [SerializeField] private float timeToLive = 5f;
    [SerializeField] private float fadeOutTime = 2f;
    private MaterialFade Fade;
    // Start is called before the first frame update
    void Start()
    {
        Fade = GetComponent<MaterialFade>();
        Fade.ResetAlpha(0);
        Fade.StartFade(fadeStartTime, 1f);
        Invoke("CleanUp", timeToLive);
    }

    void CleanUp()
    {
        Fade.StopFade();
        Fade.StartFade(fadeOutTime, -1f);
        Destroy(this.gameObject, fadeOutTime);
    }
     
}