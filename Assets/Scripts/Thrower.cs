using UnityEngine;

public class Thrower : MonoBehaviour
{
    public GameObject prefabToThrow;
    public float throwForce = 10f;
    public float offsetDistance = 1f; // Offset distance in front of the player
    AudioSource audioSource;

    private void Start()
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        audioSource = audioSources[2];
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) // Instantiate prefab only once per button press
        {
            audioSource.Play();
            // Instantiate the prefab with an offset to avoid direct interaction with the player
            Vector3 spawnPosition = transform.position + transform.forward * offsetDistance;
            GameObject thrownPrefab = Instantiate(prefabToThrow, spawnPosition, transform.rotation);

            Rigidbody rb = thrownPrefab.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(transform.forward * throwForce, ForceMode.Impulse);
            }
            else
            {
                Debug.LogError("Thrown prefab does not have a Rigidbody component.");
            }

            // Optional: Ignore collision between the player and the thrown prefab
            Collider playerCollider = GetComponent<Collider>();
            Collider prefabCollider = thrownPrefab.GetComponent<Collider>();
            if (playerCollider != null && prefabCollider != null)
            {
                Physics.IgnoreCollision(prefabCollider, playerCollider);
            }
        }
    }
}
