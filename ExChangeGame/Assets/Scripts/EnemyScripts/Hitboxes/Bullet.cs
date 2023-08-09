using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float pushForceAway = 1f;
    [SerializeField] private float pushForceUp = 1f;
    [SerializeField] private int damage = 2;
    
    private AudioSource hitSound;
    private Renderer _renderer;
    private Rigidbody _rigidbody;
    private Collider _collider;
    
    private void Start()
    {
        hitSound = GetComponent<AudioSource>();
        _renderer = GetComponent<Renderer>();
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }
    
    /// <summary>
    /// simple Hitbox Script, which reduces the health of the other object then applies knockback
    /// </summary>
    /// <param name="other"></param>
    private void OnCollisionEnter(Collision other)
    {
        GameObject hit = other.gameObject;
        Debug.Log(hit.tag, other.gameObject);
        if (hit.TryGetComponent<BasicHealth>(out var health))
        {
            health.Damage(damage);
            Debug.Log(health);
            var direction = other.transform.position - transform.position;
            direction.Normalize();
            hit.GetComponent<Rigidbody>().AddForce(direction * pushForceAway + Vector3.up * pushForceUp,
                ForceMode.VelocityChange);
            hit.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        }
        StartCoroutine(DestroyBullet());
    }
    
    private IEnumerator DestroyBullet()
    {
        hitSound.Play();
        _collider.enabled = false;
        _renderer.enabled = false;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.useGravity = false;
        _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}