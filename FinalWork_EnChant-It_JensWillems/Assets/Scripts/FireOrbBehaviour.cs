using System.Collections;
using UnityEngine;

public class FireOrbBehaviour : MonoBehaviour
{
    private Camera _mainCamera;

    [Header("Settings")]
    public float Speed = 5;
    public int DamageAmount = 10;
    public AudioSource AudioSourceImpact;
    public GameObject ParentGameObject;
    public AudioSource AudioSourceShoot;
    public ParticleSystem ParticleSystem;

    public float Damage = 25f;

    public void Awake()
    {
        ParticleSystem = GetComponent<ParticleSystem>();
    }

    private void Start()
    {
        _mainCamera = Camera.main;

        if (_mainCamera == null)
        {
            return;
        }

        Vector3 forwardDirection = _mainCamera.transform.forward;
        transform.rotation = Quaternion.LookRotation(forwardDirection);

        StartCoroutine(StopParticlesAndDestroyAfterDelay());
    }

    void Update()
    {
        GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * Speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyManager enemyManager = collision.transform.GetComponent<EnemyManager>();
            if (enemyManager != null)
            {
                enemyManager.Hit(Damage);
            }

            Debug.Log("hit!");
            AudioSourceImpact.Play();
            AudioSourceShoot.Stop();
            ParticleSystem.Stop();

            if (ParentGameObject != null)
            {
                StartCoroutine(StopParticlesAfterDelay());
            }
            else
            {
                Debug.LogError("Parent GameObject is not assigned.");
            }

            StartCoroutine(DealDamageAfterParticlesStop(collision.collider));
        }
    }

    private IEnumerator StopParticlesAfterDelay()
    {
        for (int i = 0; i < 10; i++)
        {
            yield return null;
        }

        ParticleSystem[] particleSystems = ParentGameObject.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem ps in particleSystems)
        {
            ps.Stop();
        }
    }

    private IEnumerator DealDamageAfterParticlesStop(Collider enemy)
    {
        yield return null;

        EnemyInteraction enemyHealth = enemy.GetComponent<EnemyInteraction>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(DamageAmount);
        }
    }

    private IEnumerator StopParticlesAndDestroyAfterDelay()
    {
        yield return new WaitForSeconds(15f);

        ParticleSystem[] particleSystems = ParentGameObject.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem ps in particleSystems)
        {
            ps.Stop();
        }
        Destroy(gameObject);
    }
}
