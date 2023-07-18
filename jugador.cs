using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jugador : MonoBehaviour
{
    public GameManager gameManager;
    public float fuerzaSalto;
    private bool enSuelo = true;
    private Animator animator;
    private new Rigidbody2D rigidbody2D;
    public EnergyBarController energyBar;

    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (gameManager.start)
        {
            if (enSuelo && Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetBool("estaSaltando", true);
                rigidbody2D.AddForce(new Vector2(0, fuerzaSalto));
                enSuelo = false;
            }
        }

        if (gameManager.gameOver)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("piso"))
        {
            animator.SetBool("estaSaltando", false);
            enSuelo = true;
        }

        if (collision.gameObject.CompareTag("Obstaculo"))
        {
            gameManager.gameOver = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Rayo"))
        {
            Destroy(collision.gameObject);
            energyBar.currentEnergy = energyBar.maxEnergy;
        }
    }
}
