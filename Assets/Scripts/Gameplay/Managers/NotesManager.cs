using System.Collections.Generic;
using UnityEngine;

public class NotesManager : MonoBehaviour
{
    public GameObject notePrefab;
    public Transform spawnPoint;
    public Transform hitPoint; // The hit area position
    public Transform destroyPoint; // Point where notes are destroyed
    public float noteTravelTime = 2f; // Time it takes for a note to travel from spawn to hit area
    public GameObject musicPlayer;
    
    [SerializeField]private float songTime; // Current time in the song
    [SerializeField]private Queue<float> beatTimes = new Queue<float>(); // Store beat times (in seconds)
    [SerializeField]private List<MusicNote> notes = new List<MusicNote>();
    private AudioSource audioSource; // Reference to the music audio source

    void Start()
    {
        notePrefab = GameDataManager.instance.gameDataSo.notePrefab;
        audioSource = musicPlayer.GetComponent<AudioSource>();
        LoadBeatMap();
        audioSource.Play();
    }

    void Update()
    {
        songTime = audioSource.time; // Sync with the music
        SpawnNotes();
        MoveNotes();
    }

    private void SpawnNotes()
    {
        if (beatTimes.Count > 0 && songTime >= beatTimes.Peek() - noteTravelTime)
        {
            // Spawn a note for the next beat
            var newNoteObj = Instantiate(notePrefab, spawnPoint.position, Quaternion.identity);
            var newNote = newNoteObj.GetComponent<MusicNote>();
            notes.Add(newNote.GetComponent<MusicNote>());
            newNote.beatTime = beatTimes.Peek();
            beatTimes.Dequeue();
        }
    }

    private void MoveNotes()
    {
        List<MusicNote> notesToRemove = new List<MusicNote>();
        foreach (MusicNote note in notes)
        {
            
            // Calculate total distance from spawn to destroy point
            float totalDistance = Vector3.Distance(spawnPoint.position, destroyPoint.position);
            float distanceToHit = Vector3.Distance(spawnPoint.position, hitPoint.position);
            float totalTravelTime = (totalDistance / distanceToHit) * noteTravelTime;

            float elapsedTime = totalTravelTime - (note.beatTime - songTime);
            float progress = Mathf.Clamp01(elapsedTime / totalTravelTime);

            // Linearly interpolate from spawn point to destroy point
            note.transform.position = Vector3.Lerp(spawnPoint.position, destroyPoint.position, progress);

            // // Calculate the progress of the note based on song time
            // elapsedTime = noteTravelTime - (note.beatTime- songTime);
            // progress = Mathf.Clamp01(elapsedTime / noteTravelTime);
            //
            // // Linearly interpolate from spawn point to hit point
            // note.transform.position = Vector3.Lerp(spawnPoint.position, hitPoint.position, progress);

            // Destroy notes that pass the hit area
            if (progress >= 1f)
            {
                notesToRemove.Add(note);
            }
        }

        while (notesToRemove.Count > 0)
        {
            var noteToRemove = notesToRemove[0];
            notes.Remove(noteToRemove);
            notesToRemove.RemoveAt(0);
            Destroy(noteToRemove.gameObject);
        }
    }

    private void LoadBeatMap()
    {
        // Load beat times from a predefined beat map (seconds for each beat)
        // Example: 1s, 2s, 3s, etc. These should be based on your song's rhythm
        float[] beats = { 0.4f, 0.8f, 1.2f, 1.6f, 2.4f, 2.8f, 8f, 9f, }; // Replace with your beat map data
        // float[] beats = { 1f,}; // Replace with your beat map data
        foreach (var beat in beats)
        {
            beatTimes.Enqueue(beat);
        }
    }
}