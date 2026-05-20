using UnityEngine;

public class PostureScript : MonoBehaviour
{

    public float posture;
    public float maxPosture = 100f;
    public float minPosture = 0f;
    public float postureDamage;


    void Start()
    {
        posture = minPosture;
    }

    void Update()
    {
        if (posture < 0)
        {
            posture = 0;
        }

        if (posture > maxPosture)
        {
            posture = maxPosture;
        }
    }
}
