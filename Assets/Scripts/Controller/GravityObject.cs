using UnityEngine;

public class GravityObject : MonoBehaviour
{
    public float repulsionForce = 10.0f; // �о�� ���� ����
    public float maxRepulsionDistance = 5.0f; // �ִ� �б� �Ÿ�
    private string safetyZoneTag = "Safety Zone"; // ���� ���� �±�

    private Transform player;
    private Rigidbody2D playerRigidbody;
    private bool isInSafetyZone = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerRigidbody = player.GetComponent < Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (player != null && playerRigidbody != null)
        {
            Vector2 directionToPlayer = player.position - transform.position;
            float distance = directionToPlayer.magnitude;

            if (distance < maxRepulsionDistance && !isInSafetyZone)
            {
                float repulsionStrength = (maxRepulsionDistance - distance) * repulsionForce;
                Vector2 force = directionToPlayer.normalized * repulsionStrength;
                playerRigidbody.AddForce(-force); // ���� ������ ����
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(safetyZoneTag))
        {
            isInSafetyZone = true;
            // ���� ������ �� �� �÷��̾ �о�� ���� ��Ȱ��ȭ
            repulsionForce = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(safetyZoneTag))
        {
            isInSafetyZone = false;
            // ���� �������� ���� �� �÷��̾ �о�� ���� �ٽ� Ȱ��ȭ
            repulsionForce = 10.0f;
        }
    }
}
