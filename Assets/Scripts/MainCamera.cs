using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public Transform target; // Посилання на об'єкт, за яким камера має слідкувати
    public float smoothSpeed = 5f; // Плавність руху камери
    private Vector3 offset; // Відстань між камерою і гравцем

    void Start()
    {
        // Розраховуємо відстань між камерою і гравцем (початковий зсув)
        offset = transform.position - target.position;
    }

    void LateUpdate()
    {
        // Розраховуємо цільове положення камери, з урахуванням плавності
        Vector3 targetPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);

        // Встановлюємо нове положення камери
        transform.position = smoothedPosition;
    }
}
