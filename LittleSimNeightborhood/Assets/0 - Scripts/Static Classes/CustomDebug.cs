using UnityEngine;

namespace Custom.Debug
{
    public class DebugCustom : MonoBehaviour
    {
        public static void DrawSquare(Vector2 pPosition, float pScale, Color pColor, float pDuration = 0.001f)
        {
            Vector2 t_rightBot = pPosition + new Vector2(pScale / 2f, -(pScale / 2f));
            Vector2 t_rightTop = t_rightBot + new Vector2(0f, pScale);
            Vector2 t_leftTop = t_rightTop - new Vector2(pScale, 0f);
            Vector2 t_leftBot = t_leftTop - new Vector2(0f, pScale);

            UnityEngine.Debug.DrawLine(t_rightBot, t_rightTop, pColor, pDuration);
            UnityEngine.Debug.DrawLine(t_rightTop, t_leftTop, pColor, pDuration);
            UnityEngine.Debug.DrawLine(t_leftTop, t_leftBot, pColor, pDuration);
            UnityEngine.Debug.DrawLine(t_leftBot, t_rightBot, pColor, pDuration);
        }
    }
}
