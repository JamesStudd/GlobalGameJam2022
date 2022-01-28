using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayback : MonoBehaviour
{
    bool isRec = false;
    bool doPlay = false;
    List<float> nums = new List<float>();
    float tempX;
    float tempY;
    float tempZ;
    bool playedNoRep = false;
    public Vector3 startPosi;
    // Use this for initialization
    void Start()
    {

    }

    [ContextMenu("Record")]
    public void Record()
    {
        if (isRec == true)
        {
            isRec = false;
            transform.position = startPosi;
        }
        else if (isRec == false)
        {
            startPosi = transform.position;
            isRec = true;
            doPlay = false;
        }
    }

    public void Update()
    {
        if (playedNoRep == true)
        {
            doPlay = false;
        }

        if (isRec == true)
        {
            tempX = transform.position.x;
            tempY = transform.position.y;
            tempZ = transform.position.z;
            nums.Add(tempX);
            nums.Add(tempY);
            nums.Add(tempZ);
        }

        if (doPlay == true)
        {
            doPlay = false;
            StartCoroutine("Playback");
            Debug.Log(doPlay);
        }
    }

    public IEnumerator Playback()
    {
        playedNoRep = true;
        for (int i = 0; i < nums.Count; i += 3)
        {
            transform.position = new Vector3(nums[i], nums[i + 1], nums[i + 2]);
            yield return null;
        }
    }
    [ContextMenu("Reset")]
    public void Reset()
    {
        nums.Clear();
    }
    [ContextMenu("Play")]
    public void Play()
    {
        isRec = false;
        doPlay = true;
        StartCoroutine("Playback");
    }
}
