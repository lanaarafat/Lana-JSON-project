using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Collections;

[Serializable]
public class WorkoutDetails
{
    public int ballId;
    public float speed;
    public Vector3 ballDirection;
}

[Serializable]
public class WorkoutInfo
{
    public int workoutID;
    public string workoutName;
    public string description;
    public string ballType;
    public List<WorkoutDetails> workoutDetails;
}

[Serializable]
public class WorkoutData
{
    public string ProjectName;
    public int numberOfWorkoutBalls;
    public List<WorkoutInfo> workoutInfo;
}

public class JSONHandler : MonoBehaviour
{
    private string fileName = "WorkoutInfoJSONAssignment.json";
    public GameObject buttonPrefab;
    public Transform buttonContainer;
    public Transform ballContainer;
    public GameObject ballPrefab;
    public TextMeshProUGUI descriptionTextBox;
    public TextMeshProUGUI titleText;
    public Button playPauseButton;

    private WorkoutData workoutData;
    private List<GameObject> allButtons = new List<GameObject>();
    private Coroutine currentSpawnCoroutine;
    private bool isSpawning = false;

    private void Start()
    {
        LoadWorkoutData();
        GenerateButtons();
        SetTitleText();
        InitializePlayPauseButton();
    }

    private void LoadWorkoutData()
    {
        string filePath = Path.Combine(Application.dataPath, fileName);
        string data = File.ReadAllText(filePath);
        workoutData = JsonUtility.FromJson<WorkoutData>(data);
    }

    private void SetTitleText()
    {
        titleText.text = workoutData.ProjectName;
    }

    private void GenerateButtons()
    {
        foreach (var workout in workoutData.workoutInfo)
        {
            GameObject button = Instantiate(buttonPrefab, buttonContainer);
            TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = workout.workoutName;
            allButtons.Add(button);

            Button buttonComponent = button.GetComponent<Button>();
            buttonComponent.onClick.AddListener(() => ShowDescription(workout.description));
            buttonComponent.onClick.AddListener(() => StartWorkout(workout));
        }
    }

    private void ShowDescription(string description)
    {
        descriptionTextBox.text = description;
    }

    private void StartWorkout(WorkoutInfo workout)
    {
        if (isSpawning)
        {
            StopCoroutine(currentSpawnCoroutine);
            isSpawning = false;
        }
        playPauseButton.GetComponentInChildren<TextMeshProUGUI>().text = "Play";

        playPauseButton.onClick.RemoveAllListeners();
        playPauseButton.onClick.AddListener(() => ToggleSpawning(workout));
    }

    private void InitializePlayPauseButton()
    {
        playPauseButton.onClick.AddListener(() => Debug.Log("Select a workout first"));
    }

    private void ToggleSpawning(WorkoutInfo workout)
    {
        if (isSpawning)
        {
            StopCoroutine(currentSpawnCoroutine);
            isSpawning = false;
            playPauseButton.GetComponentInChildren<TextMeshProUGUI>().text = "Play";
        }
        else
        {
            currentSpawnCoroutine = StartCoroutine(SpawnBallsCoroutine(workout.workoutDetails, workout.ballType));
            isSpawning = true;
            playPauseButton.GetComponentInChildren<TextMeshProUGUI>().text = "Pause";
        }
    }

    private IEnumerator SpawnBallsCoroutine(List<WorkoutDetails> workoutDetails, string ballType)
    {
        foreach (var detail in workoutDetails)
        {
            GameObject ball = Instantiate(ballPrefab, GetSpawnPosition(detail.ballDirection), Quaternion.identity, ballContainer);
            ball.name = ballType;

            Ball ballComponent = ball.GetComponent<Ball>();
            if (ballComponent != null)
            {
                ballComponent.ballId = detail.ballId;
                ballComponent.MoveBall(detail.ballDirection, detail.speed);
            }

            yield return new WaitForSeconds(2f);
        }

        isSpawning = false;
        playPauseButton.GetComponentInChildren<TextMeshProUGUI>().text = "Play";
    }

    private Vector3 GetSpawnPosition(Vector3 direction)
    {
        return new Vector3(direction.x, 0.5f, direction.z);
    }
}
