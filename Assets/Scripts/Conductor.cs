using System;
using UnityEngine;

public class Conductor : MonoBehaviour
{
    [Header("Music")]
    public AudioSource musicSource;

    [Header("Song Settings")]
    public float bpm = 120f;
    public int beatsPerMeasure = 4;

    [Header("Runtime Info")]
    public float secondsPerBeat;
    public float songPosition;
    public int songPositionInBeats;

    private double songStartTime;
    private int lastReportedBeat = -1;

    public static event Action<int> OnBeat;

    private void Awake()
    {
        secondsPerBeat = 60f / bpm;
    }

    public bool IsSongPlaying()
    {
        return musicSource.isPlaying;
    }

    public void StopSong()
    {
        musicSource.Stop();
    }

    public void StartSong()
    {
        songStartTime = AudioSettings.dspTime + 0.5f;

        musicSource.PlayScheduled(songStartTime);
    }

    private void Update()
    {
        if (!musicSource.isPlaying)
            return;

        songPosition =
            (float)(AudioSettings.dspTime - songStartTime);

        songPositionInBeats =
            Mathf.FloorToInt(songPosition / secondsPerBeat);

        if (songPositionInBeats > lastReportedBeat)
        {
            lastReportedBeat = songPositionInBeats;

            Debug.Log("BEAT: " + songPositionInBeats);

            OnBeat?.Invoke(songPositionInBeats);
        }
    }
}
