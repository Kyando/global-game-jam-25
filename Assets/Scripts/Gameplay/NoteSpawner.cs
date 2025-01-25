using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NoteSpawner : MonoBehaviour
{
    public Transform spawnPoint;
    public Transform destroyPoint;
    public float noteSpeed = 5.0f;
    public List<GameObject> notes;

    [SerializeField] private float noteSpawnTimer = 1.0f;
    [SerializeField] private float spawnTimeCounter = 0f;

    private GameObject notePrefab = null;


    void Start()
    {
        notePrefab = GameDataManager.instance.gameDataSo.notePrefab;
        spawnTimeCounter = noteSpawnTimer;
    }

    void Update()
    {
        MoveNotes();
        spawnTimeCounter -= Time.deltaTime;

        if (spawnTimeCounter <= 0)
        {
            spawnTimeCounter = noteSpawnTimer;
            SpawnNote();
        }
    }

    private void MoveNotes()
    {
        List<GameObject> notesToRemove = new List<GameObject>();
        foreach (var note in notes)
        {
            note.transform.position += Vector3.left * (noteSpeed * Time.deltaTime);

            if (note.transform.position.x <= destroyPoint.position.x)
            {
                notesToRemove.Add(note);
            }
        }

        while (notesToRemove.Count > 0)
        {
            var noteToRemove = notesToRemove[0];
            notes.Remove(noteToRemove);
            notesToRemove.RemoveAt(0);
            Destroy(noteToRemove);
        }
    }

    private void SpawnNote()
    {
        var newNote = Instantiate(notePrefab, spawnPoint.position, Quaternion.identity);
        SpriteRenderer newNoteSpriteRenderer = newNote.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        int noteIndex = Random.Range(0, 4);
        int zRotation = noteIndex * 90;

        newNote.transform.localEulerAngles = new Vector3(0, 0, zRotation);
        newNoteSpriteRenderer.color = GameDataManager.instance.GetColorByIndex(noteIndex);

        notes.Add(newNote);
    }
}