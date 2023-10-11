using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawner : MonoBehaviour
{
    public GameObject bombPrefab;
    public float startXPosition = -5f; // 시작 x 좌표
    public float endXPosition = 25f;   // 끝 x 좌표
    public float spacing = 1f;         // 폭탄과 폭탄 사이의 거리
    public float activationInterval = 1f; // 폭탄 활성화 및 비활성화 간격

    private List<GameObject> bombs = new List<GameObject>();
    private float currentX;

    private void Start()
    {
        currentX = startXPosition;

        // 모든 폭탄을 미리 생성합니다.
        while (currentX <= endXPosition)
        {
            Vector3 spawnPosition = new Vector3(currentX, -3f, transform.position.z);
            GameObject bomb = Instantiate(bombPrefab, spawnPosition, Quaternion.identity);
            bombs.Add(bomb);

            spawnPosition = new Vector3(currentX, 0f, transform.position.z);
            bomb = Instantiate(bombPrefab, spawnPosition, Quaternion.identity);
            bombs.Add(bomb);

            spawnPosition = new Vector3(currentX, 3f, transform.position.z);
            bomb = Instantiate(bombPrefab, spawnPosition, Quaternion.identity);
            bombs.Add(bomb);

            currentX += spacing;
        }

        // 폭탄을 주기적으로 활성화 및 비활성화합니다.
        StartCoroutine(ActivateBombs());
    }

    private IEnumerator ActivateBombs()
    {
        while (true)
        {
            // 모든 폭탄을 활성화
            foreach (var bomb in bombs)
            {
                bomb.SetActive(true);
            }

            // 지정된 시간(activationInterval)만큼 대기
            yield return new WaitForSeconds(activationInterval);

            // 모든 폭탄을 비활성화
            foreach (var bomb in bombs)
            {
                bomb.SetActive(false);
            }

            // 다시 지정된 시간(activationInterval)만큼 대기
            yield return new WaitForSeconds(activationInterval);
        }
    }

}
