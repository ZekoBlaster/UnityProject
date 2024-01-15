using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public float speed = 5f;
    public float avoidanceRadius = 2f; // Radius to check for other zombies
    private Transform playerTransform;

    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    void Update()
    {
        if (playerTransform != null)
        {
            Vector3 direction = playerTransform.position - transform.position;
            direction.y = 0;
            direction.Normalize();

            // Avoidance logic
            Vector3 avoidanceDir = Vector3.zero;
            Collider[] hits = Physics.OverlapSphere(transform.position, avoidanceRadius, LayerMask.GetMask("Zombie"));
            foreach (var hit in hits)
            {
                if (hit.gameObject != gameObject) // Avoid self
                {
                    avoidanceDir += transform.position - hit.transform.position;
                }
            }
            avoidanceDir.Normalize();

            // Combine chase and avoidance directions
            Vector3 combinedDir = direction + avoidanceDir;
            combinedDir.Normalize();

            // Move the enemy
            transform.position += combinedDir * speed * Time.deltaTime;

            // Face the player
            if (combinedDir != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(combinedDir, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, speed * Time.deltaTime);
            }
        }
    }

    void OnDrawGizmos()
    {
        // Visualize the avoidance radius in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, avoidanceRadius);
    }
}
