using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Fade : MonoBehaviour
{

    protected Coroutine fade = null;

      
    #region public

    public virtual bool IsFading()
    {
        if (fade == null)
            return false;

        return true;
    }


    public virtual void StopFade()
    {
        if (fade == null)
            return;
        StopCoroutine(fade);
        fade = null;
    }

    #endregion

 

    protected virtual void ObjectToFade(float amount)
    {

    }

    protected virtual IEnumerator FadeObject(float time, float target)
    {
        float timeFinish = Time.time + time;
        float amount = target / time;
        while (timeFinish > Time.time)
        {
            ObjectToFade(amount);
            yield return null;
        }

        fade = null;
    }
}
