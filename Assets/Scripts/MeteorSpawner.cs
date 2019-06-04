using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    public GameObject meteorPrefab;
    public int numMeteors;
    public int maxMeteors;
    public float incrementFrequency;
    public float meteorFrequency;
    
    private void Start()
    {
        InvokeRepeating(nameof(Spawn), 1, meteorFrequency);
        InvokeRepeating(nameof(IncreaseDifficulty), 1 + incrementFrequency, incrementFrequency);
    }

    private void Spawn()
    {
        var mainCamera = Camera.main;
        for (var i = 0; i < numMeteors; i++)
        {
            var bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
            var topRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));
            var spawnLoc = new Vector3(Random.Range(bottomLeft.x, topRight.x), topRight.y, 0);
            Instantiate(meteorPrefab, spawnLoc, Quaternion.identity);
        }
    }
    
    private void IncreaseDifficulty()
    {
        if (numMeteors < maxMeteors)
            numMeteors++;
    }
}
