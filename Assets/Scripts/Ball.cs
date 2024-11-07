using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public int ballId;
    private Vector3 direction;
    private float speed;

    public void MoveBall(Vector3 ballDirection, float ballSpeed)
    {
        direction = ballDirection;
        speed = ballSpeed;
        StartCoroutine(MoveCoroutine());
    }

    private IEnumerator MoveCoroutine()
    {
        while (true)
        {
            transform.Translate(direction * speed * Time.deltaTime);
            yield return null;
        }
    }
}
