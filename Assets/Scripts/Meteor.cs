using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Meteor : MonoBehaviour
{
    public AudioClip explosionAudio;
    public GameObject particlePrefab;
    private SpriteRenderer m_SpriteRenderer;
    private Rigidbody2D m_RigidBody;
    private readonly Color[] m_ParticleColors = {new Color(255, 165, 0), Color.red, Color.yellow};

    private void Awake()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_RigidBody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        var velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-8f, -6f));
        m_RigidBody.velocity = velocity;
        m_RigidBody.angularVelocity = (velocity.x < 0 ? velocity.magnitude : velocity.magnitude * -1) * 175;

        var scale = Random.Range(0.5f, 1.5f);
        transform.localScale = new Vector3(scale, scale, scale);
    }
    
    private void Update()
    {
        if (!m_SpriteRenderer.isVisible)
            Destroy(gameObject);
        var loc = transform.position;
        loc.x += Random.Range(-0.1f, 0.1f);
        var particle = Instantiate(particlePrefab, loc, Quaternion.identity);
        particle.GetComponent<Particle>().Init(m_ParticleColors, m_RigidBody.velocity.normalized * -1, 0.2f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(explosionAudio, Camera.main.transform.position, 0.3f);
            var loc = transform.position;
            loc.y -= GetComponent<CircleCollider2D>().radius;
            for (var i = 0; i < 30; i++)
            {
                var particle = Instantiate(particlePrefab, loc, Quaternion.identity);
                particle.GetComponent<Particle>().Init(m_ParticleColors, new Vector2(Random.Range(-2f, 2f), Random.Range(0f, 2f)), 0.5f);
            }
        }
        else if (other.CompareTag("Player"))
        {
            var dinoPlayer = other.GetComponent<DinoPlayer>();
            if (!dinoPlayer.IsDead)
            {
                Destroy(gameObject);
                AudioSource.PlayClipAtPoint(explosionAudio, Camera.main.transform.position, 0.3f);
                dinoPlayer.Die();
            }
        }
    }
}
