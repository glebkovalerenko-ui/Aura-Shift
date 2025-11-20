// PlayerController.cs
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Скорость движения")]
    [SerializeField] private float forwardSpeed = 10f;

    [Header("Объекты форм")]
    [SerializeField] private GameObject circleModel;
    [SerializeField] private GameObject squareModel;
    [SerializeField] private GameObject triangleModel;

    private AuraShape currentShape;

    // Start вызывается перед первым кадром
    void Start()
    {
        // Начинаем игру с формой круга
        ChangeShape(AuraShape.Circle);
    }

    // Update вызывается каждый кадр
    void Update()
    {
        // 1. Постоянное движение вперед
        // Vector3.forward - это короткая запись для (0, 0, 1)
        // Time.deltaTime делает движение плавным и не зависящим от мощности компьютера
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);

        // 2. Проверка нажатия (тап в любом месте)
        // GetMouseButtonDown(0) отлавливает клик левой кнопки мыши или тап по экрану
        if (Input.GetMouseButtonDown(0))
        {
            // Вычисляем, какая форма должна быть следующей
            AuraShape nextShape = currentShape switch
            {
                AuraShape.Circle => AuraShape.Square,
                AuraShape.Square => AuraShape.Triangle,
                AuraShape.Triangle => AuraShape.Circle,
                _ => AuraShape.Circle
            };

            // Вызываем метод для смены формы
            ChangeShape(nextShape);
        }
    }

    // Метод для смены визуальной формы
    private void ChangeShape(AuraShape newShape)
    {
        currentShape = newShape;

        // Выключаем все модели, чтобы быть уверенными, что активна только одна
        circleModel.SetActive(false);
        squareModel.SetActive(false);
        triangleModel.SetActive(false);

        // Включаем нужную модель в зависимости от newShape
        switch (newShape)
        {
            case AuraShape.Circle:
                circleModel.SetActive(true);
                break;
            case AuraShape.Square:
                squareModel.SetActive(true);
                break;
            case AuraShape.Triangle:
                triangleModel.SetActive(true);
                break;
        }
    }
}