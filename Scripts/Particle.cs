using System.Collections;
using UnityEngine;

public class Particle : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Kill());
    }

    public void Initialize(Vector2 velocity, Color color)
    {
        GetComponent<Rigidbody2D>().velocity = velocity;
        GetComponent<SpriteRenderer>().color = color;
    }

    private IEnumerator Kill()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
