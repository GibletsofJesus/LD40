using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dog : MonoBehaviour
{
    public static int total;
    [Header("Settings")]
    public bool isActuallyAPerson = true;
    public AudioClip m_barkSound;
    public float m_speed;
	public float m_RunSpeed = 500f;
    public float m_attentionSpan;
    public float m_alertRadius;
    public string m_name;
    float offset = 0;
    public DogState m_state = DogState.following;

	public enum DogState
	{
		waiting,
		following,
		running,
		snoofing
	}

    [Header("References")]
    public LineRenderer m_lead;
    public Rigidbody2D m_rigidbody;
    public DistanceJoint2D m_joint;
    public PointMaker m_route;
    public GameObject m_owner;
    public DogHUD m_hud;
    //airhorns
    [SerializeField]
    Transform targetthing;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (m_owner)
        {
            m_joint = m_owner.AddComponent<DistanceJoint2D>();
            m_joint.maxDistanceOnly = true;
            m_joint.autoConfigureDistance = false;
            m_joint.distance = Player.m_leadLength;
            m_joint.connectedBody = m_rigidbody;

            m_attentionSpan = Random.Range(1.5f, 15);

            float r = Random.Range(0.75f, 1.5f);
            transform.localScale = Vector3.one * r;
            m_rigidbody.mass *= r;
            m_speed *= r;
            {
                offset = Random.value * 6;
            }
        }
        prevPos = transform.position;
    }

    void Update()
    {
        if (GameStateManager.instance.m_currentState == GameStateManager.GameSate.Gameplay)
        {
            if (!isActuallyAPerson)
            {
                if (m_owner.tag == "Player")
                    m_joint.distance = Player.m_leadLength;
                else
                    m_joint.distance = 2.5f;
                Vector3[] newPos =
                    {
                        transform.position,
                        (transform.position + m_owner.transform.position) / 2,
                        m_owner.transform.position
                    };
                m_lead.SetPositions(newPos);
                if (m_hud)
                {
                    m_hud.UpdateBar(Mathf.Abs((transform.position - prevPos).magnitude));
                }
            }
            Movement();
        }
    }

    Vector3 prevPos;
    Vector3 targetPos;
	public float m_fSnoofRadius = 2.0f;
	float m_fNextSnoofTime = -1;
	float m_fSnoofTimeMin = 1f;
	float m_fSnoofTimeMax = 6f;
	Vector3 m_SnoofTarget;
	public bool m_SnoofAlongPath = false;

	void Movement()
	{
		Vector3 target = Vector3.zero;
		float fSpeed = m_speed;

		switch (m_state)
		{
			case DogState.waiting:
				break;
			case DogState.following:
				target = m_route.WhereGo(transform.position, offset, !isActuallyAPerson);
				break;
			case DogState.running:
				// Currently just the same as following, but faster.
				target = m_route.WhereGo(transform.position, offset, !isActuallyAPerson);
				fSpeed = m_RunSpeed;
				break;
			case DogState.snoofing:
				// Keep on heading towards a snoof location until we've either gotten to it, or we've gone for a certain amount of time.
				// TODO: Allow the player to pull the pup away from his snoof target and cancel out of snoof mode.
				if (m_fNextSnoofTime <= Time.time || Vector3.Distance(target, transform.position) <= 0.5f) {
					Vector3 newTarget = (Random.insideUnitCircle * m_fSnoofRadius);
					m_SnoofTarget = newTarget + (m_SnoofAlongPath ? m_route.WhereGo(transform.position, offset, !isActuallyAPerson) : Player.instance.transform.position);
					m_fNextSnoofTime = Random.Range(m_fSnoofTimeMin, m_fSnoofTimeMax) + Time.time;
				}

				target = m_SnoofTarget;
				break;
		}

		targetthing.transform.position = target - Vector3.back * 10;

		float n = .0f;
		Vector3 noise = new Vector3(Random.Range(-n, n), Random.Range(-n, n), Random.Range(-n, n));
		m_rigidbody.AddForce((target - transform.position + noise).normalized * fSpeed);

		Vector3 diff = target - transform.position + noise;
		diff.Normalize();
		float z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.LerpUnclamped(transform.rotation, Quaternion.Euler(0, 0, z + 180), Time.deltaTime);
	}
}
