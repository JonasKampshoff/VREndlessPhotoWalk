using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class PhotoCamera : MonoBehaviour
{
    [Header("Referenzen")]
    [SerializeField] private Camera cam;
    [SerializeField] private RenderTexture texture;
    [SerializeField] private Image image;
    [SerializeField] private Image kameraOverlay;


    [Header("Masken")]
    [SerializeField] private LayerMask withUI;
    [SerializeField] private LayerMask withoutUI;
    [SerializeField] private LayerMask triggerLayer;


    [Header("Kamera Einstellungen")]
    [SerializeField] private float fieldOfView = 90;
    [SerializeField] private float zoom = 1; // 1 - 4

    [SerializeField] private int width = 720;
    [SerializeField] private int heigth = 480;



    private List<Texture2D> images = new List<Texture2D>();
    private List<Sprite> sprites = new List<Sprite>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        image.enabled = false;
        kameraOverlay.enabled = true;
    }

    float delay = 5;

    // Update is called once per frame
    void Update()
    {
        //Zoom
        cam.fieldOfView = fieldOfView / zoom;
    }

    private void LateUpdate()
    {
        delay -= Time.deltaTime;
        if (delay < 0)
        {
            Photo();
            delay = 5;
        }
    }

    public void StoreImage(int id)
    {
        string filePath = Application.dataPath + "/Bild" + id + ".png";
        byte[] bytes = images[id].EncodeToPNG();
        File.WriteAllBytes(filePath, bytes);
    }

    public void Photo()
    {
        StartCoroutine(PhotoCoroutine());
    }

public void Zoom()
    {

    }

public void Zoomout()
    {

    }

    IEnumerator PhotoCoroutine()
    {
        //Setup for dem Foto
        //Deaktivieren des UI
        cam.cullingMask = withoutUI;
        RenderTexture activeRenderTexture = RenderTexture.active;
        RenderTexture.active = texture;

        //Foto erstellen
        cam.Render();
        Texture2D newImage = new Texture2D(width, heigth);
        newImage.ReadPixels(new Rect(0, 0, width, heigth), 0, 0);
        newImage.Apply();
        images.Add(newImage);
        TriggerPhotoEvents();


        //Setup nachher
        RenderTexture.active = activeRenderTexture;
        cam.cullingMask = withUI;


        Sprite sprite = Sprite.Create(newImage, new Rect(0, 0, newImage.width, newImage.height), new Vector2(newImage.width / 2, newImage.height / 2)); ;
        sprites.Add(sprite);
        image.enabled = true;
        image.sprite = sprite;

        kameraOverlay.enabled = false;
        yield return new WaitForSeconds(1);
        image.enabled = false;
        kameraOverlay.enabled = true;
    }
    //Vector2[] raycastPoints = new Vector2[] { new(0, 0), new(50, 50), new(-50, 50), new(50, -50), new(-50, -50), new(100, 100), new(-100, 100), new(100, -100), new(-100, -100), new(200, 150), new(-200, 150), new(200, -150), new(-200, -150), new(150, 0), new(-150, 0) };
    Vector2[] raycastPoints = new Vector2[] { new (0.5f, 0.5f), new (0.4f, 0.4f), new(0.6f, 0.4f), new(0.4f, 0.6f), new(0.6f, 0.6f), new(0.3f, 0.3f), new(0.7f, 0.3f), new(0.3f, 0.7f), new(0.7f, 0.7f), new(0.2f, 0.2f), new(0.8f, 0.2f), new(0.2f, 0.8f), new(0.8f, 0.8f), new(0.2f, 0.5f), new(0.8f, 0.5f) };
    int[] rayCastDistance = new int[] { 40, 40, 40, 40, 40, 30, 30, 30, 30, 20, 20, 20, 20, 20, 20, 20 };

    Ray[] rays;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        if (rays != null)
            for (int i = 0; i < raycastPoints.Length; i++)
            {
                Gizmos.DrawLine(rays[i].origin, rays[i].origin + rays[i].direction * rayCastDistance[i]*zoom);
            }
    }

    public void TriggerPhotoEvents() {

        rays = new Ray[raycastPoints.Length];
        for (int i = 0; i < raycastPoints.Length; i++)
        {
            rays[i] = cam.ViewportPointToRay(raycastPoints[i]);

            RaycastHit[] hits = Physics.RaycastAll(rays[i], rayCastDistance[i] * zoom, triggerLayer);
            foreach (RaycastHit hit in hits)
            {
                if (hit.transform.TryGetComponent<PhotoEvent>(out PhotoEvent trigger))
                {
                    trigger.TriggerEvent();
                }
            }
        }
       
    }
}
