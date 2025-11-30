using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Vector3 CamOffset = new Vector3(0, 4f, -6f); 
    UIManager MyUIManager;

    public GameObject BallPrefab;   // prefab of Ball

    // Constants for SetupBalls
    public static Vector3 StartPosition = new Vector3(0, 0, -6.35f);
    public static Quaternion StartRotation = Quaternion.Euler(0, 90, 90);
    const float BallRadius = 0.286f;
    const float RowSpacing = 0.02f;

    GameObject PlayerBall;
    GameObject CamObj;

    const float CamSpeed = 3f;

    const float MinPower = 15f;
    const float PowerCoef = 1f;

    void Awake()
    {
        // PlayerBall, CamObj, MyUIManager를 얻어온다.
        // ---------- TODO ----------
        PlayerBall = GameObject.Find("PlayerBall");
        CamObj = GameObject.Find("Main Camera");
        MyUIManager = FindObjectOfType<UIManager>();
        // --------------------
    }

    void Start()
    {
        SetupBalls();
    }

    // Update is called once per frame
    void Update()
    {
        // 좌클릭시 raycast하여 클릭 위치로 ShootBallTo 한다.
        // ---------- TODO ----------
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                ShootBallTo(hit.point);
            }
        }
        // --------------------
    }

    void LateUpdate()
    {
        CamMove();
    }

    void SetupBalls()
    {
        int ballIndex = 1;

        for (int row = 0; row < 5; row++)
        {
            for (int i = 0; i <= row; i++)
            {
                float x = (i - row * 0.5f) * (BallRadius * 2 + RowSpacing);

                float z = row * (BallRadius * Mathf.Sqrt(3) + RowSpacing);

                Vector3 pos = StartPosition + new Vector3(x, 0, z);

                GameObject ball = Instantiate(BallPrefab, pos, StartRotation);
                ball.name = ballIndex.ToString();

                Material mat = Resources.Load<Material>($"Materials/ball_{ballIndex}");
                ball.GetComponent<MeshRenderer>().material = mat;

                ballIndex++;
            }
        }
    }


    void CamMove()
    {
        if (CamObj != null && PlayerBall != null)
        {
            Vector3 targetPos = PlayerBall.transform.position + CamOffset;

            CamObj.transform.position = Vector3.Lerp(
                CamObj.transform.position,
                targetPos,
                Time.deltaTime * CamSpeed
            );

            CamObj.transform.LookAt(PlayerBall.transform);
        }
    }

    float CalcPower(Vector3 displacement)
    {
        return MinPower + displacement.magnitude * PowerCoef;
    }

    void ShootBallTo(Vector3 targetPos)
    {
        // targetPos의 위치로 공을 발사한다.
        // 힘은 CalcPower 함수로 계산하고, y축 방향 힘은 0으로 한다.
        // ForceMode.Impulse를 사용한다.
        // ---------- TODO ----------

        if (PlayerBall == null) return;

        Rigidbody rb = PlayerBall.GetComponent<Rigidbody>();
        if (rb == null) return;

        Vector3 dir = (targetPos - PlayerBall.transform.position);
        dir.y = 0;                         // y축 힘 제거
        float power = CalcPower(dir);

        rb.AddForce(dir.normalized * power, ForceMode.Impulse);

        // --------------------
    }

    // When ball falls
    public void Fall(string ballName)
    {
        // "{ballName} falls"을 1초간 띄운다.
        // ---------- TODO ----------

        StartCoroutine(ShowFallMessage(ballName));

        // --------------------
    }

    IEnumerator ShowFallMessage(string ballName)
    {
        MyUIManager.DisplayText($"{ballName} falls", 1f);
        yield return new WaitForSeconds(1f);
        MyUIManager.DisplayText("", 0f);
    }
}
