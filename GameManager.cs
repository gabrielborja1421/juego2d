using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic; // Agrega esta línea

public class GameManager : MonoBehaviour
{
    public float velocidad = 2;
    public Renderer bg;
    public GameObject col1;
    public GameObject picos1;
    public GameObject picos2;
    public GameObject rasho1;
    public GameObject rasho2;
    public GameObject rasho3;
    public bool start = false;
    public bool gameOver = false;

    public GameObject menuInicio;
    public GameObject menuGameOver;
    public List<GameObject> suelo;
    public List<GameObject> obstaculos;
    public List<GameObject> rayos;

    private EnergyBarController energyBarController;
    private ScoreManager scoreManager;

    private void Start()
    {
        energyBarController = FindObjectOfType<EnergyBarController>();
        scoreManager = FindObjectOfType<ScoreManager>();

        // Crear Mapa
        for (int i = 0; i < 23; i++)
        {
            suelo.Add(Instantiate(col1, new Vector2(-10 + i, -3), Quaternion.identity));
        }

        //Crear Obstaculos
        obstaculos.Add(Instantiate(picos1, new Vector2(15, -2), Quaternion.identity));
        obstaculos.Add(Instantiate(picos2, new Vector2(20, -2), Quaternion.identity));

        //Crear rayos iniciales
        SpawnRayos(3);
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
            }
        }
        else
        {
            menuInicio.SetActive(false);
            //Mover BG
            bg.material.mainTextureOffset = bg.material.mainTextureOffset + new Vector2(0.015f, 0) * velocidad * Time.deltaTime;

            // Mover Mapa
            for (int i = 0; i < suelo.Count; i++)
            {
                if (suelo[i].transform.position.x <= -11)
                {
                    suelo[i].transform.position = new Vector3(11f, -3, 0);
                }
                suelo[i].transform.position = suelo[i].transform.position + new Vector3(-1, 0, 0) * velocidad * Time.deltaTime;
            }

            //Mover Obstaculos
            for (int i = 0; i < obstaculos.Count; i++)
            {
                if (obstaculos[i].transform.position.x <= -10)
                {
                    float randomObs = Random.Range(10, 18);
                    obstaculos[i].transform.position = new Vector3(randomObs, -2, 0);
                }
                obstaculos[i].transform.position = obstaculos[i].transform.position + new Vector3(-1, 0) * velocidad * Time.deltaTime;
            }

            // Mover rayos y crear
            for (int i = rayos.Count - 1; i >= 0; i--)
            {
                if (rayos[i] == null)
                {
                    rayos.RemoveAt(i);
                    continue;
                }

                rayos[i].transform.position += new Vector3(-1, 0, 0) * velocidad * Time.deltaTime;

                if (rayos[i].transform.position.x <= -10)
                {
                    Destroy(rayos[i]);
                    rayos.RemoveAt(i);
                    SpawnRayo();
                }
            }
        }
    }

    private void SpawnRayos(int count)
    {
        for (int i = 0; i < count; i++)
        {
            float randomObs = Random.Range(10, 18);
            GameObject newRayo = null;

            int randomIndex = Random.Range(0, 3);
            switch (randomIndex)
            {
                case 0:
                    newRayo = Instantiate(rasho1, new Vector3(randomObs, 1, 0), Quaternion.identity);
                    break;
                case 1:
                    newRayo = Instantiate(rasho2, new Vector3(randomObs, 1, 0), Quaternion.identity);
                    break;
                case 2:
                    newRayo = Instantiate(rasho3, new Vector3(randomObs, 1, 0), Quaternion.identity);
                    break;
                default:
                    break;
            }

            if (newRayo != null)
            {
                rayos.Add(newRayo);
            }
        }
    }

    private void SpawnRayo()
    {
        float randomObs = Random.Range(10, 18);
        GameObject newRayo = null;

        int randomIndex = Random.Range(0, 3);
        switch (randomIndex)
        {
            case 0:
                newRayo = Instantiate(rasho1, new Vector3(randomObs, 1, 0), Quaternion.identity);
                break;
            case 1:
                newRayo = Instantiate(rasho2, new Vector3(randomObs, 1, 0), Quaternion.identity);
                break;
            case 2:
                newRayo = Instantiate(rasho3, new Vector3(randomObs, 1, 0), Quaternion.identity);
                break;
            default:
                break;
        }

        if (newRayo != null)
        {
            rayos.Add(newRayo);
        }
    }

    public void SetGameOver()
    {
        gameOver = true;
        energyBarController.gameOver = true; // Actualiza la variable gameOver en EnergyBarController
        scoreManager.enabled = false; // Desactiva el ScoreManager para detener la actualización del puntaje
    }
}
