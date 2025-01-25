using TMPro;
using UnityEngine;

public class Controller : MonoBehaviour
{

    public TextMeshProUGUI maxSpawnText;
    public TextMeshProUGUI checkIntervalText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;

    private ObjectSpawner spawner;

    private int score;
    private float timer = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        spawner = GetComponent<ObjectSpawner>();

    }

    // Update is called once per frame
    void Update()
    {

        if (!timerRunning())
        {

            if (Input.GetKeyDown(KeyCode.I)) modifyMaxSpawn(-1);
            if (Input.GetKeyDown(KeyCode.O)) modifyMaxSpawn(1);
            if (Input.GetKeyDown(KeyCode.K)) modifyCheckInterval(-1);
            if (Input.GetKeyDown(KeyCode.L)) modifyCheckInterval(1);

        }

        if (timer > 0)
        {

            timer -= Time.deltaTime;
            if (timer < 0) timer = 0;

        }

        if (timerRunning())
        {

            if (timer < 10) spawner.maxSpawn = 5;
            else if (timer < 20) spawner.maxSpawn = 4;
            else if (timer < 30) spawner.maxSpawn = 3;
            else if (timer < 40) spawner.maxSpawn = 2;

        }

        if (Input.GetKeyDown(KeyCode.P) && !timerRunning()) startChallenge();

        if (Input.GetKeyDown(KeyCode.Escape)) endGame();

        maxSpawnText.text = "Max Spawn: " + spawner.maxSpawn;
        checkIntervalText.text = "Check Interval: " + spawner.checkInterval;
        scoreText.text = "Score: " + score;
        timerText.text = "Timer: " + timer.ToString("F2");

    }

    private void modifyMaxSpawn(int modifier)
    {

        spawner.maxSpawn += modifier;
        spawner.maxSpawn = Mathf.Clamp(spawner.maxSpawn, 1, 5);

    }

    private void modifyCheckInterval(int modifier)
    {

        spawner.checkInterval += modifier;
        spawner.checkInterval = Mathf.Clamp(spawner.checkInterval, 1, 10);

    }

    public void increaseScore()
    {

        score++;
        if (score < 0) score = 0;

    }

    public bool timerRunning()
    {

        return timer > 0;

    }

    private void startChallenge()
    {

        timer = 50;
        score = 0;
        spawner.maxSpawn = 1;

    }

    private void endGame()
    {

        Application.Quit();

    }

}
