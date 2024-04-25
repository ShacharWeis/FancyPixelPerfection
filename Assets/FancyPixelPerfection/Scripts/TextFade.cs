using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextFade : Fade
{
    private TextMeshPro _text;


    private void Awake()
    {
        _text = this.GetComponent<TextMeshPro>();
    }



    public void ResetAlpha(float alpha)
    {
        Color color = _text.color;
        color.a = alpha;
        _text.color = color;
    }

    public void StartFade(float time, float alpha)
    {
        float currentAlpha = _text.color.a;
        float deltaAlpha = alpha - currentAlpha;
        fade = StartCoroutine(FadeObject(time, deltaAlpha));
    }


    protected override void ObjectToFade(float deltaAlpha)
    {
        
        Color color = _text.color;
        float currentAlpha = color.a;
        float newAlpha = Mathf.Clamp01(currentAlpha + deltaAlpha * Time.deltaTime);
        color.a = newAlpha;
        _text.color = color;

    }
}
