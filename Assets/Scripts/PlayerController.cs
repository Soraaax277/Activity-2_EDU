using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    public GameObject rocketPrefab;
    public float fireInterval = 2f;

    public int rocketCount = 4;
    public int maxRockets = 8;

    public float powerUpPickupRange = 0.5f;

    float fireTimer;

    void Update()
    {
        Move();
        HandleFiring();
        CheckPowerUps();
    }

    void Move()
    {
        float x = 0;
        float y = 0;

        if (Input.GetKey(KeyCode.W)) y = 1;
        if (Input.GetKey(KeyCode.S)) y = -1;
        if (Input.GetKey(KeyCode.A)) x = -1;
        if (Input.GetKey(KeyCode.D)) x = 1;

        Vector3 dir = new Vector3(x, y, 0).normalized;
        transform.position += dir * moveSpeed * Time.deltaTime;
    }

    void HandleFiring()
    {
        fireTimer += Time.deltaTime;

        if (fireTimer >= fireInterval)
        {
            fireTimer = 0f;
            FireRockets();
        }
    }

    void FireRockets()
    {
        float angleStep = 360f / rocketCount;
        float startAngle = 45f;

        for (int i = 0; i < rocketCount; i++)
        {
            float angle = startAngle + angleStep * i;
            Quaternion rot = Quaternion.Euler(0, 0, angle);

            Instantiate(
                rocketPrefab,
                transform.position,
                rot
            );
        }
    }

    void CheckPowerUps()
    {
        PowerUp[] powerUps = FindObjectsOfType<PowerUp>();

        foreach (PowerUp p in powerUps)
        {
            float dist = Vector3.Distance(transform.position, p.transform.position);

            if (dist <= powerUpPickupRange)
            {
                IncreaseRockets();
                Destroy(p.gameObject);
            }
        }
    }

    void IncreaseRockets()
    {
        if (rocketCount < maxRockets)
            rocketCount++;
    }
}