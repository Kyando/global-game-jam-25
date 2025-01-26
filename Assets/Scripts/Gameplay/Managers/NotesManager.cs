using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using static UnityEngine.InputSystem.InputAction;

public class NotesManager : MonoBehaviour
{
    public static NotesManager instance;
    public GameObject notePrefab;
    public Transform spawnPoint;
    public Transform hitPoint; // The hit area position
    public Transform destroyPoint; // Point where notes are destroyed
    public float noteTravelTime = 2f; // Time it takes for a note to travel from spawn to hit area
    public GameObject musicPlayerParent;

    public float perfectHitThreshold = 0.1f;
    public float greatHitThreshold = 0.5f;

    private Dictionary<PlayerType, MusicVolumeController> playerMusicMap =
        new Dictionary<PlayerType, MusicVolumeController>();

    [SerializeField] private float songTime; // Current time in the song
    [SerializeField] private Queue<float> beatTimes = new Queue<float>(); // Store beat times (in seconds)
    [SerializeField] private List<MusicNote> notes = new List<MusicNote>();
    private AudioSource audioSource; // Reference to the music audio source

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        notePrefab = GameDataManager.instance.gameDataSo.notePrefab;
        audioSource = musicPlayerParent.transform.GetChild(0).GetComponent<AudioSource>();
        LoadBeatMap();
        for (int i = 0; i < musicPlayerParent.transform.childCount; i++)
        {
            var childTransform = musicPlayerParent.transform.GetChild(i);
            var childAudioSource = childTransform.GetComponent<AudioSource>();
            childAudioSource.Play();

            var musicVolumeController = childTransform.GetComponent<MusicVolumeController>();
            if (musicVolumeController is not null)
            {
                playerMusicMap.Add(musicVolumeController.playerType, musicVolumeController);
            }
        }
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
            SpriteRenderer newNoteSpriteRenderer =
                newNote.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
            int noteIndex = Random.Range(0, 4);
            int zRotation = noteIndex * 90;

            newNote.noteId = noteIndex;
            newNote.transform.localEulerAngles = new Vector3(0, 0, zRotation);
            newNoteSpriteRenderer.color = GameDataManager.instance.GetColorByIndex(noteIndex);
            notes.Add(newNote.GetComponent<MusicNote>());
            newNote.beatTime = beatTimes.Peek();
            beatTimes.Dequeue();
        }
    }

    private void MoveNotes()
    {
        List<MusicNote> notesToRemove = new List<MusicNote>();


        var currentPlayerType = PlayerType.PLAYER_ONE;
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
            if (!noteToRemove.isNotePlayed)
            {
                UIManager.Instance.OnNoteMiss();
                playerMusicMap[currentPlayerType].targetVolume = 0;
            }

            notes.Remove(noteToRemove);
            notesToRemove.RemoveAt(0);
            Destroy(noteToRemove.gameObject);
        }

        var nextNote = GetNextNote();
        if (nextNote is not null)
        {
            nextNote.transform.localScale = new Vector3(1.25f, 1.25f, 1);
        }
    }

    private void LoadBeatMap()
    {
        // Load beat times from a predefined beat map (seconds for each beat)
        // Example: 1s, 2s, 3s, etc. These should be based on your song's rhythm
        float[] beats =
        {
            2.4f,
            3.2f,
            3.6f,
            4.0f,
            4.4f,
            9.6f,
            10.4f,
            11.2f,
            11.6f,
            12.0f,
            12.4f,
            12.8f,
            13.4f,
            13.6f,
            14.0f,
            14.4f,
            14.8f,
            15.2f,
            15.6f,
            16.0f,
            16.8f,
            17.6f,
            18.4f,
            19.2f,
            19.8f,
            20.0f,
            20.4f,
            20.8f,
            21.2f,
            21.6f,
            22.0f,
            22.4f,
            22.6f,
            22.8f,
            23.0f,
            23.2f,
            23.4f,
            23.6f,
            23.8f,
            24.0f,
            24.4f,
            24.8f,
            25.2f,
            25.6f,
            26.0f,
            26.4f,
            26.8f,
            27.2f,
            27.6f,
            28.0f,
            28.4f,
            28.8f,
            29.0f,
            29.2f,
            29.4f,
            29.6f,
            29.8f,
            30.0f,
            30.2f,
            30.4f,
            30.8f,
            31.2f,
            31.6f,
            32.0f,
            32.2f,
            32.4f,
            32.6f,
            32.8f,
            33.0f,
            33.2f,
            33.4f,
            33.6f,
            34.0f,
            34.4f,
            34.8f,
            35.2f,
            35.4f,
            35.6f,
            35.8f,
            36.0f,
            36.2f,
            36.4f,
            36.6f,
            36.8f,
            37.2f,
            37.6f,
            38.0f,
            38.4f,
            38.8f,
            39.2f,
            39.6f,
            40.0f,
            40.4f,
            40.8f,
            41.0f,
            41.2f,
            41.4f,
            42.0f,
            42.8f,
            43.6f,
            44.4f,
            45.2f,
            46.0f,
            46.8f,
            47.6f,
            48.0f,
            48.4f,
            48.8f,
            49.2f,
            49.6f,
            50.0f,
            50.4f,
            50.8f,
            51.2f,
            51.6f,
            52.0f,
            52.4f,
            52.8f,
            53.2f,
            53.6f,
            54.0f,
            55.2f,
            55.4f,
            55.6f,
            55.8f,
            56.8f,
            57.0f,
            57.2f,
            57.4f,
            57.6f,
            58.0f,
            58.4f,
            58.8f,
            59.2f,
            59.6f,
            60.0f,
            60.4f,
            61.6f,
            61.8f,
            62.0f,
            62.2f,
            63.2f,
            63.4f,
            63.6f,
            63.8f,
            64.0f,
            64.4f,
            64.8f,
            65.2f,
            65.6f,
            66.4f,
            67.2f,
            67.8f,
            68.0f,
            68.4f,
            68.8f,
            69.2f,
            69.6f,
            70.0f,
            70.4f,
            71.2f,
            72.0f,
            72.8f,
            73.6f,
            74.2f,
            74.4f,
            74.8f,
            75.2f,
            75.6f,
            76.0f,
            76.4f,
            76.8f,
            77.0f,
            77.2f,
            77.4f,
            77.6f,
            77.8f,
            78.0f,
            78.2f,
            78.4f,
            78.8f,
            79.2f,
            79.6f,
            80.0f,
            80.4f,
            80.8f,
            81.2f,
            81.6f,
            82.0f,
            82.4f,
            82.8f,
            83.2f,
            83.4f,
            83.6f,
            83.8f,
            84.0f,
            84.2f,
            84.4f,
            84.6f,
            84.8f,
            85.0f,
            85.2f,
            85.4f,
            85.6f,
            85.8f,
            86.0f,
            86.2f,
            86.4f,
            86.6f,
            86.8f,
            87.0f,
            87.2f,
            87.4f,
            87.6f,
            87.8f,
            88.0f,
            88.2f,
            88.4f,
            88.6f,
            88.8f,
            89.0f,
            89.2f,
            89.4f,
            89.6f,
            89.8f,
            90.0f,
            90.2f,
            90.4f,
            90.6f,
            90.8f,
            91.0f,
            91.2f,
            91.4f,
            91.6f,
            91.8f,
            92.0f,
            92.2f,
            92.4f,
            92.6f,
            92.8f,
            93.2f,
            93.6f,
            94.0f,
            94.4f,
            94.8f,
            95.2f,
            95.6f,
            96.0f,
            97.6f,
            99.2f,
            100.8f,
            102.4f,
            104.0f,
            105.6f,
            107.2f,
            108.0f,
            109.6f,
            109.8f,
            110.0f,
            110.2f,
            111.2f,
            111.4f,
            111.6f,
            111.8f,
            112.0f,
            112.4f,
            112.8f,
            113.2f,
            113.6f,
            114.0f,
            114.4f,
            114.8f,
            116.0f,
            116.2f,
            116.4f,
            116.6f,
            117.6f,
            117.8f,
            118.0f,
            118.2f,
            118.4f,
            118.8f,
            119.2f,
            119.6f,
            120.0f,
            120.4f,
            120.8f,
            121.2f,
            121.6f,
            122.2f,
            122.4f,
            122.8f,
            123.2f,
            123.6f,
            124.0f,
            124.4f,
            124.8f,
            125.6f,
            126.4f,
            127.2f,
            128.0f,
            128.6f,
            128.8f,
            129.2f,
            129.6f,
            130.0f,
            130.4f,
            130.8f,
            131.2f,
            131.4f,
            131.6f,
            131.8f,
            132.0f,
            132.2f,
            132.4f,
            132.6f,
            132.8f,
            133.2f,
            133.6f,
            134.0f,
            134.4f,
            134.8f,
            135.2f,
            135.6f,
            136.0f,
            136.4f,
            136.8f,
            137.2f,
            137.6f,
            138.0f,
            138.4f,
            138.8f,
            139.2f,
            139.6f,
            140.0f,
            140.4f,
            140.8f,
            141.2f,
            141.6f,
            142.0f,
            142.4f,
            142.8f,
            143.2f,
            143.6f,
            144.0f,
            144.2f,
            144.4f,
            144.6f,
            144.8f,
            145.0f,
            145.2f,
            145.4f,
            145.6f,
            145.8f,
            146.0f,
            146.2f,
            146.4f,
            146.6f,
            146.8f,
            147.0f,
            148.8f,
            149.2f,
            149.6f,
            150.0f,
            150.4f,
            150.8f,
            151.2f,
            151.6f,
            152.0f,
        };

        // float[] beats = { 1f,}; // Replace with your beat map data
        foreach (var beat in beats)
        {
            beatTimes.Enqueue(beat);
        }
    }

    public void OnNotePressed(int noteId, PlayerType playerType)
    {
        MusicNote nextNote = GetNextNote();

        if (nextNote is null || nextNote.isNotePlayed)
        {
            return;
        }

        // float notePosX = nextNote.transform.position.x;
        // float hitPosX = hitPoint.position.x;
        //
        //
        // float beatTime = nextNote.beatTime;
        // float currentBeatDiff = Mathf.Abs(songTime - beatTime);
        //
        // // float dist = Mathf.Abs(notePosX - hitPosX);
        // float dist = currentBeatDiff;
        //
        // if (dist > greatHitThreshold)
        // {
        //     UIManager.Instance.OnNoteMiss();
        //     return;
        // }

        
        nextNote.OnNotePlayed();
        if (nextNote.noteId == noteId)
        {
            UIManager.Instance.OnNoteHit();
            playerMusicMap[playerType].targetVolume = 1;
        }
        else
        {
            UIManager.Instance.OnNoteMiss();
            playerMusicMap[playerType].targetVolume = 0;
        }
    }

    private MusicNote GetNextNote()
    {
        var raycastHit2D = Physics2D.Raycast(hitPoint.transform.position, Vector2.zero);
        if (raycastHit2D)
        {
            var note = raycastHit2D.collider.gameObject.GetComponent<MusicNote>();
            return note;
        }
        
        return null;
    }

    public void OnPlayerNotePressed(CallbackContext context)
    {
        var value = context.ReadValue<float>();
        // Debug.Log("OnPlayerNotePressed");
        // Debug.Log(value);
    }
}