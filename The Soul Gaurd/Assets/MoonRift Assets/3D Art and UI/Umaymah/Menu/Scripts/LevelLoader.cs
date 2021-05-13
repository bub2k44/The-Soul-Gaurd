using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;
    //private GameMaster gm;
    int checkpoint;
    int index;
    private void Start()
    {
       // PlayerPrefs.SetInt("CheckPoint", 0);
        checkpoint = PlayerPrefs.GetInt("CheckPoint");
        Debug.Log(checkpoint);
        //gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    //public void LoadSettingsMenu()
    //{
    //    StartCoroutine(LoadLevel(SceneManager.GetSceneByBuildIndex().buildIndex = )
    //}

    public void LoadNextLevel()
    {

        //Checks the progress of player
        //if he has cleared the check point, they will continue with the fight
        //if not he will play the game again
        
       

        
            
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));

    }

    public void LoadLevel2()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        //gm.LastCheckPointPos = new Vector2(0, 1.5f);
    }

    public void ReloadLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
    }

    private IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex, LoadSceneMode.Single);
    }
    public void LoadTargetLevel(string target)
    {
        SceneManager.LoadScene(target);
    }
}
