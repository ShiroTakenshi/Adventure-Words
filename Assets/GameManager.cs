using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class QuestionData
{
    public Sprite imageQues;
    public string indonesiaKata;
    public string answerInggris;
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private ScriptableAnswer questionDataScriptable;
    [SerializeField] private TMP_Text questionText;
    [SerializeField] private Image QuesImage;
    [SerializeField] private WordData[] AnswerPrefabs;
    [SerializeField] private WordData[] WordPrefabs;
    [SerializeField] private GameObject gameOverWins;
    [SerializeField] private GameObject gameOverLose;
    [SerializeField] private Player player, enemy;
    [SerializeField] GameObject timerImage;
    [SerializeField] private float maxTime = 10f;
    private char[] charWordArray = new char[12];
    private int currentAnswerIndex = 0;
    private bool isAnswer = true;
    private List<int> WordSelectIndex;
    private int currentQuestionIndex = 0;
    private GameState gameState = GameState.OnPLay;
    private string answerWords;
    private float timeLeft;
    private bool couritine = false;

    public enum GameState
    {
        OnPLay,
        Next
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        WordSelectIndex = new List<int>();
        QuesImage.enabled = false;
    }

    private void Start()
    {
        WordSelectIndex = new List<int>();
        QuestionSet();
        timeLeft = maxTime;
    }

    private void Update()
    {
        player.UpdateHealth();
        enemy.UpdateHealth();
        if (timeLeft > 0)
        {
            try
            {
                Image timerImage = GameObject.Find("TimerBar").GetComponent<Image>();
                timeLeft -= Time.deltaTime;
                timerImage.fillAmount = timeLeft / maxTime;
            }
            catch (NullReferenceException)
            {
                timerImage.SetActive(false);
            }
        }
        else
        {
            gameOverLose.SetActive(true);
            player.health = 0;
            if (!couritine)
                StartCoroutine(attackTimeOutScenes());
            // timerImage.SetActive(false);
        }
    }



    private void QuestionSet()
    {
        gameState = GameState.OnPLay;

        questionText.text = questionDataScriptable.questions[currentQuestionIndex].indonesiaKata;
        QuesImage.sprite = questionDataScriptable.questions[currentQuestionIndex].imageQues;
        answerWords = questionDataScriptable.questions[currentQuestionIndex].answerInggris;

        QuestionResets();
        WordSelectIndex.Clear();

        Array.Clear(charWordArray, 0, charWordArray.Length);

        for (int i = 0; i < answerWords.Length; i++)
        {
            charWordArray[i] = char.ToUpper(answerWords[i]);
        }

        for (int i = answerWords.Length; i < WordPrefabs.Length; i++)
        {
            charWordArray[i] = (char)UnityEngine.Random.Range(65, 91);
        }

        charWordArray = ShuffleWordList.ShuffleListItems<char>(charWordArray.ToList()).ToArray();

        for (int i = 0; i < WordPrefabs.Length; i++)
        {
            WordPrefabs[i].SetChar(charWordArray[i]);
        }
    }

    public void SelectedOptions(WordData wordData)
    {

        if (gameState == GameState.Next || currentAnswerIndex >= answerWords.Length)
        {
            return;
        }

        WordSelectIndex.Add(wordData.transform.GetSiblingIndex());
        wordData.gameObject.SetActive(false);
        AnswerPrefabs[currentAnswerIndex].SetChar(wordData.CharValue);

        currentAnswerIndex++;
        if (player.health <= 0)
        {
            gameOverLose.SetActive(true);
        }

        if (currentAnswerIndex == answerWords.Length)
        {
            isAnswer = true;

            for (int i = 0; i < answerWords.Length; i++)
            {
                if (char.ToUpper(answerWords[i]) != char.ToUpper(AnswerPrefabs[i].CharValue))
                {
                    isAnswer = false;
                    break;
                }
            }
            // kalau Jawaban Benar
            if (isAnswer)
            {
                player.health += player.restoreHealth;
                Debug.Log("Jawaban Benar");
                enemy.health -= 25;
                gameState = GameState.Next;
                timeLeft += 2;
                currentQuestionIndex++;

                if (currentQuestionIndex < questionDataScriptable.questions.Count)
                {
                    Invoke("QuestionSet", 0.5f);
                }
                else
                {
                    Debug.Log("Game Selesai");
                    gameOverWins.SetActive(true);
                    enemy.health = 0;
                    player.AnimateAttack();
                    timerImage.SetActive(false);
                }
            }
            // kalau Jawaban salah
            else if (!isAnswer)
            {
                this.enemy.health += this.enemy.restoreHealth;
                Debug.Log("Salah");
                player.TakeDamage();
                if (player.health <= 0)
                {
                    gameOverLose.SetActive(true);
                    player.health = 0;
                    enemy.AnimateAttack();
                    timerImage.SetActive(false);
                }
                else
                {
                    QuestionResets();
                }
            }
        }
    }


    public void QuestionResets()
    {
        for (int i = 0; i < AnswerPrefabs.Length; i++)
        {
            AnswerPrefabs[i].gameObject.SetActive(true);
            AnswerPrefabs[i].SetChar('_');
        }
        for (int i = answerWords.Length; i < AnswerPrefabs.Length; i++)
        {
            AnswerPrefabs[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < WordPrefabs.Length; i++)
        {
            WordPrefabs[i].gameObject.SetActive(true);
        }
        currentAnswerIndex = 0;
    }

    public void hintAnswer()
    {
        // event hint answer pada button
        if (AnswerPrefabs[currentAnswerIndex].CharValue.Equals('_'))
        {
            AnswerPrefabs[currentAnswerIndex].SetChar(char.ToUpper(answerWords[currentAnswerIndex]));
            if (currentAnswerIndex == answerWords.Length - 1)
            {
                QuesImage.enabled = true;
                QuesImage.sprite = questionDataScriptable.questions[currentQuestionIndex].imageQues;
            }
            currentAnswerIndex++;
        }

        // cek health player jika health player kurang dari 0 maka game over
        player.health = player.health - player.attackDamage;
        if (player.health <= 0)
        {
            player.health = 0;
            if (player.health <= 0)
                {
                    gameOverLose.SetActive(true);
                    player.health = 0;
                    enemy.AnimateAttack();
                    if (!couritine)
                        StartCoroutine(attackTimeOutScenes());
                    timerImage.SetActive(false);
                }
            gameOverLose.SetActive(true);
        }

        // cek jika jawaban sudah benar
        if (currentAnswerIndex == answerWords.Length)
        {
            isAnswer = true;
            for (int i = 0; i < answerWords.Length; i++)
            {
                if (char.ToUpper(answerWords[i]) != char.ToUpper(AnswerPrefabs[i].CharValue))
                {
                    isAnswer = false;
                    break;
                }
            }
            if (isAnswer)
            {
                Debug.Log("Jawaban Benar");
                gameState = GameState.Next;
                currentQuestionIndex++;

                if (currentQuestionIndex < questionDataScriptable.questions.Count)
                {
                    Invoke("QuestionSet", 0.5f);
                }
                else
                {
                    Debug.Log("Game Selesai");
                    gameOverWins.SetActive(true);
                    enemy.health = 0;
                    player.AnimateAttack();
                    timerImage.SetActive(false);
                }
            }
            else if (!isAnswer)
            {
                Debug.Log("Salah");
                player.TakeDamage();
                if (player.health <= 0)
                {
                    gameOverLose.SetActive(true);
                    player.health = 0;
                    enemy.AnimateAttack();
                    if (!couritine)
                        StartCoroutine(attackTimeOutScenes());
                    timerImage.SetActive(false);
                }
                else
                {
                    QuestionResets();
                }
            }
        }

    }


    public void LastWordReset()
    {
        if (WordSelectIndex.Count > 0)
        {
            int index = WordSelectIndex[WordSelectIndex.Count - 1];
            WordPrefabs[index].gameObject.SetActive(true);
            WordSelectIndex.RemoveAt(WordSelectIndex.Count - 1);

            currentAnswerIndex--;
            if (currentAnswerIndex <= 0)
            {
                currentAnswerIndex = 0;
            }
            AnswerPrefabs[currentAnswerIndex].SetChar('_');
        }
    }

    IEnumerator attackTimeOutScenes()
    {
        couritine = true;
        yield return null;
        enemy.AnimateAttack();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToGameplay()
    {
        SceneManager.LoadScene("Platformer 2");
    }

}

