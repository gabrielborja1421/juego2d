using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnergyBarController : MonoBehaviour
{
    public float maxEnergy = 5f;
    public float currentEnergy = 5f;
    public float energyDecreaseRate = 1f;
    public bool start = false;
    public bool gameOver = false;
    public GameObject menuInicio;
    public GameObject menuGameOver;

    private Vector3 initialPosition;
    private Vector3 initialScale;

    private GameManager gameManager;
    private ScoreManager scoreManager;

    private void Start()
    {
        initialPosition = transform.localPosition;
        initialScale = transform.localScale;

        gameManager = FindObjectOfType<GameManager>();
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    private void Update()
    {
        if (!start && !gameOver)
        {
            menuInicio.SetActive(true);
            if (Input.GetKeyDown(KeyCode.X))
            {
                start = true;
            }
        }
        else if (gameOver)
        {
            menuGameOver.SetActive(true);
            if (Input.GetKeyDown(KeyCode.X))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                gameOver = false; // Reinicia la variable gameOver al reiniciar el juego
            }
        }
        else
        {
            menuInicio.SetActive(false);

            if (currentEnergy > 0)
            {
                currentEnergy -= energyDecreaseRate * Time.deltaTime;

                float energyRatio = currentEnergy / maxEnergy;
                Vector3 newPosition = initialPosition;
                newPosition.x += (initialScale.x - initialScale.x * energyRatio) / 2f;
                transform.localPosition = newPosition;

                Vector3 newScale = initialScale;
                newScale.x *= energyRatio;
                transform.localScale = newScale;
            }
            else
            {
                gameOver = true;
                gameManager.SetGameOver();

            }
        }
    }
}
