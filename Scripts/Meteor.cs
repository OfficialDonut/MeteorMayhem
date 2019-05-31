using UnityEngine;
using Random = UnityEngine.Random;

public class Meteor : MonoBehaviour
{
    public AudioClip explosionAudio;
    public GameObject particlePrefab;
    private SpriteRenderer m_Renderer;
    private readonly Color[] m_ExplosionColors = {new Color(255, 165, 0), Color.red, Color.yellow}; 

    private void Start()
    {
        m_Renderer = GetComponent<SpriteRenderer>();
        var velX = Random.Range(0, 3);
        if (transform.position.x > 0)
            velX *= -1;
        GetComponent<Rigidbody2D>().velocity = new Vector2(velX, Random.Range(-5f, -3f));

        var scale = Random.Range(0.5f, 1.5f);
        transform.localScale = new Vector3(scale, scale, scale);
    }

    private void Update()
    {
        if (!m_Renderer.isVisible)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var hitObject = other.gameObject;
        var hitName = hitObject.name;

        if (hitName == "GroundTiles")
        {
            Explode();
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(explosionAudio, Camera.main.transform.position, 0.3f);
        }
        else if (hitName == "Dinosaur")
        {
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(explosionAudio, Camera.main.transform.position, 0.3f);
            hitObject.GetComponent<Dinosaur>().Die();
        }
    }

    private void Explode()
    {
        var loc = transform.position;
        loc.y -= GetComponent<SpriteRenderer>().bounds.size.y / 2;
        for (var i = 0; i < 30; i++)
        {
            var particle = Instantiate(particlePrefab, loc, Quaternion.identity).GetComponent<Particle>();
            particle.Initialize(new Vector2(Random.Range(-2f, 2f), Random.Range(0f, 2f)), m_ExplosionColors[Random.Range(0, m_ExplosionColors.Length)]);
        }
    }
}
