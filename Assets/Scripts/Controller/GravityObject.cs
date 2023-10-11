using UnityEngine;

public class GravityObject : MonoBehaviour
{
    public float repulsionForce = 10.0f; // 밀어내는 힘의 강도
    public float maxRepulsionDistance = 5.0f; // 최대 밀기 거리
    private string safetyZoneTag = "Safety Zone"; // 안전 구역 태그

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
                playerRigidbody.AddForce(-force); // 힘의 방향을 반전
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(safetyZoneTag))
        {
            isInSafetyZone = true;
            // 안전 구역에 들어갈 때 플레이어를 밀어내는 힘을 비활성화
            repulsionForce = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(safetyZoneTag))
        {
            isInSafetyZone = false;
            // 안전 구역에서 나올 때 플레이어를 밀어내는 힘을 다시 활성화
            repulsionForce = 10.0f;
        }
    }
}
