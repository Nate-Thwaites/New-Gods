using Player;
using UnityEngine;


namespace player
{
    public class PlayerAnimSounds : MonoBehaviour
    {
        [SerializeField] PlayerScript playerScript;
        float timer;
        const float Delay = 0.35f;
        void Start()
        {
            timer = 0f;
            playerScript = GetComponentInParent<PlayerScript>();
        }

        // Update is called once per frame
        void Update()
        {
            timer -= Time.deltaTime;
        }

        public void Footsteps()
        {
            if (timer <= 0f)
            {
                playerScript.audioSource.PlayOneShot(AudioManager.instance.SFXClips[3]);
                timer = Delay;
            }


        }
    }
}
