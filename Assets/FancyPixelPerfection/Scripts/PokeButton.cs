using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class PokeButton : MonoBehaviour
{
    [SerializeField] private UnityEvent OnClick;
    [SerializeField] private Transform PushableButton;
    private float cooldown = 0;
    private Color startColor;
    private void Start()
    {
        startColor = PushableButton.GetComponent<Renderer>().material.color;
    }

    public void Cooldown()
    {
        cooldown = 1;
    }
    private void Update()
    {
        if (cooldown > 0)
            cooldown -= Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (cooldown > 0) return;
        
        Debug.Log("PokeButton got OnTriggerEnter!");

        if (other.tag.Equals("FingerTip")) 
        {
            Debug.Log("Touch came from Player");
            cooldown = 1;
            OnClick.Invoke();
            AnimateAClick();
        }
    }

    [Button]
    public void AnimateAClick()
    {
        var st = DOTween.Sequence();
        st.Append(PushableButton.DOLocalMoveY(-3, 0.2f));
        st.Append(PushableButton.DOLocalMoveY(0.149f, 0.2f));
        
        var sc = DOTween.Sequence();
        sc.Append(PushableButton.GetComponent<Renderer>().material.DOColor(Color.green, 0.2f));
        sc.Append(PushableButton.GetComponent<Renderer>().material.DOColor(startColor, 0.2f));
    }
    

}
