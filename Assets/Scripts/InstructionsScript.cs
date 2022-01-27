using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InstructionsScript : MonoBehaviour
{
    private GameObject star;
    [SerializeField]
    private Text firstInstructionText;
    [SerializeField]
    private Text secondInstructionText;
    [SerializeField]
    private Text thirdInstructionText;
    [SerializeField]
    private Text easterEggText;

    [SerializeField]
    private GameObject tutorialLine;
    private int easterEgg = 0;
    private bool stopLoop = false;

    [SerializeField] private GameObject lineRendererPrefab;
    public GameObject line;
    public LineRenderer lr;
    public GameObject linePool;

    public bool instructionsActive = false;

    // Start is called before the first frame update
    void Start()
    {
        star = GameObject.Find("Star (0)"); //Asigning star so that it doesn't have to be done manually
        
        //Setting all the instruction texts to invisible excepting the first one
        secondInstructionText.enabled = false;
        thirdInstructionText.enabled = false;
        easterEggText.enabled = false;
        instructionsActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(star.GetComponent<Star>().isValid == true && stopLoop == false) //If the first instruction is followed
        {
            //Hiding the instruction that was completed
            firstInstructionText.enabled = false;
            //Showing next instruction
            secondInstructionText.enabled = true;
            
            tutorialLine = GameObject.Find("LineRenderer(Clone)");

            stopLoop = true;

            easterEgg++;
            //Check if the tutorial line has been deleted 4 times and the user was about to delete it 5 times
            if (easterEgg > 4)
            {
                easterEggText.enabled = true;           //Show EasterEgg text
                secondInstructionText.enabled = false;  //Hide instruction

                //Instantiate randomly 50 lines and generate their colliders
                for (int i = 0; i < 50; i++)
                {
                    line = Instantiate(lineRendererPrefab);
                    lr = line.GetComponent<LineRenderer>();
                    lr.positionCount = 2;
                    line.transform.SetParent(linePool.transform);
                    lr.SetPosition(0, new Vector3(Random.Range(-9f, 9f), Random.Range(-3f, 3f), 0f));
                    lr.SetPosition(1, new Vector3(Random.Range(-9f, 9f), Random.Range(-3f, 3f), 0f));
                    //Since the line collider is not connected to any star the xDisplace can be null
                    lr.gameObject.GetComponent<LineCollision>().GenerateCollider(lr, 0f);
                }
            }
        }

        if (tutorialLine == null)
        {
            //Hiding the instruction that was completed
            secondInstructionText.enabled = false;
            //Showing next instruction
            thirdInstructionText.enabled = true;

            stopLoop = false;
        }

    }
}
