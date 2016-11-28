using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public void GoToSurvivalLevel()
    {
        SceneManager.LoadScene(1);
    } 
}
