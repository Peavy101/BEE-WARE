using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypewriterText : MonoBehaviour
{

    public float delay = 0.1f; 
    public string fullText;
    public string fullTextTwo;
    private string currentText = "";

    void Start()
    {
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {

        float completedCount = 0;
        float totalCount = 45;

        for(int i = 0; i < fullText.Length+1; i++)
        {
            currentText = fullText.Substring(0, i);
            this.GetComponent<Text>().text = currentText;
            completedCount++;
            if(completedCount == totalCount)
            {
                StartCoroutine(TextTwo());
            }
            yield return new WaitForSeconds(delay);
            
        }

    }

    IEnumerator TextTwo()
    {
        

        yield return new WaitForSeconds(2);

        currentText = "";
        float completedCount = 0;
        float totalCount = 26;


        for(int i = 0; i < fullTextTwo.Length+1; i++)
        {
            currentText = fullTextTwo.Substring(0, i);
            this.GetComponent<Text>().text = currentText;
            completedCount++;
            if(completedCount == totalCount)
            {
                yield return new WaitForSeconds(2);
                FindObjectOfType<DestroyTextBox>().ByeTextBox();
                Destroy(gameObject);
            }
            yield return new WaitForSeconds(delay);
            
        }


    }



}
