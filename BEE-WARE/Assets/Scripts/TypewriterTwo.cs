using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypewriterTwo : MonoBehaviour
{

    public float delay = 0.1f; 
    public string sceneText;
    public string sceneTextTwo;
    private string currentSceneText = "";
    bool barryShook = false;

    void Start()
    {
        if(!barryShook)
        {
            barryShook = true;
            StartCoroutine(FirstSceneText());
            FindObjectOfType<PlayerMovement>().BarryIsShaking();
        }
        else
        {
            FindObjectOfType<PlayerMovement>().BarryIsNotShaking();
        }
    }

    IEnumerator FirstSceneText()
    {

        float completedCount = 0;
        float totalCount = 29;

        for(int i = 0; i < sceneText.Length+1; i++)
        {
            currentSceneText = sceneText.Substring(0, i);
            this.GetComponent<Text>().text = currentSceneText;
            completedCount++;
            if(completedCount == totalCount)
            {
                StartCoroutine(SecondSceneText());
            }
            yield return new WaitForSeconds(delay);
            
        }

    }

    IEnumerator SecondSceneText()
    {
        

        yield return new WaitForSeconds(2);

        currentSceneText = "";
        float completedCount = 0;
        float totalCount = 89;


        for(int i = 0; i < sceneTextTwo.Length+1; i++)
        {
            currentSceneText = sceneTextTwo.Substring(0, i);
            this.GetComponent<Text>().text = currentSceneText;
            completedCount++;
            if(completedCount == totalCount)
            {
                yield return new WaitForSeconds(2);
                this.GetComponent<Text>().text = "";
                FindObjectOfType<DestroyTextBox>().ByeTextBox();
                FindObjectOfType<PlayerMovement>().BarryIsNotShaking();
                FindObjectOfType<HoneyQuakeParticles>().NoMoreQuake();
                FindObjectOfType<HoneyWallRise>().HoneyStart();
                Destroy(gameObject);
            }
            yield return new WaitForSeconds(delay);
            
        }





    }




}