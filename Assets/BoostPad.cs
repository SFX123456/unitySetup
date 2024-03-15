using UnityEngine;
using UnityEngine.Serialization;

public class BoostPad : MonoBehaviour
{
    [SerializeField] private float continuousVelocityMultiplier = 1000;

    private void OnTriggerStay(Collider collision)
    {
        if (!collision.CompareTag("Player")) return;
        collision.GetComponentInParent<PrometeoCarController>().ApplyForce(continuousVelocityMultiplier);
    }
}