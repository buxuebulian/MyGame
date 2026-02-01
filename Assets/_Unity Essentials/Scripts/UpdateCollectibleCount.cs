using UnityEngine;
using TMPro;
using System; // Required for Type handling


public class UpdateCollectibleCount : MonoBehaviour
{
    private TextMeshProUGUI textComponent; // Reference to the TextMeshProUGUI component
    void Awake()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
    }
    void OnEnable()
    {
        //安全检查（可选）
        if (CollectibleManager.Instance == null)
        {
            Debug.LogError("CollectibleManager is missing");
            enabled = false;
            return;
        }

        //初始显示（每次启用都刷新）
        UpdateDisplay(CollectibleManager.Instance.GetCurrentCount());

        //订阅事件
        CollectibleManager.Instance.OnCollectibleCountChanged += UpdateDisplay;
    }

    void OnDisable()
    {
        //取消订阅（防止内存泄漏和无效回调）
        if (CollectibleManager.Instance != null)
        {
            CollectibleManager.Instance.OnCollectibleCountChanged -= UpdateDisplay;
        }
    }
    void OnDestroy()
    {
        //取消订阅（防止内存泄漏和无效回调）
        if (CollectibleManager.Instance != null)
        {
            CollectibleManager.Instance.OnCollectibleCountChanged -= UpdateDisplay;
        }
    }
    public void UpdateDisplay(int count)
    {
        
        textComponent.text = $"Collectibles remaining: {count}";
    }
}
