using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawner : MonoBehaviour
{
    public GameObject bombPrefab;
    public float startXPosition = -5f; // ���� x ��ǥ
    public float endXPosition = 25f;   // �� x ��ǥ
    public float spacing = 1f;         // ��ź�� ��ź ������ �Ÿ�
    public float activationInterval = 1f; // ��ź Ȱ��ȭ �� ��Ȱ��ȭ ����

    private List<GameObject> bombs = new List<GameObject>();
    private float currentX;

    private void Start()
    {
        currentX = startXPosition;

        // ��� ��ź�� �̸� �����մϴ�.
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

        // ��ź�� �ֱ������� Ȱ��ȭ �� ��Ȱ��ȭ�մϴ�.
        StartCoroutine(ActivateBombs());
    }

    private IEnumerator ActivateBombs()
    {
        while (true)
        {
            // ��� ��ź�� Ȱ��ȭ
            foreach (var bomb in bombs)
            {
                bomb.SetActive(true);
            }

            // ������ �ð�(activationInterval)��ŭ ���
            yield return new WaitForSeconds(activationInterval);

            // ��� ��ź�� ��Ȱ��ȭ
            foreach (var bomb in bombs)
            {
                bomb.SetActive(false);
            }

            // �ٽ� ������ �ð�(activationInterval)��ŭ ���
            yield return new WaitForSeconds(activationInterval);
        }
    }

}
