using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HeighAnimationText : MonoBehaviour
{
    // Start is called before the first frame update
    public float heightPoint = 0;
    public int valorInicial = 0;
    public float intervaloDeTiempo = 1f;

    public TextMeshProUGUI textMesh;
    public Slider slider;

    private float velSlider;

    public GameObject panel;

    // lista de sprites
    public Sprite[] spritesCaquitas;

    public GameObject caquitaImage;
    private SpriteRenderer spriteRenderer;
    private int randomSprite;
    private float sizeCaquita;
    private void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        textMesh.text = valorInicial.ToString();
        slider.value = valorInicial;
        velSlider = slider.maxValue / 15;
        spriteRenderer = caquitaImage.GetComponent<SpriteRenderer>();
        randomSprite = Random.Range(0, spritesCaquitas.Length);
        Debug.Log("Random: " + randomSprite);
        spriteRenderer.sprite = spritesCaquitas[randomSprite];
        // sizeCaquita = heightPoint / slider.maxValue  * 10;
        // caquitaImage.transform.localScale = new Vector3(sizeCaquita, sizeCaquita, sizeCaquita);
    }

    private void AumentarNumero()
    {
        if(slider.value < slider.maxValue){
            if(slider.value < heightPoint){
                slider.value += 1f;
            }
        }
        if (valorInicial <= heightPoint)
        {
            textMesh.text = valorInicial.ToString();
            valorInicial++;
        }
        else
        {
            // Puedes detener la repetición aquí si se ha alcanzado el valor final
            // CancelInvoke("AumentarNumero");
        }
    }

    private void endGame (float score) {
        heightPoint = score;
        InvokeRepeating("AumentarNumero", 0f, intervaloDeTiempo);
        panel.GetComponent<Image>().color = new Color(0, 0, 0, 1f);
        return;
    }
}
