using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevel : MonoBehaviour
{
    AudioSource myAudioSource;
    [SerializeField] float timeToWait = 2f;
    void Start()
    {
        myAudioSource=GetComponent<AudioSource>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            StartCoroutine(NextLevel());
        }
    }
    IEnumerator NextLevel()
    {
        myAudioSource.PlayOneShot(myAudioSource.clip);
        yield return new WaitForSecondsRealtime(timeToWait);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex+1;

        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex=0;
        }
        FindObjectOfType<ScenePersist>().ResetScenePresist();
        SceneManager.LoadScene(nextSceneIndex);
    }


}
