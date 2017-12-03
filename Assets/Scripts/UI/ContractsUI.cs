using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContractsUI : MonoBehaviour
{
    public static ContractsUI instance;
    [SerializeField]
    DogProfile profile_prefab;

    [System.Serializable]
    public class DogContract
    {
        public string name, tagline;
        public Sprite m_pic;
        public Dog m_dog;
        public int pay, minDistance;
        public bool m_walkMe;
    }

    public List<DogContract> m_contracts = new List<DogContract>();

    public void ToggleDog(int index)
    {
        m_contracts[index].m_walkMe = !m_contracts[index].m_walkMe;
    }

    void Awake()
    {
        instance = this;
        //I guess init the contracts

        for (int i = 0; i < m_contracts.Count; i++)
        {
            if (m_contracts[i].m_dog)
            {
                Instantiate(profile_prefab, transform).Init(m_contracts[i], i);
            }
        }

        DontDestroyOnLoad(gameObject);
    }

    [SerializeField]
    DogHUD m_DogHUDPrefab;
    [SerializeField]
    Transform doghudParent;

    public void WALK_DOGS()
    {
        foreach (var item in m_contracts)
        {
            if (item.m_walkMe)
            {
                item.m_dog.gameObject.SetActive(true);
                item.m_dog.m_owner = Player.instance.gameObject;
                Instantiate(m_DogHUDPrefab, doghudParent).Init(item.name, item.minDistance);
            }
        }
        Destroy(GameObject.Find("music menu"));
        GameStateManager.instance.ChangeStategames(GameStateManager.GameSate.Gameplay);
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
}
