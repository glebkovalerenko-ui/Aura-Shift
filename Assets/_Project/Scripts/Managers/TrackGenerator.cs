// TrackGenerator.cs
using System.Collections.Generic;
using UnityEngine;

public class TrackGenerator : MonoBehaviour
{
    [Header("Элементы трека")]
    [SerializeField] private GameObject trackSegmentPrefab; // Наш "чертеж" сегмента дороги
    [SerializeField] private int initialSegments = 5; // Сколько сегментов создаем на старте

    [Header("Ссылка на игрока")]
    [SerializeField] private Transform playerTransform;

    private float segmentLength = 100f; // Длина одного сегмента (должна совпадать с Z в Scale у префаба!)
    private float spawnZ = 0f; // Координата Z, где нужно создать следующий сегмент
    private List<GameObject> activeSegments = new List<GameObject>();

    void Start()
    {
        // Создаем стартовые сегменты, чтобы игрок не появился в пустоте
        for (int i = 0; i < initialSegments; i++)
        {
            SpawnSegment();
        }
    }

    void Update()
    {
        // Проверяем, если игрок проехал достаточно далеко,
        // то создаем новый сегмент и удаляем старый
        if (playerTransform.position.z > (spawnZ - initialSegments * segmentLength) + segmentLength)
        {
            SpawnSegment();
            RecycleSegment();
        }
    }

    private void SpawnSegment()
    {
        // Создаем новый сегмент из "чертежа" (префаба) в нужной позиции
        GameObject segment = Instantiate(trackSegmentPrefab, Vector3.forward * spawnZ, Quaternion.identity);
        spawnZ += segmentLength;
        activeSegments.Add(segment);
    }

    // Удаляем самый старый сегмент, который остался позади
    private void RecycleSegment()
    {
        Destroy(activeSegments[0]);
        activeSegments.RemoveAt(0);
    }
}