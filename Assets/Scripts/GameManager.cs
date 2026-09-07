using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int totalKoin;
    private int koinTerkumpul = 0;
    private bool sudahMenang = false;

    public Text pesanMenang;

    void Awake()
    {
        if (FindObjectsOfType<GameManager>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        totalKoin = GameObject.FindGameObjectsWithTag("Coin").Length;
        SetupPesanMenang();
    }

    public void AmbilKoin()
    {
        if (sudahMenang)
            return;

        koinTerkumpul++;
        totalKoin = Mathf.Max(totalKoin - 1, 0);

        if (totalKoin == 0)
        {
            Menang();
        }
    }

    void Menang()
    {
        if (sudahMenang)
            return;

        sudahMenang = true;
        Debug.Log("KAMU MENANG!");

        if (pesanMenang != null)
        {
            pesanMenang.text = "KAMU MENANG!";
            pesanMenang.gameObject.SetActive(true);
        }
    }

    void SetupPesanMenang()
    {
        if (pesanMenang != null)
        {
            pesanMenang.text = string.Empty;
            pesanMenang.gameObject.SetActive(false);
            return;
        }

        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas == null)
        {
            GameObject canvasGO = new GameObject("Canvas");
            canvas = canvasGO.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasGO.AddComponent<CanvasScaler>();
            canvasGO.AddComponent<GraphicRaycaster>();
        }

        GameObject textGO = new GameObject("PesanMenang");
        textGO.transform.SetParent(canvas.transform, false);
        pesanMenang = textGO.AddComponent<Text>();
        pesanMenang.alignment = TextAnchor.MiddleCenter;
        pesanMenang.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        pesanMenang.fontSize = 48;
        pesanMenang.color = Color.yellow;
        pesanMenang.text = string.Empty;
        pesanMenang.gameObject.SetActive(false);

        RectTransform rt = textGO.GetComponent<RectTransform>();
        rt.anchorMin = new Vector2(0, 0);
        rt.anchorMax = new Vector2(1, 1);
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;
    }
}
