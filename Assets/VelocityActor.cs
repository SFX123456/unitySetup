using UnityEngine;

public class VelocityActor : MonoBehaviour
{
    [SerializeField] public float minVelocity = 5f;
    [SerializeField] public float minMass = 10f;
    [SerializeField] public Rigidbody rb;

    private void OnCollisionEnter(Collision collision)
    {
        if (!(collision.relativeVelocity.magnitude >= minVelocity) || !(collision.rigidbody.mass >= minMass)) return;

        rb.isKinematic = false;
        rb.useGravity = true;
        rb.AddExplosionForce(15f, collision.contacts[0].point, 10f, 3.0f, ForceMode.Impulse);
    }

    public void GotHit()
    {
        rb.isKinematic = false;
        rb.useGravity = true;
        rb.AddExplosionForce(15f, transform.position, 10f, 3.0f, ForceMode.Impulse);
    }
}