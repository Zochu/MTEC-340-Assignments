using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    [SerializeField] float xBound = 10.0f;
    [SerializeField] float yBound = 4.7f;
    [SerializeField] Vector2 velocity;
    [SerializeField] AudioClip Death;
    [SerializeField] AudioClip WallHit;
    [SerializeField] AudioClip PaddleHit;
    [SerializeField] float pitchInc;
    [SerializeField] PaddleBehavior pbL;
    [SerializeField] PaddleBehavior pbR;
    [SerializeField] float rotate;

    // Start is called before the first frame update
    void Start()
    {
        ResetBall();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameBehavior.Instance.CurrentState == State.Play)
        {
            transform.position += new Vector3(
                velocity.x, velocity.y, 0) * Time.deltaTime;
            transform.Rotate(0.0f, 0.0f, rotate);

            if (Mathf.Abs(transform.position.y) > yBound)
            {
                velocity.y *= -1;
                transform.position = new Vector3(
                    transform.position.x,
                    transform.position.y > 0 ? yBound - 0.01f : -yBound + 0.01f,
                    transform.position.z
                );

                AudioBehavior.Instance.PlaySound(WallHit, 0.5f);
            }

            if (Mathf.Abs(transform.position.x) > xBound)
            {
                AudioBehavior.Instance.PlaySound(Death, 0.5f);
                GameBehavior.Instance.UpdateScore(transform.position.x > 0 ? 0 : 1);
                ResetBall();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            velocity.x *= -1;

            velocity.x += velocity.x > 0 ?
                GameBehavior.Instance.VelocityIncrement :
                GameBehavior.Instance.VelocityIncrement * -1;

            velocity.y += velocity.y > 0 ?
                GameBehavior.Instance.VelocityIncrement :
                GameBehavior.Instance.VelocityIncrement * -1;
            pbL.velocity += GameBehavior.Instance.VelocityIncrement;
            pbR.velocity += GameBehavior.Instance.VelocityIncrement;

            rotate += GameBehavior.Instance.VelocityIncrement * 0.2f;


            AudioBehavior.Instance.PlaySound(PaddleHit, 0.5f);
            AudioBehavior.Instance.Source.pitch += pitchInc;
        }
    }

    void ResetBall()
    {
        transform.position = new Vector3(0, 0, 0);
        velocity = new Vector2(
            GameBehavior.Instance.InitVelocity,
            GameBehavior.Instance.InitVelocity
        );

        velocity.x *= Random.Range(0.0f, 1.0f) >= 0.5f ? 1 : -1;
        velocity.y *= Random.Range(0.0f, 1.0f) >= 0.5f ? 1 : -1;
        //PaddleBehavior.velocity = 5.0f;
        pbL.velocity = 5.0f;
        pbR.velocity = 5.0f;
        AudioBehavior.Instance.Source.pitch = 1;
        
        rotate = 0.0f;
    }
}
