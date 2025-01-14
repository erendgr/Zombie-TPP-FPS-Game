using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class ReloadUIController : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public float blinkDuration = 2f; // Yanıp sönme süresi

    void Start()
    {
        // Rengi tamamen görünür ve şeffaf arasında animasyon yap
        textComponent.DOFade(0, blinkDuration).SetLoops(-1, LoopType.Yoyo);
    }
}