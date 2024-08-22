using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Barracuda.TextureAsTensorData;

public class audio : MonoBehaviour
{
    // Start is called before the first frame update

    AudioSource audioSource;
    public bool x = false;

    public float time = 0;
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (x)
        {
            time = 0;
            audioSource.Play();
            x = false;
        }

        if (time > 3)
        {
            audioSource.Pause();
            time = 0;
        }

        time = time + Time.deltaTime;
    }
}
