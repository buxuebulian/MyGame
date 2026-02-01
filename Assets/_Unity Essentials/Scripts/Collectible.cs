using UnityEngine;

public class Collectible : MonoBehaviour
{
    [Header("收集品设置")]
    public float rotationSpeed;
    public bool isRequiredForCompletion = true;
    [Header("收集特效")]
    public GameObject onCollectEffect;
    public AudioClip onCollectSound;
    
    void Awake()
    {
        //注册到管理器
        CollectibleManager.Instance.RegisterCollectible();
    }
    
    void Update()
    {
        transform.Rotate(0, rotationSpeed, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Collected();
        }
    }

    void OnDestroy()
    {
        //注销，例如被玩家拾取后Destroy
        CollectibleManager.Instance?.UnregisterCollectible();
    }
    private void Collected()
    {
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