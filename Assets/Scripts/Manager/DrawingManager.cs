using UnityEngine;

public class DrawingManager : MonoBehaviour
{
    public Camera m_camera;
    public GameObject brush;

    LineRenderer currentLineRenderer;

    Vector2 lastPos;
    public WritingSoundManager writingSoundManager;

    // Add a reference to GamePlayManager
    public GamePlayManager gamePlayManager;

    private void Update()
    {
        Drawing();
    }

    void Drawing()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // Assuming you have a reference to a GamePlayManager instance called 'gamePlayManager'
            gamePlayManager.StartWriting();
            CreateBrush();
            lastPos = m_camera.ScreenToWorldPoint(Input.mousePosition);
        }
        else if (Input.GetKey(KeyCode.Mouse0))
        {
            PointToMousePos();
            writingSoundManager.PlayWritingSound();
        }
        else
        {
            currentLineRenderer = null;
            writingSoundManager.StopWritingSound();
        }
    }

    void CreateBrush()
    {
        GameObject brushInstance = Instantiate(brush);
        currentLineRenderer = brushInstance.GetComponent<LineRenderer>();

        Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);

        currentLineRenderer.SetPosition(0, mousePos);
        currentLineRenderer.SetPosition(1, mousePos);

    }

    void AddAPoint(Vector2 pointPos)
    {
        currentLineRenderer.positionCount++;
        int positionIndex = currentLineRenderer.positionCount - 1;
        currentLineRenderer.SetPosition(positionIndex, pointPos);
    }

    void PointToMousePos()
    {
        Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
        if (lastPos != mousePos)
        {
            AddAPoint(mousePos);
            lastPos = mousePos;
        }
    }

}
