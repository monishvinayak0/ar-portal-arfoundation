using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Ui_Manager : MonoBehaviour
{
    [SerializeField]
    private GameObject roomPrefab;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DontDestroyOnLoad(this);
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            SceneCheck();
        }
    }


    public void SceneCheck()
    {
        PlaceOnPlane.Instance.placedPrefab = roomPrefab;
    }

    public void LoadRoom(GameObject go)
    {
        roomPrefab = go;
    }

    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void NextPanel(GameObject panel1,GameObject panel2)
    {
        panel1.SetActive(false);
     //   panel02.SetActive(true);
    }
    public void OnApplicationQuit()
    {
        Application.Quit();
    }
}
