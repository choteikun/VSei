using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom;
using SonicBloom.Koreo;

public class TitleScaler : MonoBehaviour
{
    [EventID]
    public string eventID;

    public float minScale;
    public float maxScale;

    void Start()
    {
        //註冊Koreography事件。
        Koreographer.Instance.RegisterForEventsWithTime(eventID, AdjustScale);
    }
    void OnDestroy()
    {
        //有時候Koreographer Instance會提前清理。
        if (Koreographer.Instance != null)
        {
            //取消所有註冊的事件
            Koreographer.Instance.UnregisterForAllEvents(this);
        }
    }
    void AdjustScale(KoreographyEvent evt, int sampleTime, int sampleDelta, DeltaSlice deltaSlice)
    {
        if (evt.HasCurvePayload())//如果數據是SonicBloom.Koreo.CurvePayload類型
        {
            //獲取當前音頻位置的曲線值。 這將是一個[0, 1] 之間的值，將在下面用於插值

            float curveValue = evt.GetValueOfCurveAtTime(sampleTime);

            transform.localScale = Vector2.one * Mathf.Lerp(minScale, maxScale, curveValue);
        }
    }
}
