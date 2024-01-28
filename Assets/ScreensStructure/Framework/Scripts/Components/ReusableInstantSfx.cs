using UnityEngine;

public class ReusableInstantSfx : MonoBehaviour
{
    public void Activate(Vector3 position)
    {
        gameObject.transform.position = position;
        gameObject.SetActive(true);
        GetComponent<ParticleSystem>().Play();
        if (GetComponent<AudioSource>().clip != null)
        {
            GetComponent<AudioSource>().Play();
        }
    }

    void Start()
    {
        GetComponent<AudioSource>().loop = false;
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (!GetComponent<ParticleSystem>().isPlaying)
        {
            if (!GetComponent<AudioSource>().isPlaying)
            {
                gameObject.SetActive(false);
            }
        }
    }
}

