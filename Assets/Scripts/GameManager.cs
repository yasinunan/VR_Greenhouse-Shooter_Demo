using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager instance { 
        get {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                
            }
            return _instance;
        } 
    }
    public int totalScore { get; private set; }
    private int harmfulScore;
    private int beneficialScore;

    public GameObject baseBug;
    public Text totalScoreText;
    public Text harmfulScoreText;
    public Text beneficialScoreText;
    public Text finishScore;

    public GameObject scoreUI;
    public GameObject scoreCanvas;
    public GameObject startUI;
    public GameObject finishUI;
    public GameObject numpadUI;
    public GameObject canvasAuthUI;
 
    public float timer = 30.0f;

    public BugData[] bugsData;
    private BugController bugController;

    bool isRight = false;
    //[HideInInspector]
    public bool isFinished = false;

    public bool isPlaying = false;

    public Text[] bugNames;
    public Text[] bugTypes;
    public Text[] bugPoints;
    public Image[] bugImages;

    public XRInteractorLineVisual XRInteractorLineVisual;

    public GameObject killedEffect;


    public void StartGame()
    {
        //timer = 30;
        Time.timeScale = 1;
        SetScore(0, true);
        InvokeRepeating("SpawnBugs", 1f, 1f);
        
    }

    private void Update()
    {


        if (isPlaying)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                CleanScene();
                Time.timeScale = 0;
                FinishUIActive();
                
                //event getdata
            }
        }

    }

    private void BugsInformation()
    {
        
        for (int i = 0; i < bugsData.Length; i++)
        {
            bugNames[i].text = bugsData[i].bugName;

            if (bugsData[i].isHarmful)
            {
                bugTypes[i].text = "Harmful";
            }
            else
            {
                bugTypes[i].text = "Beneficial";
            }

            bugPoints[i].text = bugsData[i].score.ToString();

            bugImages[i].sprite = bugsData[i].sprite;
            
        }
    }
    

    private void Awake()
    {

        NumpadUIActive();

    }

    public void StartUIActive()
    {
        BugsInformation();
        startUI.SetActive(true);
        scoreUI.SetActive(false);
        isPlaying = false;

        XRInteractorLineVisual.lineLength = 2;

    }
    public void StartUIDeactive()
    {
        startUI.SetActive(false);
        scoreUI.SetActive(true);
        isPlaying = true;

        XRInteractorLineVisual.lineLength = 0;


    }

    private void FinishUIActive()
    {

        finishUI.SetActive(true);
        scoreUI.SetActive(false);
        isPlaying = false;
        isFinished = true;

        FirebaseManager.instance.GetData();
        XRInteractorLineVisual.lineLength = 2;
    }

    public void FinishUIDeactive()
    {
        finishUI.SetActive(false);
        scoreUI.SetActive(true);
        isPlaying = true;

        XRInteractorLineVisual.lineLength = 2;
    }

    public void NumpadUIDeactive()
    {
        
        numpadUI.SetActive(false);

    }
    public void NumpadUIActive()
    {

        numpadUI.SetActive(true);
        XRInteractorLineVisual.lineLength = 2;

    }

    public void CanvasAuthUIDeactive()
    {
        
        canvasAuthUI.SetActive(false);

    }
    public void CanvasAuthUIActive()
    {
        
        canvasAuthUI.SetActive(true);
        XRInteractorLineVisual.lineLength = 2;

    }


    private void Start()
    {
        
        
    }

    

    private void CleanScene()
    {
        GameObject bullet = GameObject.Find("Bullet(Clone)");

        if (bullet != null)
        {
            Destroy(bullet);
        }


    }

    public void Restart()
    {

        isFinished = false;
        SceneManager.LoadScene(0);
    }

    

    public void SetScore(int score , bool isHarmful)
    {
        this.totalScore += score;
        totalScoreText.text = this.totalScore.ToString();
        finishScore.text = this.totalScore.ToString();

        if (isHarmful)
        {
            harmfulScore += score;
            harmfulScoreText.text = this.harmfulScore.ToString();
        }
        else
        {
            beneficialScore += score;
            beneficialScoreText.text = this.beneficialScore.ToString();
        }
    }
    

    private void SpawnBugs()
    {
        if (isPlaying)
        {

            BugData bugData = bugsData[Random.Range(0, bugsData.Length)];

            int randomway = Random.Range(0, 2);
            if (randomway == 0)
                isRight = true;
            else
                isRight = false;

            var xValue = isRight ? 2f : -2f;
            var movementWay = isRight ? Vector3.right : Vector3.left;

            Vector3 randomSpawnPoint = new Vector3(xValue, (bugData.movementType == MovementType.Jump ? 1f : Random.Range(1f, 2f)), Random.Range(1f, 4f));
            GameObject spawnedBug = Instantiate(baseBug, randomSpawnPoint, Quaternion.Euler(movementWay));

            spawnedBug.GetComponent<BugController>().Initialize(bugData ,isRight);
        }

    }
    public void ScoreUI(GameObject shotBug, bool isHarmful , int score)
    {

        Vector3 scoreUIPos = shotBug.transform.position + new Vector3(0, .5f, 0);
        GameObject newScoreCanvas = Instantiate(scoreCanvas, scoreUIPos, Quaternion.identity);
        newScoreCanvas.GetComponent<ScoreUI>().SetScoreUIAnimation(isHarmful, score);
    }
}
