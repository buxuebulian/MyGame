using UnityEngine;

public class BallBounceSound : MonoBehaviour
{
    public AudioClip bounceSound;    // 在Inspector中分配弹跳音效
    private AudioSource audioSource;
    private Rigidbody rb;

    void Start()
    {
        // 获取或添加AudioSource组件
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        // 获取Rigidbody组件
        rb = GetComponent<Rigidbody>();

        // 配置 AudioSource 组件
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1.0f; // 3D音效

    }

    void OnCollisionEnter(Collision collision)
    {
        // 检查碰撞是否足够强烈
        if (collision.relativeVelocity.magnitude > 1.0f)
        {
            PlayBounceSound(collision.relativeVelocity.magnitude);
        }
    }

    void PlayBounceSound(float impactForce)
    {
        if (bounceSound != null && audioSource != null)
        {
            // 根据撞击力度调整音量
            float volume = Mathf.Clamp(impactForce / 10.0f, 0.1f, 1.0f);
            audioSource.volume = volume;

            // 轻微随机化音调增加真实感
            audioSource.pitch = Random.Range(0.9f, 1.1f);

            audioSource.PlayOneShot(bounceSound);
        }
    }
}
