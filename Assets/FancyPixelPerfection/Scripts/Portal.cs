using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Portal : MonoBehaviour
{
    
    public void CloseAndDestroy()
    {
        transform.DOScale(0, 0.3f).OnComplete(() => Destroy(gameObject));
    }
    
}
