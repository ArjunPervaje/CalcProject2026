using UnityEngine;

public class QuestionScript
{
    public enum Unit
    {
        Derivatives,
        Integrals,
        Limits,
        ImplicitDifferentiation,
        RelatedRates,
        Optimization,
        PolarCoordinates,
        Parametric,
        Series,
    }

    public enum Level
    {
        Ab,
        Bc,
    }

    public enum Difficulty
    {
        Easy,
        Medium,
        Hard,
    }

    public enum Answer
    {
        A,
        B,
        C,
        D,
    }

    private Unit unit;
    private Level level;
    private Difficulty difficulty;
    private Answer answer;
    private Texture question;

    public QuestionScript(Texture question, Answer answer, Unit unit, Level level, Difficulty difficulty)
    {
        this.answer = answer;
        this.unit = unit;
        this.level = level;
        this.difficulty = difficulty;
        this.question = question;
    }
    
    public bool CheckAnswer(Answer answer)
    {
        return (this.answer == answer);
    }

    public Texture GetSprite()
    {
        return question;
    }

    public Level GetLevel()
    {
        return level;
    }

    public Unit GetUnit()
    {
        return unit;
    }

    public Difficulty GetDifficulty()
    {
        return difficulty;
    }
}
