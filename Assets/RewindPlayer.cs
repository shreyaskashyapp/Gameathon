using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindPlayer : MonoBehaviour
{
    public bool isRewinding;
    List<RecordedFrame> recordedFrames;
    public Animator animator;
    public int rewindCount = 3;

    private struct RecordedFrame
    {
        public Vector3 position;
        public Quaternion rotation;
        public Dictionary<string, bool> animationParameters;

        public RecordedFrame(Vector3 pos, Quaternion rot, Animator animator)
        {
            position = pos;
            rotation = rot;
            animationParameters = new Dictionary<string, bool>();

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

    void Start()
    {
        recordedFrames = new List<RecordedFrame>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && rewindCount>0)
        {
            StartRewind();
            rewindCount= rewindCount -1;
            Debug.Log(rewindCount);
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

        foreach (var param in recordedFrames.Count > 0 ? recordedFrames[0].animationParameters : new Dictionary<string, bool>())
        {
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

        if (frame.animationParameters.ContainsKey("isJumping") && frame.animationParameters["isJumping"])
        {
            transform.position = Vector3.Lerp(transform.position, frame.position, Time.fixedDeltaTime * 5f);
        }
        else
        {
            transform.position = frame.position;
        }

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