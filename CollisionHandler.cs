using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip crashAudio;
    [SerializeField] AudioClip successAudio;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;
 
    AudioSource audioSource;

    bool isControllable = true;
    bool isCollidable = true;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys()
    {
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            LoadNextScene();
        }
        else if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            isCollidable = !isCollidable;
            Debug.Log("Current collision mode: " + isCollidable);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!isControllable)
        {
            return;
        }

        if (!isCollidable)
        {
            return;
        }

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                break;

            case "Finish":
                StartSuccessSequence();
                break;

            case "Fuel":
                break;

            default:
                StartCrashSequence();
                break;
        }
    }

    private void StartSuccessSequence()
    {
        isControllable = false;
        successParticles.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(successAudio);
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextScene", levelLoadDelay);;
    }

    private void StartCrashSequence()
    {
        isControllable = false;
        crashParticles.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(crashAudio);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
    }

    void LoadNextScene()
    {
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }

        SceneManager.LoadScene(nextScene);
    }

    void ReloadLevel()
    {
        int sceneNumber = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneNumber);
    }
}
