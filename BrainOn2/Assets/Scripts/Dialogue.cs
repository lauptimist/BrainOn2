using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{

    public TMP_Text textComponent;
    public TMP_Text nameComponent;
    public string[] lines;
    public string[] lineNames;
    public float textSpeed;

    public GameObject cadre;
    public GameObject player;
    public GameObject enemy;

    private int index;


    // Start is called before the first frame update
    void Start()
    {

        textComponent.text = string.Empty;
        nameComponent.text = lineNames[index];
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        cadre.SetActive(true);
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
            // Type each character one by one
        foreach(char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if(index < lines.Length - 1)
        {
            index++;
            nameComponent.text = lineNames[index];
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
            cadre.SetActive(false);
            player.SetActive(false);
            enemy.SetActive(false);
        }
    }



}
