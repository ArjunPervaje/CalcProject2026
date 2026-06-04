using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionManagerScript : MonoBehaviour
{
    public List<QuestionScript> questions = new System.Collections.Generic.List<QuestionScript>();
    public Canvas canvas;
    public RawImage image;
    private QuestionScript currentChosenQuestion;
    public bool lastAnswerCorrect = false;

    private QuestionScript.Level AbOrBc;

    public enum QuestionToGet
    {
        Easy,
        Medium,
        Hard,
        Any,
        Series,
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        AbOrBc = GameObject.FindGameObjectWithTag("AbBc").GetComponent<AbVsBc>().GetSelectedLevel();
        
        // Here you can add all the questions
        // questions.Add(new QuestionScript(Resources.Load<Texture>("TestImg"), QuestionScript.Answer.A, QuestionScript.Unit.Derivatives, QuestionScript.Level.Ab, QuestionScript.Difficulty.Easy));
        
        // AB Derivatives Easy
        questions.Add(new QuestionScript(Resources.Load<Texture>("Question1"), QuestionScript.Answer.A, QuestionScript.Unit.Derivatives, QuestionScript.Level.Ab, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("Question2"), QuestionScript.Answer.A, QuestionScript.Unit.Derivatives, QuestionScript.Level.Ab, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("Question3"), QuestionScript.Answer.C, QuestionScript.Unit.Derivatives, QuestionScript.Level.Ab, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("Question4"), QuestionScript.Answer.D, QuestionScript.Unit.Derivatives, QuestionScript.Level.Ab, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("Question5"), QuestionScript.Answer.D, QuestionScript.Unit.Derivatives, QuestionScript.Level.Ab, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("Question6"), QuestionScript.Answer.D, QuestionScript.Unit.Derivatives, QuestionScript.Level.Ab, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("Question7"), QuestionScript.Answer.B, QuestionScript.Unit.Derivatives, QuestionScript.Level.Ab, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("Question8"), QuestionScript.Answer.C, QuestionScript.Unit.Derivatives, QuestionScript.Level.Ab, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("Question9"), QuestionScript.Answer.A, QuestionScript.Unit.Derivatives, QuestionScript.Level.Ab, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("Question10"), QuestionScript.Answer.D, QuestionScript.Unit.Derivatives, QuestionScript.Level.Ab, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("Question11"), QuestionScript.Answer.C, QuestionScript.Unit.Derivatives, QuestionScript.Level.Ab, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("AbDer12"), QuestionScript.Answer.A, QuestionScript.Unit.Derivatives, QuestionScript.Level.Ab, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("AbDer13"), QuestionScript.Answer.C, QuestionScript.Unit.Derivatives, QuestionScript.Level.Ab, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("AbDer14"), QuestionScript.Answer.A, QuestionScript.Unit.Derivatives, QuestionScript.Level.Ab, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("AbDer15"), QuestionScript.Answer.B, QuestionScript.Unit.Derivatives, QuestionScript.Level.Ab, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("AbDer16"), QuestionScript.Answer.A, QuestionScript.Unit.Derivatives, QuestionScript.Level.Ab, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("AbDer17"), QuestionScript.Answer.D, QuestionScript.Unit.Derivatives, QuestionScript.Level.Ab, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("AbDer18"), QuestionScript.Answer.B, QuestionScript.Unit.Derivatives, QuestionScript.Level.Ab, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("AbDer19"), QuestionScript.Answer.A, QuestionScript.Unit.Derivatives, QuestionScript.Level.Ab, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("AbDer20"), QuestionScript.Answer.C, QuestionScript.Unit.Derivatives, QuestionScript.Level.Ab, QuestionScript.Difficulty.Easy));
        
        // AB Integrals Easy
        questions.Add(new QuestionScript(Resources.Load<Texture>("AbInt1"), QuestionScript.Answer.B, QuestionScript.Unit.Integrals, QuestionScript.Level.Ab, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("AbInt2"), QuestionScript.Answer.C, QuestionScript.Unit.Integrals, QuestionScript.Level.Ab, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("AbInt3"), QuestionScript.Answer.A, QuestionScript.Unit.Integrals, QuestionScript.Level.Ab, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("AbInt4"), QuestionScript.Answer.B, QuestionScript.Unit.Integrals, QuestionScript.Level.Ab, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("AbInt5"), QuestionScript.Answer.D, QuestionScript.Unit.Integrals, QuestionScript.Level.Ab, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("AbInt6"), QuestionScript.Answer.C, QuestionScript.Unit.Integrals, QuestionScript.Level.Ab, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("AbInt7"), QuestionScript.Answer.B, QuestionScript.Unit.Integrals, QuestionScript.Level.Ab, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("AbInt8"), QuestionScript.Answer.C, QuestionScript.Unit.Integrals, QuestionScript.Level.Ab, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("AbInt9"), QuestionScript.Answer.D, QuestionScript.Unit.Integrals, QuestionScript.Level.Ab, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("AbInt10"), QuestionScript.Answer.C, QuestionScript.Unit.Integrals, QuestionScript.Level.Ab, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("AbInt11"), QuestionScript.Answer.D, QuestionScript.Unit.Integrals, QuestionScript.Level.Ab, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("AbInt12"), QuestionScript.Answer.A, QuestionScript.Unit.Integrals, QuestionScript.Level.Ab, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("AbInt13"), QuestionScript.Answer.B, QuestionScript.Unit.Integrals, QuestionScript.Level.Ab, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("AbInt14"), QuestionScript.Answer.C, QuestionScript.Unit.Integrals, QuestionScript.Level.Ab, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("AbInt15"), QuestionScript.Answer.A, QuestionScript.Unit.Integrals, QuestionScript.Level.Ab, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("AbInt16"), QuestionScript.Answer.D, QuestionScript.Unit.Integrals, QuestionScript.Level.Ab, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("AbInt17"), QuestionScript.Answer.B, QuestionScript.Unit.Integrals, QuestionScript.Level.Ab, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("AbInt18"), QuestionScript.Answer.D, QuestionScript.Unit.Integrals, QuestionScript.Level.Ab, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("AbInt19"), QuestionScript.Answer.C, QuestionScript.Unit.Integrals, QuestionScript.Level.Ab, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("AbInt20"), QuestionScript.Answer.A, QuestionScript.Unit.Integrals, QuestionScript.Level.Ab, QuestionScript.Difficulty.Easy));
        
        // AB Limits Easy
        questions.Add(new QuestionScript(Resources.Load<Texture>("AbLim1"), QuestionScript.Answer.C, QuestionScript.Unit.Limits, QuestionScript.Level.Ab, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("AbLim2"), QuestionScript.Answer.A, QuestionScript.Unit.Limits, QuestionScript.Level.Ab, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("AbLim3"), QuestionScript.Answer.B, QuestionScript.Unit.Limits, QuestionScript.Level.Ab, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("AbLim4"), QuestionScript.Answer.C, QuestionScript.Unit.Limits, QuestionScript.Level.Ab, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("AbLim5"), QuestionScript.Answer.D, QuestionScript.Unit.Limits, QuestionScript.Level.Ab, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("AbLim6"), QuestionScript.Answer.B, QuestionScript.Unit.Limits, QuestionScript.Level.Ab, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("AbLim7"), QuestionScript.Answer.A, QuestionScript.Unit.Limits, QuestionScript.Level.Ab, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("AbLim8"), QuestionScript.Answer.C, QuestionScript.Unit.Limits, QuestionScript.Level.Ab, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("AbLim9"), QuestionScript.Answer.B, QuestionScript.Unit.Limits, QuestionScript.Level.Ab, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("AbLim10"), QuestionScript.Answer.A, QuestionScript.Unit.Limits, QuestionScript.Level.Ab, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("AbLim11"), QuestionScript.Answer.D, QuestionScript.Unit.Limits, QuestionScript.Level.Ab, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("AbLim12"), QuestionScript.Answer.C, QuestionScript.Unit.Limits, QuestionScript.Level.Ab, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("AbLim13"), QuestionScript.Answer.A, QuestionScript.Unit.Limits, QuestionScript.Level.Ab, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("AbLim14"), QuestionScript.Answer.C, QuestionScript.Unit.Limits, QuestionScript.Level.Ab, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("AbLim15"), QuestionScript.Answer.D, QuestionScript.Unit.Limits, QuestionScript.Level.Ab, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("AbLim16"), QuestionScript.Answer.A, QuestionScript.Unit.Limits, QuestionScript.Level.Ab, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("AbLim17"), QuestionScript.Answer.B, QuestionScript.Unit.Limits, QuestionScript.Level.Ab, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("AbLim18"), QuestionScript.Answer.D, QuestionScript.Unit.Limits, QuestionScript.Level.Ab, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("AbLim19"), QuestionScript.Answer.A, QuestionScript.Unit.Limits, QuestionScript.Level.Ab, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("AbLim20"), QuestionScript.Answer.B, QuestionScript.Unit.Limits, QuestionScript.Level.Ab, QuestionScript.Difficulty.Easy));
        
        // AB Implicit Differentiation Medium
        questions.Add(new QuestionScript(Resources.Load<Texture>("AbImp1"), QuestionScript.Answer.A, QuestionScript.Unit.ImplicitDifferentiation, QuestionScript.Level.Ab, QuestionScript.Difficulty.Medium));
        questions.Add(new QuestionScript(Resources.Load<Texture>("AbImp2"), QuestionScript.Answer.B, QuestionScript.Unit.ImplicitDifferentiation, QuestionScript.Level.Ab, QuestionScript.Difficulty.Medium));
        questions.Add(new QuestionScript(Resources.Load<Texture>("AbImp3"), QuestionScript.Answer.C, QuestionScript.Unit.ImplicitDifferentiation, QuestionScript.Level.Ab, QuestionScript.Difficulty.Medium));
        questions.Add(new QuestionScript(Resources.Load<Texture>("AbImp4"), QuestionScript.Answer.A, QuestionScript.Unit.ImplicitDifferentiation, QuestionScript.Level.Ab, QuestionScript.Difficulty.Medium));
        questions.Add(new QuestionScript(Resources.Load<Texture>("AbImp5"), QuestionScript.Answer.D, QuestionScript.Unit.ImplicitDifferentiation, QuestionScript.Level.Ab, QuestionScript.Difficulty.Medium));
        questions.Add(new QuestionScript(Resources.Load<Texture>("AbImp6"), QuestionScript.Answer.B, QuestionScript.Unit.ImplicitDifferentiation, QuestionScript.Level.Ab, QuestionScript.Difficulty.Medium));
        questions.Add(new QuestionScript(Resources.Load<Texture>("AbImp7"), QuestionScript.Answer.C, QuestionScript.Unit.ImplicitDifferentiation, QuestionScript.Level.Ab, QuestionScript.Difficulty.Medium));
        questions.Add(new QuestionScript(Resources.Load<Texture>("AbImp8"), QuestionScript.Answer.B, QuestionScript.Unit.ImplicitDifferentiation, QuestionScript.Level.Ab, QuestionScript.Difficulty.Medium));
        questions.Add(new QuestionScript(Resources.Load<Texture>("AbImp9"), QuestionScript.Answer.D, QuestionScript.Unit.ImplicitDifferentiation, QuestionScript.Level.Ab, QuestionScript.Difficulty.Medium));
        questions.Add(new QuestionScript(Resources.Load<Texture>("AbImp10"), QuestionScript.Answer.A, QuestionScript.Unit.ImplicitDifferentiation, QuestionScript.Level.Ab, QuestionScript.Difficulty.Medium));
        
        // AB Related Rates Medium
        
        
        // AB Optimization Medium
        
        // AB Series Hard
        
        // BC Integrals Easy
        questions.Add(new QuestionScript(Resources.Load<Texture>("Question80"), QuestionScript.Answer.A, QuestionScript.Unit.Integrals, QuestionScript.Level.Bc, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("Question81"), QuestionScript.Answer.D, QuestionScript.Unit.Integrals, QuestionScript.Level.Bc, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("Question82"), QuestionScript.Answer.D, QuestionScript.Unit.Integrals, QuestionScript.Level.Bc, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("Question83"), QuestionScript.Answer.A, QuestionScript.Unit.Integrals, QuestionScript.Level.Bc, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("Question84"), QuestionScript.Answer.C, QuestionScript.Unit.Integrals, QuestionScript.Level.Bc, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("Question85"), QuestionScript.Answer.B, QuestionScript.Unit.Integrals, QuestionScript.Level.Bc, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("Question86"), QuestionScript.Answer.A, QuestionScript.Unit.Integrals, QuestionScript.Level.Bc, QuestionScript.Difficulty.Easy));
        
        // BC Polar Coordinates Easy
        questions.Add(new QuestionScript(Resources.Load<Texture>("Question87"), QuestionScript.Answer.A, QuestionScript.Unit.PolarCoordinates, QuestionScript.Level.Bc, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("Question88"), QuestionScript.Answer.C, QuestionScript.Unit.PolarCoordinates, QuestionScript.Level.Bc, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("Question89"), QuestionScript.Answer.D, QuestionScript.Unit.PolarCoordinates, QuestionScript.Level.Bc, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("Question90"), QuestionScript.Answer.B, QuestionScript.Unit.PolarCoordinates, QuestionScript.Level.Bc, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("Question91"), QuestionScript.Answer.D, QuestionScript.Unit.PolarCoordinates, QuestionScript.Level.Bc, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("Question92"), QuestionScript.Answer.B, QuestionScript.Unit.PolarCoordinates, QuestionScript.Level.Bc, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("Question93"), QuestionScript.Answer.A, QuestionScript.Unit.PolarCoordinates, QuestionScript.Level.Bc, QuestionScript.Difficulty.Easy));
        
        // BC Polar Coordinates Medium
        
        // BC Parametric Easy
        questions.Add(new QuestionScript(Resources.Load<Texture>("BC_Parametric_Easy_(B)_1"), QuestionScript.Answer.B, QuestionScript.Unit.Parametric, QuestionScript.Level.Bc, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("BC_Parametric_Easy_(B)_2"), QuestionScript.Answer.B, QuestionScript.Unit.Parametric, QuestionScript.Level.Bc, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("BC_Parametric_Easy_(A)_3"), QuestionScript.Answer.A, QuestionScript.Unit.Parametric, QuestionScript.Level.Bc, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("BC_Parametric_Easy_(D)_4"), QuestionScript.Answer.D, QuestionScript.Unit.Parametric, QuestionScript.Level.Bc, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("BC_Parametric_Easy_(C)_5"), QuestionScript.Answer.C, QuestionScript.Unit.Parametric, QuestionScript.Level.Bc, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("BC_Parametric_Easy_(C)_6"), QuestionScript.Answer.C, QuestionScript.Unit.Parametric, QuestionScript.Level.Bc, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("BC_Parametric_Easy_(B)_7"), QuestionScript.Answer.B, QuestionScript.Unit.Parametric, QuestionScript.Level.Bc, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("BC_Parametric_Easy_(B)_8"), QuestionScript.Answer.B, QuestionScript.Unit.Parametric, QuestionScript.Level.Bc, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("BC_Parametric_Easy_(B)_9"), QuestionScript.Answer.B, QuestionScript.Unit.Parametric, QuestionScript.Level.Bc, QuestionScript.Difficulty.Easy));
        questions.Add(new QuestionScript(Resources.Load<Texture>("BC_Parametric_Easy_(A)_10"), QuestionScript.Answer.A, QuestionScript.Unit.Parametric, QuestionScript.Level.Bc, QuestionScript.Difficulty.Easy));
        
        // BC Series Hard
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayQuestion(QuestionToGet questionToGet)
    {
        currentChosenQuestion = GetRandomQuestion(questionToGet);
        
        image.texture = currentChosenQuestion.GetSprite();
        canvas.gameObject.SetActive(true);
    }

    private QuestionScript GetRandomQuestion(QuestionToGet questionToGet)
    {
        QuestionScript potentialQuestion = questions[Random.Range(0, questions.Count)];
        switch (questionToGet)
        {
            case QuestionToGet.Any:
            {
                if (AbOrBc == QuestionScript.Level.Ab)
                {
                    if (potentialQuestion.GetLevel() != QuestionScript.Level.Ab ||
                        potentialQuestion.GetUnit() == QuestionScript.Unit.Series)
                    {
                        return GetRandomQuestion(questionToGet);
                    }
                    else
                    {
                        return potentialQuestion;
                    }
                }
                else
                {
                    if (potentialQuestion.GetUnit() == QuestionScript.Unit.Series)
                    {
                        return GetRandomQuestion(questionToGet);
                    }
                    else
                    {
                        return potentialQuestion;
                    }
                }
            }
            case QuestionToGet.Easy:
            {
                if (AbOrBc == QuestionScript.Level.Ab)
                {
                    if (potentialQuestion.GetLevel() != QuestionScript.Level.Ab ||
                        potentialQuestion.GetUnit() == QuestionScript.Unit.Series ||
                        potentialQuestion.GetDifficulty() != QuestionScript.Difficulty.Easy)
                    {
                        return GetRandomQuestion(questionToGet);
                    }
                    else
                    {
                        return potentialQuestion;
                    }
                }
                else
                {
                    if (potentialQuestion.GetUnit() == QuestionScript.Unit.Series ||
                        potentialQuestion.GetDifficulty() != QuestionScript.Difficulty.Easy)
                    {
                        return GetRandomQuestion(questionToGet);
                    }
                    else
                    {
                        return potentialQuestion;
                    }
                }
            }
            case QuestionToGet.Medium:
            {
                if (AbOrBc == QuestionScript.Level.Ab)
                {
                    if (potentialQuestion.GetLevel() != QuestionScript.Level.Ab ||
                        potentialQuestion.GetUnit() == QuestionScript.Unit.Series ||
                        potentialQuestion.GetDifficulty() != QuestionScript.Difficulty.Medium)
                    {
                        return GetRandomQuestion(questionToGet);
                    }
                    else
                    {
                        return potentialQuestion;
                    }
                }
                else
                {
                    if (potentialQuestion.GetUnit() == QuestionScript.Unit.Series ||
                        potentialQuestion.GetDifficulty() != QuestionScript.Difficulty.Medium)
                    {
                        return GetRandomQuestion(questionToGet);
                    }
                    else
                    {
                        return potentialQuestion;
                    }
                }
            }
            case QuestionToGet.Hard:
            {
                if (AbOrBc == QuestionScript.Level.Ab)
                {
                    if (potentialQuestion.GetLevel() != QuestionScript.Level.Ab ||
                        potentialQuestion.GetUnit() == QuestionScript.Unit.Series ||
                        potentialQuestion.GetDifficulty() != QuestionScript.Difficulty.Hard)
                    {
                        return GetRandomQuestion(questionToGet);
                    }
                    else
                    {
                        return potentialQuestion;
                    }
                }
                else
                {
                    if (potentialQuestion.GetUnit() == QuestionScript.Unit.Series ||
                        potentialQuestion.GetDifficulty() != QuestionScript.Difficulty.Hard)
                    {
                        return GetRandomQuestion(questionToGet);
                    }
                    else
                    {
                        return potentialQuestion;
                    }
                }
            }
            case QuestionToGet.Series:
            {
                if (AbOrBc == QuestionScript.Level.Ab)
                {
                    if (potentialQuestion.GetLevel() != QuestionScript.Level.Ab ||
                        potentialQuestion.GetUnit() != QuestionScript.Unit.Series)
                    {
                        return GetRandomQuestion(questionToGet);
                    }
                    else
                    {
                        return potentialQuestion;
                    }
                }
                else
                {
                    if (potentialQuestion.GetUnit() != QuestionScript.Unit.Series)
                    {
                        return GetRandomQuestion(questionToGet);
                    }
                    else
                    {
                        return potentialQuestion;
                    }
                }
            }
        }
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
