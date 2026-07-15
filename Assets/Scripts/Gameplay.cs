using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Gameplay : MonoBehaviour
{
    public GameObject[] Swords;
    public GameObject[] holes;
    public GameObject pirate;
    int holeCount = 0;
    int rndHole = 0;
    bool isGameOver = false;

    void RestarGame()
    { 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    private void Start()
    {
        holeCount = holes.Length;
        rndHole = Random.Range(0, holeCount);
    }

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.CompareTag("Holes"))
                {
                    for (int i = 0; i < holes.Length; i++)
                    {
                        if (holes[i].gameObject == hit.collider.gameObject)
                        {
                            holes[i].SetActive(false);
                            Swords[i].SetActive(true);
                            if (i == rndHole) isGameOver = true;
                            break;
                        }
                    }
                }


                Debug.Log("Hit object: " + hit.collider.gameObject.name);
            
            }
        }
        Vector2 scrollValue = Mouse.current.scroll.ReadValue();
        scrollValue.y *= 200;
        transform.Rotate(Vector3.up, 20 * scrollValue.y * Time.deltaTime);

        if (Keyboard.current.rKey.wasPressedThisFrame)
        { 
            RestarGame();
        }
        
    }
    private void FixedUpdate()
    {
        if (isGameOver)
        { 
            Rigidbody rb = GetComponentInChildren<Rigidbody>();
            rb.AddForce(17, 1000, 15);
            isGameOver = false;
            pirate.transform.SetParent(null);

        }

    }
}
