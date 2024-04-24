using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class MaterialFade :  Fade
{
    private Renderer renderer;

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
    }

    #region public
    public void ResetAlpha(float alpha)
    {
        StopFade();
        Color color = renderer.material.color;
        color.a = alpha;
        renderer.material.color = color;
    }

    public void StartFade(float time, float alpha)
    {
        float currentAlpha = renderer.material.color.a;
        float deltaAlpha = alpha - currentAlpha;
        fade = StartCoroutine(FadeObject(time, deltaAlpha));
        fade = StartCoroutine(FadeObject(time, alpha));
    }
    #endregion

    protected override void ObjectToFade(float deltaAlpha)
    {
        Color color = renderer.material.color;
        float currentAlpha = color.a;
        float newAlpha = Mathf.Clamp01(currentAlpha + deltaAlpha * Time.deltaTime);
        color.a = newAlpha;
        renderer.material.color = color;
    }   
}
