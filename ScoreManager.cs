using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ScoreManager : MonoBehaviour
{
    private float puntos;
    private TextMeshProUGUI textMesh;

    private void Start(){
        textMesh = GetComponent<TextMeshProUGUI>();

    }
    private void Update(){
        
        puntos += Time.deltaTime;
        textMesh.text = puntos.ToString("0");

    } 
}
