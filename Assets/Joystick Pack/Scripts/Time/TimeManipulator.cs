using UnityEngine;
using UnityEngine.UI;

public class TimeManipulator : MonoBehaviour
{
    [Header("��������� ���������")]
    public float initialTimeScale = 1f;    // ��������� �������� (1 = ����������)
    public float increaseInterval = 3f;    // ��������� ������ 3 �������
    public float increaseAmount = 0.5f;    // ��������� �� 10%
    public float maxTimeScale = 3f;        // ������������ ��������� (3x)

    [Header("����� ��������")]
    public Text speedText;

    private float lastIncreaseTime;

    private void Start()
    {
        Time.timeScale = initialTimeScale;
        lastIncreaseTime = Time.time;
        UpdateSpeedUI();
    }

    private void Update()
    {
        if (Time.time - lastIncreaseTime >= increaseInterval)
        {
            IncreaseSpeed();
            lastIncreaseTime = Time.time;
        }
    }

    private void IncreaseSpeed()
    {
        Time.timeScale += increaseAmount;
        Time.timeScale = Mathf.Min(Time.timeScale, maxTimeScale); // ����������� 
        UpdateSpeedUI();
    }

    private void UpdateSpeedUI()
    {
        if (speedText != null)
        {
            speedText.text = $"��������: {Time.timeScale:F1}x";
        }
    }
    public void SetPause(bool pause)
    {
        Time.timeScale = pause ? 0f : initialTimeScale;
        UpdateSpeedUI();
    }
}