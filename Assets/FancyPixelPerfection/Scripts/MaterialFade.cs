using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class MaterialFade :  Fade
{
    private Renderer _renderer;
    public Renderer Renderer
    {
        get { return _renderer; }
        private set { _renderer = value; }
    }

    private void Awake()
    {
        Renderer = GetComponent<Renderer>();
    }

    #region public
    public void ResetAlpha(float alpha)
    {
        StopFade();
        Color color = _renderer.material.color;
        color.a = alpha;
        _renderer.material.color = color;
    }

    public void StartFade(float time, float alpha)
    {
        float currentAlpha = _renderer.material.color.a;
        float deltaAlpha = alpha - currentAlpha;
        fade = StartCoroutine(FadeObject(time, deltaAlpha));

    }
    #endregion

    protected override void ObjectToFade(float deltaAlpha)
    {
        Color color = _renderer.material.color;
        float currentAlpha = color.a;
        float newAlpha = Mathf.Clamp01(currentAlpha + deltaAlpha * Time.deltaTime);
        color.a = newAlpha;
        _renderer.material.color = color;
    }
}
