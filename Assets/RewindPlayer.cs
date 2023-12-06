using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindPlayer : MonoBehaviour
{
    public bool isRewinding;
    List<RecordedFrame> recordedFrames;
    public Animator animator;

    // Structure to store position, rotation, and animation data
    private struct RecordedFrame
    {
        public Vector3 position;
        public Quaternion rotation;
        public Dictionary<string, bool> animationParameters; // Animation state

        public RecordedFrame(Vector3 pos, Quaternion rot, Animator animator)
        {
            position = pos;
            rotation = rot;
            animationParameters = new Dictionary<string, bool>();

            // Record all boolean parameters from the animator
            var parameters = animator.parameters;
            foreach (var param in parameters)
            {
                if (param.type == AnimatorControllerParameterType.Bool)
                {
                    animationParameters[param.name] = animator.GetBool(param.name);
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        recordedFrames = new List<RecordedFrame>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartRewind();
        }
        if (Input.GetKeyUp(KeyCode.Return))
        {
            StopRewind();
        }
    }

    void FixedUpdate()
    {
        if (isRewinding && recordedFrames.Count > 0)
        {
            Rewind();
        }
        else
        {
            Record();
        }

        // Update animation state
        foreach (var param in recordedFrames.Count > 0 ? recordedFrames[0].animationParameters : new Dictionary<string, bool>())
        {
            Debug.Log(param.Key + 
            param.Value);
            animator.SetBool(param.Key, param.Value);
        }
    }

    void Record()
    {
        recordedFrames.Insert(0, new RecordedFrame(transform.position, transform.rotation, animator));
    }

    void Rewind()
    {
        RecordedFrame frame = recordedFrames[0];
        transform.position = frame.position;
        transform.rotation = frame.rotation;

        foreach (var param in frame.animationParameters)
        {
            animator.SetBool(param.Key, param.Value);
        }

        recordedFrames.RemoveAt(0);
    }

    void StartRewind()
    {
        isRewinding = true;
    }

    void StopRewind()
    {
        isRewinding = false;
    }
}