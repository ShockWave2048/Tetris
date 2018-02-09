using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ObjectToCanvasFitter : MonoBehaviour
{
    public GameObject target;
    public Canvas frame;
    public GameObject test;
    public Text trText;

    private void Update()
    {
        Camera c = Camera.main;
        Event e = Event.current;

        if (e!=null)
        {
            if (trText != null)
            {
                trText.text = e.mousePosition.x.ToString() + ":" + e.mousePosition.y.ToString();
            }

            var p = c.ScreenToWorldPoint(new Vector3(e.mousePosition.x, e.mousePosition.y, c.nearClipPlane));

            GUILayout.BeginArea(new Rect(20, 20, 250, 120));
            GUILayout.Label("Screen pixels: " + c.pixelWidth + ":" + c.pixelHeight);
            GUILayout.Label("Mouse position: " + e.mousePosition);
            GUILayout.Label("World position: " + p.ToString("F3"));
            GUILayout.EndArea();
        }
    }

    [ExecuteInEditMode]
    private void OnGUI()
    {
        if (target == null || frame == null) return;

        //var s = Screen.currentResolution;
        Camera c = Camera.main;
        Event e = Event.current;
        if (trText!=null)
        {
            trText.text = e.mousePosition.x.ToString() + ":" +e.mousePosition.y.ToString();
        }

        var p = c.ScreenToWorldPoint(new Vector3(e.mousePosition.x, e.mousePosition.y, c.nearClipPlane));

        GUILayout.BeginArea(new Rect(20, 20, 250, 120));
        GUILayout.Label("Screen pixels: " + c.pixelWidth + ":" + c.pixelHeight);
        GUILayout.Label("Mouse position: " + e.mousePosition);
        GUILayout.Label("World position: " + p.ToString("F3"));
        GUILayout.EndArea();

        var s = Camera.main.pixelRect;

        RectTransform rect = frame.GetComponent<RectTransform>(); // Target frame rect.

        Vector3 vec1 = new Vector3(rect.position.x + rect.sizeDelta.x / 2, c.pixelHeight - rect.position.y - rect.sizeDelta.y / 2, 0); // Frame center in pixels.

        Vector3 v3 = Camera.main.ScreenToWorldPoint(vec1);
        if (test!=null)test.transform.localPosition = v3;
        v3.z = target.transform.position.z;
        target.transform.localPosition = v3;

        Bounds b = new Bounds(new Vector3(), new Vector3(10, 20, 1));

        Vector3 vec0 = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        vec1 = Camera.main.ScreenToWorldPoint(new Vector3(rect.rect.width, 0, 0));

        float scrCanvasWidth = Vector3.Distance(vec0, vec1);

        vec1 = Camera.main.ScreenToWorldPoint(new Vector3(0, rect.rect.height, 0));

        float scrCanvasHeight = Vector3.Distance(vec0, vec1);

        float scale = Mathf.Min(scrCanvasWidth / (b.extents.x * 2), scrCanvasHeight / (b.extents.y * 2));

        target.transform.localScale = new Vector3(scale, scale, scale);

        //Debug.Log("## GO  positioned!");

        //Uti
        //Screen
    }
}
