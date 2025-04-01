using UnityEngine;

// ���������� ���������� ������: ����������� ������, ��������� � �����
public class TurretRF : MonoBehaviour
{
    [Header("���������")]
    public float attackRange = 10f; // ������������ ��������� �����

    [Header("������ �� �������")]
    public Transform raycastPoint; // ����� ������� ���� �����������
    public Transform rotationHinge; // ������ ��� �������� ������

    private Transform _player;     // ������ �� ������
    private bool _playerDetected;  // ���� �����������

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

        // ��������� � �������� ���������
        raycastPoint.LookAt(_player);
        CheckPlayerVisibility();

        if (IsPlayerInRange() && _playerDetected)
        {
            AimAtPlayer();  // ������� ������
            AttackPlayer(); // ������ �����
        }
    }

    // ���������� ������� � ������ (����� �������������� ��� ���������)
    protected virtual void AimAtPlayer()
    {
        rotationHinge.LookAt(_player);
    }

    // ������� ������ ����� (����� ���������)
    protected virtual void AttackPlayer()
    {
        Debug.Log("Firing at player!");
    }

    // �������� ��������� ������ ����� raycast
    private void CheckPlayerVisibility()
    {
        Ray ray = new Ray(raycastPoint.position, raycastPoint.forward);
        _playerDetected = Physics.Raycast(ray, out RaycastHit hit, attackRange)
                        && hit.transform == _player;
    }

    // �������� ��������� �� ������
    private bool IsPlayerInRange()
    {
        return Vector3.Distance(transform.position, _player.position) <= attackRange;
    }

    // ������������ ���� � ���������
    private void OnDrawGizmos()
    {
        if (raycastPoint == null) return;
        Gizmos.color = _playerDetected ? Color.red : Color.cyan;
        Gizmos.DrawRay(raycastPoint.position, raycastPoint.forward * attackRange);
    }
}