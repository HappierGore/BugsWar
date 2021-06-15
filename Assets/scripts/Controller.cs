using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DigitalRubyShared;
public class Controller : MonoBehaviour
{
    [SerializeField] Camera camara = null;
    private TapGestureRecognizer tapGesture;
    private ScaleGestureRecognizer scaleGesture;
    private PanGestureRecognizer panGesture;
    private float scalingZ = 5.0f;
    public static Vector3 clickedPosition;
    // Start is called before the first frame update
    void Start()
    {
        CreateTapGesture();
        CreatePanGesture();
        CreateScaleGesture();
    }

    // --------------- GESTOS --------------
    private void CreateTapGesture()
    {
        tapGesture = new TapGestureRecognizer();
        tapGesture.StateUpdated += TapGestureCallback;
        FingersScript.Instance.AddGesture(tapGesture);
    }
    private void CreateScaleGesture()
    {
        scaleGesture = new ScaleGestureRecognizer();
        scaleGesture.StateUpdated += ScaleGestureCallback;
        FingersScript.Instance.AddGesture(scaleGesture);
    }
    private void CreatePanGesture()
    {
        panGesture = new PanGestureRecognizer();
        panGesture.MinimumNumberOfTouchesToTrack = 1;
        panGesture.StateUpdated += PanGestureCallback;
        FingersScript.Instance.AddGesture(panGesture);
    }

    // Activos
    private void TapGestureCallback(GestureRecognizer gesture)
    {
        if (gesture.State == GestureRecognizerState.Ended)
        {
            Vector3 clickPos = new Vector3(gesture.FocusX, gesture.FocusY, 0.0f);
            clickPos = Camera.main.ScreenToWorldPoint(clickPos);
            clickedPosition = clickPos;
        }
    }
    private void ScaleGestureCallback(GestureRecognizer gesture)
    {
        if (gesture.State == GestureRecognizerState.Executing)
        {
            scalingZ = scaleGesture.ScaleMultiplier * camara.orthographicSize;
            camara.orthographicSize = scalingZ;
        }
    }
    private void PanGestureCallback(GestureRecognizer gesture)
    {
        if (gesture.State == GestureRecognizerState.Executing)
        {
            float deltaX = panGesture.DeltaX / 100.0f;
            Vector3 pos = camara.transform.position;
            pos.x += deltaX * -1;
            if (MobMovement.ownCastle.position.x + 8.0f < pos.x &&  MobMovement.ownCastle.position.x < camara.transform.position.x &&
                MobMovement.enemyCastle.position.x - 8.0f > pos.x && MobMovement.enemyCastle.position.x > camara.transform.position.x)
            {
                camara.transform.position = pos;
            }
        }
    }
}


