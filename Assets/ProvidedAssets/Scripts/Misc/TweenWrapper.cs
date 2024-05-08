using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenWrapper
{
    public enum TweenType
    {
        Scale,
        Move
    }

    public static void ScaleBySpeed(GameObject gameObject, Vector3? from, Vector3 to, float speed, iTween.EaseType easeType = iTween.EaseType.linear)
    {
        if (from != null)
            gameObject.transform.localScale = (Vector3)from;

        iTween.ScaleTo(gameObject,
            iTween.Hash(
                "x", to.x, "y", to.y, "z", to.z,
                "easeType", easeType.ToString(),
                "speed", speed
                )
            );
    }

    public static void PositionByTime(GameObject gameObject, Vector3? from, Vector3 to, float time, iTween.EaseType easeType = iTween.EaseType.linear)
    {
        if (from != null)
            gameObject.transform.position = (Vector3)from;

        iTween.MoveTo(gameObject,
            iTween.Hash(
                "x", to.x, "y", to.y, "z", to.z,
                "easeType", easeType.ToString(),
                "time", time
                )
            );
    }

    public static void Stop(GameObject gameObject, TweenType type)
    {
        iTween.Stop(gameObject, type.ToString());
    }
}

