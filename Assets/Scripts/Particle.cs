using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class Particle : MonoBehaviour
{
    public void Init(Color color, Vector2 velocity, float life)
    {
        GetComponent<SpriteRenderer>().color = color;
        GetComponent<Rigidbody2D>().velocity = velocity;
        StartCoroutine(Kill(life));
    }
    
    public void Init(Color[] colorOptions, Vector2 velocity, float life)
    {
        Init(colorOptions[Random.Range(0, colorOptions.Length)], velocity, life);
    }

    private IEnumerator Kill(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
