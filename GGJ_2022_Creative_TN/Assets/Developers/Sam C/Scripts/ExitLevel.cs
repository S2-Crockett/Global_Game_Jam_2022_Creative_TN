using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            //GameManager.instance.UpdateGameState(GameState.Menu);
            //StartCoroutine(UIManager.instance.DelayedStart(GameState.Menu));
            SceneManager.LoadScene("Level_Completion_Scene");
        }
    }
}
