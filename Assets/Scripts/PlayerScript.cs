using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    //Instanciate line
    public LineRenderer lineRend;

    //Instanciate mouse positions needed
    public Vector2 mousePos;
    public Vector2 starPos;

    //Bool to check if a line is being created
    private bool isConnecting = false;

    //All the lines created will be stored in the line GameObject variable
    private GameObject line;
    private LineRenderer lr; //The line renderer from the lines will be stored here
    
    //Particle effects variables
    private GameObject particleEffect; //General one
    public GameObject connectedParticleEffect; //first effect
    public GameObject grabParticleEffect;      //second effect

    //Declare Star class created at Star.cs script
    private Star selectedStar;
    //Declare Line class created at Line.cs script
    private Line lineScript;

    //Win detection list
    [SerializeField]
    public List<int> validatedStars = new List<int>();

    //Boolean for level completion
    [SerializeField]
    private bool isCompleted;

    //Instantiate linePool (where lines are held) and the lineRendererPrefab (which will be the model used to draw lines)
    [SerializeField] private GameObject linePool;
    [SerializeField] private GameObject lineRendererPrefab;
    [SerializeField] private GameObject particlePool;

    //Declare variables needed for the camera shake when a line is released in the void
    public float camShakeAmt = 0.1f;
    CameraShakeManager camShake;
    public static PlayerScript instance;

    private int easterEggCompletionCounter = 0;

    private void Awake()
    {
        camShake = gameObject.GetComponent<CameraShakeManager>(); //Get script from camera
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isCompleted == false) // Detect the first click
        {
            //Instantiate current mouse position
            Vector2 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //Create RayCast2D
            RaycastHit2D hit = Physics2D.Raycast(currentMousePosition, Vector2.zero);

            if (hit.collider && hit.collider.CompareTag("Star") && isConnecting == false) //Detect collisions with stars
            {
                AudioManager.instance.Play("Star_Grab"); //Play sound

                isConnecting = true;

                // Instantiate Line
                line = Instantiate(lineRendererPrefab);
                lr = line.GetComponent<LineRenderer>();
                lr.positionCount = 2;
                lr.SetPosition(0, new Vector3(hit.collider.gameObject.transform.position.x , hit.collider.gameObject.transform.position.y, 0f));
                
                //Instanciate effects
                particleEffect = Instantiate(grabParticleEffect, hit.collider.gameObject.transform.position, Quaternion.identity);
                particleEffect.transform.SetParent(particlePool.transform);

                //Organise lines
                line.transform.SetParent(linePool.transform);
                
                //Get the star script to later id getting
                selectedStar = hit.collider.gameObject.GetComponent<Star>();

                //Add the first star clicked to the line's list of the two stars it is connecting
                lineScript = line.GetComponent<Line>();
                lineScript.AddStarId(selectedStar);

            }
            else if (hit.collider && hit.collider.CompareTag("Line"))
            {
                //Check if a line clicked must be deleted
                StartCoroutine(Cr(hit, currentMousePosition));
            }
        }

        if (Input.GetMouseButton(0)) // Detect if mouse button is clicked
        {
            //Update current mouse position every frame when mouse clicked
            Vector2 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (isConnecting) //Make sure the line is only updated when the mouse hasn't been released
            {
                //Update line position
                lr.SetPosition(1, new Vector3(currentMousePosition.x, currentMousePosition.y, 0f));
            }
        }

        if (Input.GetMouseButtonUp(0)) // Detect if mouse button is released
        {
            if (isConnecting == true)  //Make sure the line is only created if a line was being created
            {
                //Instantiate current mouse position
                Vector2 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                
                //Create RayCast2D
                RaycastHit2D hit = Physics2D.Raycast(currentMousePosition, Vector2.zero);

                if (hit.collider) //Ensure that the line's second position is not in the void
                {
                    //Get script from the second star connected
                    Star hitStar = hit.collider.gameObject.GetComponent<Star>();

                    //Detect if line was released in a star and the star was different from the origin star
                    if (hit.collider.CompareTag("Star") && selectedStar.id != hitStar.id)
                    {
                        //Make sure that there are no duplicated superposed lines in the constellation
                        if (LineAlreadyExisting(selectedStar, hitStar, line.GetComponent<Line>()) == false)
                        {
                            // Fix line's last position
                            AudioManager.instance.PlayWithRandomPitch("Correct_Sound", 0.95f, 1.05f);
                            lr.SetPosition(1, new Vector3(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.y, 0f));

                            //Instanciate effect
                            particleEffect = Instantiate(connectedParticleEffect, hit.collider.gameObject.transform.position, Quaternion.identity);

                            //Generate line collider
                            lr.gameObject.GetComponent<LineCollision>().GenerateCollider(lr, 0.2f);

                            //Organise particle effects
                            particleEffect.transform.SetParent(particlePool.transform);

                            //Add the second star id to the line's list
                            lineScript.AddStarId(hitStar);

                            // Adding the first star's id to the last star's list
                            hitStar.connectedStars.Add(selectedStar.id);
                            hitStar.Evaluate();

                            // Adding the last star's id to the first star's list
                            selectedStar.connectedStars.Add(hitStar.id);
                            selectedStar.Evaluate();

                            //Check if the constellation is completed
                            isCompleted = CheckCompletion();
                        }

                        else
                        {
                            //If the line already exists
                            AudioManager.instance.Play("Wrong_Sound"); //Play sound
                            Destroy(line);                             //Destroy line
                            camShake.Shake(camShakeAmt, 0.2f);         //Shake scene
                        }
                    }

                    else 
                    {
                        AudioManager.instance.Play("Wrong_Sound");
                        Destroy(line);
                        camShake.Shake(camShakeAmt, 0.2f);
                    }
                }

                else //Detect if line was released in the void and destroy the line created
                {
                    AudioManager.instance.Play("Wrong_Sound");  //Play sound
                    Destroy(line);                              //Destroy line
                    camShake.Shake(camShakeAmt, 0.2f);          //Shake scene
                }

                //End line creation process
                isConnecting = false;
            }
        }

        //Check if the constellation is complete
        if (isCompleted == true)
        {
            gameObject.GetComponent<MySceneManager>().NextSceneCall("LevelComplete");
            isCompleted = false;
        }
    }

    IEnumerator Cr(RaycastHit2D hit, Vector2 currentMousePosition) //Check if a line clicked must be deleted
    {

        //new YieldInstruction that waits for 0.5 seconds
        yield return new WaitForSeconds(0.5f);

        //Update mouse position to the one after 0.5 seconds
        currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Send ray at mouse position
        hit = Physics2D.Raycast(currentMousePosition, Vector2.zero);

        if (hit.collider == null)   //If mouse is in the void cancel line deletion
        {
            yield break;
        }

        if (Input.GetMouseButton(0) && hit.collider && hit.collider.CompareTag("Line")) //If line hit and mouse button still pressed
        {
            lineScript = hit.collider.GetComponent<Line>(); //Get the script of the line which is about to be deleted 

            if (lineScript.lineConnections.Count == 0)
            {
                //Delete the line and play sound
                line = hit.collider.gameObject;
                AudioManager.instance.PlayWithRandomPitch("Line_Deleted", 0.9f, 1.1f);
                Destroy(line);
                easterEggCompletionCounter++;

                if (easterEggCompletionCounter > 49)
                {
                    gameObject.GetComponent<MySceneManager>().NextSceneCall("EasterEgg");
                }
            }

            else { 
                //Delete the stars connected from the opposite stars' lists
                int connectedStarId1;
                int connectedStarId2;

                connectedStarId1 = lineScript.lineConnections[0];
                connectedStarId2 = lineScript.lineConnections[1];

                GameObject parent = GameObject.Find("Stars");
                Star[] childScripts = parent.GetComponentsInChildren<Star>();

                for (int i = 0; i < childScripts.Length; i++)
                {
                    Star myChildStar = childScripts[i];
                    if (myChildStar.id == connectedStarId1)
                    {
                        myChildStar.connectedStars.Remove(connectedStarId2);
                        myChildStar.Evaluate();
                    }
                    else if (myChildStar.id == connectedStarId2)
                    {
                        myChildStar.connectedStars.Remove(connectedStarId1);
                        myChildStar.Evaluate();
                    }
                }

                //Delete the line and play sound
                line = hit.collider.gameObject;
                AudioManager.instance.PlayWithRandomPitch("Line_Deleted", 0.9f, 1.1f);
                Destroy(line);
            }

            //Check if the constellation is complete
            isCompleted = CheckCompletion();
        }

    }

    public bool LineAlreadyExisting(Star firstStar, Star lastStar, Line actualLine) //Check if the line already exists
    {

        //Loop through the Line Pool searching for a line with the same connections as the one about to be created
        GameObject parent = GameObject.Find("Line Pool");
        Line[] childScripts = parent.GetComponentsInChildren<Line>();

        for (int i = 0; i < childScripts.Length; i++)
        {
            Line myChildLine = childScripts[i];

            if (myChildLine == actualLine)
            {
                break;
            }
            
            if (myChildLine.lineConnections[0] == firstStar.id)
            {
                if (myChildLine.lineConnections[1] == lastStar.id)
                {
                    return true;
                }
            }
            else if (myChildLine.lineConnections[0] == lastStar.id)
            {
                if (myChildLine.lineConnections[1] == firstStar.id)
                {
                    return true;
                }
            }
        }

        //If not found return that the line doesn't exist yet
        return false;
    }

    public bool CheckCompletion() //Check if constellation is completed
    {
        //Loop through all the stars checking if the stars connected are the expected to be connected
        GameObject parent = GameObject.Find("Stars");
        Star[] childScripts = parent.GetComponentsInChildren<Star>();

        for (int i = 0; i < childScripts.Length; i++)
        {
            Star myChildScript = childScripts[i];
            if (myChildScript.isValid == false)
            {
                return false;
            }
        }
        return true;
    }
}
