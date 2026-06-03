using UnityEngine;
using UnityEngine.SceneManagement;

public class AbVsBc : MonoBehaviour
{
    private QuestionScript.Level selectedLevel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        selectedLevel = QuestionScript.Level.Bc;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetLevelAb()
    {
        selectedLevel = QuestionScript.Level.Ab;
        SceneManager.LoadScene("SampleScene");
    }
    
    public void SetLevelBc()
    {
        selectedLevel = QuestionScript.Level.Bc;
        SceneManager.LoadScene("SampleScene");
    }

    public QuestionScript.Level GetSelectedLevel()
    {
        return selectedLevel;
    }
}
