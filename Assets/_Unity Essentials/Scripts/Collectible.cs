using UnityEngine;

public class Collectible : MonoBehaviour
{
    [Header("收集品设置")]
    public float rotationSpeed;
    public bool isRequiredForCompletion = true;
    [Header("收集特效")]
    public GameObject onCollectEffect;
    public AudioClip onCollectSound;
    
    
    void Update()
    {
        transform.Rotate(0, rotationSpeed, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Collect();
        }
    }

    private void Collect()
    {
        // 重要：先通知管理器，再销毁对象
        if (isRequiredForCompletion && CollectionManager.Instance != null)
        {
            CollectionManager.Instance.CollectItem();
        }

        // 播放特效
        if (onCollectEffect != null)
        {
            // 跟随旋转
            // Instantiate(onCollectEffect, transform.position, transform.rotation);
            // 固定旋转
            Instantiate(onCollectEffect, transform.position, Quaternion.identity);
        }
        // 播放音效
        if(onCollectSound != null)
        {
            AudioSource.PlayClipAtPoint(onCollectSound, transform.position);
        }

        // 销毁自己
        Destroy(gameObject);
    }
}