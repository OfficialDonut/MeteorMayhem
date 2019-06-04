using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

[RequireComponent(typeof(CharacterController2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class DinoPlayer : MonoBehaviour
{
    public GameObject particlePrefab;
    public AudioSource deathAudio;
    public float runSpeed;
    public float menuSpeed;
    private CharacterController2D m_Controller;
    private SpriteRenderer m_SpriteRenderer;
    private Animator m_Animator;
    private float m_Move;
    private readonly Color[] m_ParticleColors = {new Color(0, 128, 0), Color.green, Color.black};
    private static readonly int Speed = Animator.StringToHash("Speed");
    public bool IsDead { get; private set; }

    private void Awake()
    {
        m_Controller = GetComponent<CharacterController2D>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        m_Move = Menu.IsLoaded ? menuSpeed : Input.GetAxisRaw("Horizontal") * runSpeed;
        m_Animator.SetFloat(Speed, Mathf.Abs(m_Move));
    }

    private void FixedUpdate()
    {
        m_Controller.Move(m_Move * Time.fixedDeltaTime, false, false);
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (Menu.IsLoaded && other.gameObject.CompareTag("Border"))
            menuSpeed *= -1;
    }

    public void Die()
    {
        deathAudio.Play();
        m_SpriteRenderer.enabled = false;
        IsDead = true;
        for (var i = 0; i < 50; i++)
        {
            var particle = Instantiate(particlePrefab, transform.position, Quaternion.identity);
            particle.GetComponent<Particle>().Init(m_ParticleColors, new Vector2(Random.Range(-2f, 2f), Random.Range(-2f, 2f)), 0.5f);
        }
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(2);
        SceneManager.UnloadSceneAsync("Game");
        SceneManager.LoadScene("Menu", LoadSceneMode.Additive);
        transform.position = new Vector3(0, transform.position.y, 0);
        m_SpriteRenderer.enabled = true;
        IsDead = false;
    }
}
