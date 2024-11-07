/* using System;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.UI;

[Serializable]
public class WorkoutDetails
{
    public int ballId;
    public float speed;
    public float ballDirection;
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
    public TextAsset jsonFile;
    public WorkoutData workoutData;
    public Text projectTitleText;
    public GameObject buttonPrefab;
    public Transform buttonContainer;
    public GameObject ballPrefab;
    public Transform spawnArea;
    void Start()
    {
        workoutData = JsonConvert.DeserializeObject<WorkoutData>(jsonFile.text);
        projectTitleText.text = workoutData.ProjectName;


        foreach (var workout in workoutData.workoutInfo)
        {
            GameObject button = Instantiate(buttonPrefab, buttonContainer);
            button.GetComponentInChildren<Text>().text = workout.workoutName + "\n" + workout.description;
            button.GetComponent<Button>().onClick.AddListener(() => SpawnBalls(workout.workoutDetails));
        }
    }

    void SpawnBalls(List<WorkoutDetails> workoutDetails)
    {
        foreach (var detail in workoutDetails)
        {
            GameObject ball = Instantiate(ballPrefab, spawnArea);

            // Map ball direction to specific positions
            Vector3 positionOffset;
            if (detail.ballDirection == 0.5f)
                positionOffset = new Vector3(0.5f, 0, 0);  // Right
            else if (detail.ballDirection == 0)
                positionOffset = new Vector3(0f, 0, 0);     // Center
            else if (detail.ballDirection == -0.5f)
                positionOffset = new Vector3(-0.5f, 0, 0);  // Left
            else
                positionOffset = new Vector3(0f, 0, 0);     // Default to center if value is unexpected

            // Set the ball's position relative to the spawn area
            ball.transform.localPosition = positionOffset;

            ball.GetComponent<Ball>().Initialize(detail.speed, detail.ballDirection);
        }
    }

}
*/

/* using Newtonsoft.Json;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

{
    public TextAsset jsonFile;
// public Text projectTitleText;
public GameObject buttonPrefab;
public TextMeshProUGUI projectNameText;
public Transform buttonContainer;
public Transform ballContainer;
public GameObject ballPrefab;

private WorkoutData workoutData;
private List<GameObject> allButtons = new List<GameObject>();

private void Start()
{
    LoadWorkoutData();
    GenerateButtons();
}

private void LoadWorkoutData()
{
    workoutData = JsonConvert.DeserializeObject<WorkoutData>(jsonFile.text);
    // projectTitleText.text = workoutData.ProjectName;
}

private void GenerateButtons()
{
    for (int i = 0; i < workoutData.workoutInfo.Count; i++)
    {
        WorkoutInfo workout = workoutData.workoutInfo[i];

        // Instantiate button and set its text
        GameObject button = Instantiate(buttonPrefab, buttonContainer);
        TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
        buttonText.text = $"{workout.workoutName}\n<size=12>{workout.description}</size>";
        allButtons.Add(button);

        // Add appropriate listener based on workout ID
        Button buttonComponent = button.GetComponent<Button>();
        switch (workout.workoutID)
        {
            case 1:
                buttonComponent.onClick.AddListener(() => SpawnRollingBalls(workout.workoutDetails));
                break;
            case 2:
                buttonComponent.onClick.AddListener(() => SpawnBouncingBalls(workout.workoutDetails));
                break;
            case 3:
                buttonComponent.onClick.AddListener(() => SpawnLineDriveBalls(workout.workoutDetails));
                break;
            case 4:
                buttonComponent.onClick.AddListener(() => SpawnRollingBalls(workout.workoutDetails));
                break;
            case 5:
            case 6:
                buttonComponent.onClick.AddListener(() => SpawnPopUpBalls(workout.workoutDetails));
                break;
        }
    }
}

private void SpawnBouncingBalls(List<WorkoutDetails> workoutDetails)
{
    SpawnBalls("bouncing ball", workoutDetails);
}

private void SpawnPopUpBalls(List<WorkoutDetails> workoutDetails)
{
    SpawnBalls("pop-up ball", workoutDetails);
}

private void SpawnLineDriveBalls(List<WorkoutDetails> workoutDetails)
{
    SpawnBalls("linedrive ball", workoutDetails);
}

private void SpawnRollingBalls(List<WorkoutDetails> workoutDetails)
{
    SpawnBalls("rolling ball", workoutDetails);
}

private void SpawnBalls(string ballType, List<WorkoutDetails> workoutDetails)
{
    foreach (WorkoutDetails detail in workoutDetails)
    {
        GameObject ball = Instantiate(ballPrefab, GetRandomPosition(), Quaternion.identity, ballContainer);
        ball.name = ballType;

        // Set ball properties
        var ballMoveComponent = ball.GetComponent<Ball>();
        if (ballMoveComponent != null)
        {
            ballMoveComponent.ballId = detail.ballId;
            ballMoveComponent.FireBall(detail.ballDirection, detail.speed);
        }
    }
}

private Vector3 GetRandomPosition()
{
    return new Vector3(UnityEngine.Random.insideUnitSphere.x, 0, UnityEngine.Random.insideUnitSphere.z);
}

}
*/