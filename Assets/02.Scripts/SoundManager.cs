using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;

    public AudioClip[] bgmList;
    public AudioClip[] sfxList;
    private AudioSource audio;
    private int prevIndex = 0;
    private int currIndex = 0;

    // Use this for initialization
    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType(typeof(SoundManager)) as SoundManager; 
            }
            return instance;
        }
    }

    // Use this for initialization
    void Awake()
    {
        audio = GetComponent<AudioSource>();

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            audio.loop = true;
            
            audio.clip = bgmList[0];
        }
        else if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            audio.loop = true;

            audio.clip = bgmList[1];
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            audio.loop = false;       

            prevIndex = Random.Range(3, 6);

            audio.clip = bgmList[prevIndex];

            currIndex = prevIndex;
        }

        audio.Play();
    }

    public void PlaySFX(string audioName)
    {
        for (int i = 0; i < sfxList.Length; ++i)
        {
            if (sfxList[i].name == audioName)
            {
                audio.PlayOneShot(sfxList[i]);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {       
            if (!audio.isPlaying)
            {
                if (currIndex != prevIndex)
                {
                    audio.clip = bgmList[currIndex];

                    audio.Play();

                    prevIndex = currIndex;
                }
                else
                {
                    currIndex = Random.Range(3, 6);
                }
            }
        }
    }
}
