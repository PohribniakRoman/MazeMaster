using UnityEngine;

public class TrapScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HealthBar health = other.GetComponentInChildren<HealthBar>();
            if (health != null)
            {
                health.TakeDamage(50);
            }
            else
            {
                Debug.LogWarning("No HealthBar found on Player!");
            }

            Destroy(gameObject);
        }

    }
}
