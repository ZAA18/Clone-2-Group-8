using UnityEngine;
using UnityEngine.InputSystem;

public class BeatScroller : MonoBehaviour
{

    public float beatTempo; //How fast the arrows are going to be falling

    public bool hasStarted;

    public InputActionReference startGame;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        beatTempo = beatTempo / 60f;
    }

    // Update is called once per frame
    /*void Update()
    {
        if (!hasStarted)
        {
            if (Input.anyKeyDown)
            {
                hasStarted = true;
            }

            else
            {
                //120 beats per second - tempo
                transform.position -= new Vector3(0f, beatTempo * Time.deltaTime, 0f);
            }
        }
    }
    */

    void Update()
    {
        if (!hasStarted)
        {
            if (startGame.action.WasPressedThisFrame())
            {
                hasStarted = true;
            }
        }

        if (hasStarted)
        {
            transform.position -= new Vector3(0f, beatTempo * Time.deltaTime, 0f);
        }
    }

    private void OnEnable()
    {
        startGame.action.Enable();
    }

    private void OnDisable ()
    {
        startGame.action.Disable();
    }
}
