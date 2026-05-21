using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    public Transform respawnPoint;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Checkpoint"))
        {
            print("new Checkpoint");
            respawnPoint.position = other.transform.position;
        }
    }
}
