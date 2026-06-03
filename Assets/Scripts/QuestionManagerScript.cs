using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionManagerScript : MonoBehaviour
{
    public List<QuestionScript> questions;
    public Canvas canvas;
    public RawImage image;
    private QuestionScript currentChosenQuestion;
    public bool lastAnswerCorrect = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        questions.Add(new QuestionScript(Resources.Load<Texture>("TestImg"), QuestionScript.Answer.A, QuestionScript.Unit.Derivatives, QuestionScript.Level.Ab, QuestionScript.Difficulty.Easy));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayQuestion()
    {
        currentChosenQuestion = getRandomQuestion();
        Debug.Log(currentChosenQuestion);
        image.texture = currentChosenQuestion.GetSprite();
        canvas.gameObject.SetActive(true);
    }

    private QuestionScript getRandomQuestion()
    {
        return new QuestionScript(Resources.Load<Texture>("TestImg"), QuestionScript.Answer.A,
            QuestionScript.Unit.Derivatives, QuestionScript.Level.Ab, QuestionScript.Difficulty.Easy);
        return questions[Random.Range(0, questions.Count)];
    }
    
    public bool InputAnswer(char answer)
    {
        QuestionScript.Answer newAnswer;
        if (answer == 'A')
        {
            newAnswer = QuestionScript.Answer.A;
        }
        else if (answer == 'B')
        {
            newAnswer = QuestionScript.Answer.B;
        }
        else if (answer == 'C')
        {
            newAnswer = QuestionScript.Answer.C;
        }
        else
        {
            newAnswer = QuestionScript.Answer.D;
        }
        canvas.gameObject.SetActive(false);
        return currentChosenQuestion.CheckAnswer(newAnswer);
    }
    
}
