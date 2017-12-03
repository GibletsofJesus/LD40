using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContractsUI : MonoBehaviour
{
    public static ContractsUI instance;

    [System.Serializable]
    public class DogContract
    {
        public string name;
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
        Debug.Log("init");
        //I guess init the contracts
        DontDestroyOnLoad(gameObject);
    }

    public void WALK_DOGS()
    {
        foreach (var item in m_contracts)
        {
            if (item.m_walkMe)
                item.m_dog.gameObject.SetActive(true);
        }
        Destroy(GameObject.Find("music menu"));
        GameStateManager.instance.ChangeStategames(GameStateManager.GameSate.Gameplay);
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
}
