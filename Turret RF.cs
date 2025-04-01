using UnityEngine;

// Управление поведением турели: обнаружение игрока, наведение и атака
public class TurretRF : MonoBehaviour
{
    [Header("Настройки")]
    public float attackRange = 10f; // Максимальная дистанция атаки

    [Header("Ссылки на объекты")]
    public Transform raycastPoint; // Точка выпуска луча обнаружения
    public Transform rotationHinge; // Шарнир для поворота оружия

    private Transform _player;     // Ссылка на игрока
    private bool _playerDetected;  // Флаг обнаружения

    private void Start()
    {
        _player = GameObject.FindWithTag("Player").transform;
        if (_player == null)
        {
            Debug.LogError("Player not found! Make sure player has 'Player' tag.");
        }
    }

    private void Update()
    {
        if (_player == null) return;

        // Наведение и проверка видимости
        raycastPoint.LookAt(_player);
        CheckPlayerVisibility();

        if (IsPlayerInRange() && _playerDetected)
        {
            AimAtPlayer();  // Поворот оружия
            AttackPlayer(); // Логика атаки
        }
    }

    // Мгновенный поворот к игроку (можно переопределить для плавности)
    protected virtual void AimAtPlayer()
    {
        rotationHinge.LookAt(_player);
    }

    // Базовая логика атаки (вывод сообщения)
    protected virtual void AttackPlayer()
    {
        Debug.Log("Firing at player!");
    }

    // Проверка видимости игрока через raycast
    private void CheckPlayerVisibility()
    {
        Ray ray = new Ray(raycastPoint.position, raycastPoint.forward);
        _playerDetected = Physics.Raycast(ray, out RaycastHit hit, attackRange)
                        && hit.transform == _player;
    }

    // Проверка дистанции до игрока
    private bool IsPlayerInRange()
    {
        return Vector3.Distance(transform.position, _player.position) <= attackRange;
    }

    // Визуализация луча в редакторе
    private void OnDrawGizmos()
    {
        if (raycastPoint == null) return;
        Gizmos.color = _playerDetected ? Color.red : Color.cyan;
        Gizmos.DrawRay(raycastPoint.position, raycastPoint.forward * attackRange);
    }
}