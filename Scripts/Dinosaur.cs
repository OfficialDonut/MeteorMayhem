using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dinosaur : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator runningAnimator;
    public GameObject particlePrefab;
    public int runSpeed;
    public int menuSpeed;
    private static Dinosaur _instance;
    private static readonly int Speed = Animator.StringToHash("Speed");
    private float m_Move;
    private readonly Color[] m_ExplosionColors = {new Color(0, 128, 0), Color.green, Color.black};

    private void Awake()
    {
        if (_instance != null) 
        {
            Destroy(gameObject);
            return;
        }
        
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        m_Move = SceneManager.GetActiveScene().buildIndex == 0 ? menuSpeed : Input.GetAxisRaw("Horizontal") * runSpeed;
        runningAnimator.SetFloat(Speed, Mathf.Abs(m_Move));
    }

    private void FixedUpdate()
    {
        controller.Move(m_Move * Time.fixedDeltaTime, false, false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (SceneManager.GetActiveScene().buildIndex == 0 && collision.collider.CompareTag("Border"))
        {
            menuSpeed *= -1;
        }
    }

    public void Die()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<Collider2D>().enabled = false;
        for (var i = 0; i < 30; i++)
        {
            var particle = Instantiate(particlePrefab, transform.position, Quaternion.identity).GetComponent<Particle>();
            particle.Initialize(new Vector2(Random.Range(-2f, 2f), Random.Range(-2f, 2f)), m_ExplosionColors[Random.Range(0, m_ExplosionColors.Length)]);
        }

        StartCoroutine(GameOver());
    }

    private void Respawn()
    {
        var trans = transform;
        trans.position = new Vector3(0, trans.position.y, 0);
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Collider2D>().enabled = true;
    }
    
    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(0, LoadSceneMode.Single);
        Respawn();
    }
}
