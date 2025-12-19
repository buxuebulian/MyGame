using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public float dayLength = 60f; // 一天多少秒

    void Update()
    {
        float rotationSpeed = 360f / dayLength * Time.deltaTime;
        transform.Rotate(rotationSpeed, 0, 0, Space.World);
    }
}