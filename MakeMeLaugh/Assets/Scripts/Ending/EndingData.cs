// エンディングシーン用に値を保持するやつ
using UnityEngine;

public class EndingData : MonoBehaviour
{
    public enum EndingType
    {
        BAD = 0,
        NORMAL,
        GOOD
    }

    private static EndingData _instance = null;
    private static EndingType _endingType = EndingType.BAD;

    public static EndingData Instance
    {
        get { return _instance; }
    }

    public void SetEndingType(EndingType endingType)
    {
        _endingType = endingType;
    }

    public EndingType GetEndingType()
    {
        return _endingType;
    }

    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
