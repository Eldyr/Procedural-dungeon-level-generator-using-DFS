using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace InfiniteLabyrinth.UI
{
    public class Menu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("RoomScene");
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Restart()
    {
        SceneManager.LoadScene("RoomScene");
    } 
    public void Resume()
    {
        GetComponent<PauseMenu>().ResumeGame();
    }
}
}
