using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class MaterialFade :  Fade
{
    private Renderer _matRender;
    public Renderer MatRender
    {
        get { return _matRender; }
        private set { _matRender = value; }
    }

    private void Awake()
    {
        MatRender = GetComponent<Renderer>();
    }

    #region public
    public void ResetAlpha(float alpha)
    {
        StopFade();
        Color color = _matRender.material.color;
        color.a = alpha;
        _matRender.material.color = color;
    }

    public void StartFade(float time, float alpha)
    {
        float currentAlpha = _matRender.material.color.a;
        float deltaAlpha = alpha - currentAlpha;
        fade = StartCoroutine(FadeObject(time, deltaAlpha));

    }
    #endregion

    protected override void ObjectToFade(float deltaAlpha)
    {
        Color color = _matRender.material.color;
        float currentAlpha = color.a;
        float newAlpha = Mathf.Clamp01(currentAlpha + deltaAlpha * Time.deltaTime);
        color.a = newAlpha;
        _matRender.material.color = color;
    }
}
