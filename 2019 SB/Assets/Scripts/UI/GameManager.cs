using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public enum CharacterState
{
    idle,
    walk,
    interact,
    attack,
    combat,
    stagger,
    dash
}

public class GameManager : MonoBehaviour
{
    [Header("Game Variables")]
    public GameManager gm;
    public static GameManager instance = null;
    public GameObject player;
    public GameObject follower;
    public CharacterState currentState;
    //public FloatValue currentHealth;
    public VectorValue playerPosition;
    public Message updateCoins;
    public Message updateHealth;
    public Message updateEnergy;
    public FloatValue currentHP;
    public FloatValue hpBar;
    public FloatValue currentEnergy;
    public FloatValue energyBar;
    public FloatValue currentLevel;
    public LevelSystem levelSystem;
    public MonsterInventory ownedMonsters;

    [Header("Pause System")]
    public FloatValue GameSpeed;

    [Header("Scriptable Objects")]
    public List<ScriptableObject> objects = new List<ScriptableObject>();


    [Header("Camera Variables")]
    public Name lastActiveChar;
    public GameObject currentActiveChar;
    public CinemachineVirtualCamera followCam;

    [Header("Transition Variables")]
    public GameObject fadeInPanel;
    public GameObject fadeOutPanel;
    public float fadeWait;

    private void OnEnable()
    {
       LoadScriptables();
    }

    private void OnDisable()
    {
       // SaveScriptables();
    }



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        SceneManager.activeSceneChanged += OnSceneLoaded;
    }

    private void Start()
    {
        GameSpeed.RuntimeValue = GameSpeed.initialValue;

        if (follower != null)
        {
            UpdateHP();
            currentHP.RuntimeValue = currentHP.initialValue;
            updateHealth.Raise();
            UpdateEnergy();
            currentEnergy.RuntimeValue = currentEnergy.initialValue;
            updateEnergy.Raise();
        }
    }

    public void UpdateHP()
    {
        currentHP.initialValue = follower.GetComponent<MonsterStats>().HPStat;
        if (currentHP.RuntimeValue > currentHP.initialValue)
        {
            currentHP.RuntimeValue = currentHP.initialValue;
        }
        hpBar.initialValue = currentHP.initialValue;
    }

    public void UpdateEnergy()
    {
        currentEnergy.initialValue = (follower.GetComponent<MonsterStats>().WillpowerStat * 2) * 10;
        if (currentEnergy.RuntimeValue > currentEnergy.initialValue)
        {
            currentEnergy.RuntimeValue = currentEnergy.initialValue;
        }
        energyBar.initialValue = currentEnergy.initialValue;
    }

    void OnSceneLoaded(Scene previousScene, Scene newScene)
    {
        player = GameObject.FindWithTag("Player");
        follower = GameObject.FindWithTag("Follower");
        gm = this;

        if (player != null)
        {
            player.transform.position = playerPosition.initialValue;
        }
        if (follower != null)
        {
            follower.transform.position = playerPosition.initialValue;
        }

        if (gm != null)
        {
            Invoke("UpdateUI", 0.1f);

        }

    }

    public void UpdatePosition()
    {
        playerPosition.initialValue = player.transform.position;
    }

    public void UpdateUI()
    {
        currentActiveChar = GameObject.FindWithTag(lastActiveChar.initialName);
        levelSystem = GameObject.FindWithTag("LevelSystem").GetComponent<LevelSystem>();
        updateCoins.Raise();

        if (follower != null)
        {
            UpdateHP();
            currentHP.RuntimeValue = currentHP.initialValue;
            updateHealth.Raise();
            UpdateEnergy();
            currentEnergy.RuntimeValue = currentEnergy.initialValue;
            updateEnergy.Raise();
        }
    }

    public void UpdateCam()
    {
        followCam = GameObject.FindGameObjectWithTag("VCam").GetComponent<CinemachineVirtualCamera>();
        if (currentActiveChar != null)
        {
            followCam.Follow = currentActiveChar.transform;
        }
    }

    private void Update()
    {
        Invoke("UpdateCam", 0.3f);

        if (lastActiveChar.initialName == "Follower")
        {
            currentActiveChar = follower;
        }

        if (currentActiveChar != null)
        {
            if (currentActiveChar == player)
            {
                currentState = currentActiveChar.GetComponent<PlayerMain>().currentState;
            }
            else if (currentActiveChar == follower)
            {
                currentState = currentActiveChar.GetComponent<MonsterMain>().currentState;
            }
        }

        if (player.GetComponent<PlayerMain>().currentState != CharacterState.combat)
        {
            if (Input.GetKeyDown(KeyCode.E) && (currentState == CharacterState.walk || currentState == CharacterState.idle))
            {
                Swap(lastActiveChar.initialName);
            }
        }
    }

    public void Swap(string Name)
    {
        if (follower != null && GameSpeed.RuntimeValue != 0)
        {
            if (Name == "Player") //when E pressed swap PC between player/follower
            {
                lastActiveChar.initialName = "Follower";
                currentActiveChar = follower;



            }
            else if (Name == "Follower")
            {
                lastActiveChar.initialName = "Player";
                currentActiveChar = player;
            }
        }
    }

    public IEnumerator FadeCo()
    {
        if (fadeOutPanel != null)
        {
            Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity);
        }
        yield return new WaitForSeconds(fadeWait);
        yield return null;
    }

    public void SaveScriptables()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            FileStream file = File.Create(Application.persistentDataPath + string.Format("/{0}.dat", i));
            BinaryFormatter binary = new BinaryFormatter();
            var json = JsonUtility.ToJson(objects[i]);
            binary.Serialize(file, json);
            file.Close();
        }
    }

    public void LoadScriptables()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            if (File.Exists(Application.persistentDataPath + string.Format("/{0}.dat", i)))
            {
                FileStream file = File.Open(Application.persistentDataPath + string.Format("/{0}.dat", i), FileMode.Open);
                BinaryFormatter binary = new BinaryFormatter();
                JsonUtility.FromJsonOverwrite((string)binary.Deserialize(file), objects[i]);
                file.Close();
            }
        }
    }

    public void ResetScriptables()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            if (File.Exists(Application.persistentDataPath + string.Format("/{0}.dat", i)))
            {
                File.Delete(Application.persistentDataPath + string.Format("/{0}.dat", i));
            }
        }
    }



}
