using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public ObjectInspect OI;
    [SerializeField] private Transform gameTransform;
    [SerializeField] private Transform piecePrefab;

    private Transform selectedPiece;

    private List<Transform> pieces;
    private int emptyLocation;
    private int size;
    private bool shuffling = false;
    // Start is called before the first frame update
    void Awake()
    {
        pieces = new List<Transform>();
        size = 3;
        CreateGamePieces(0.01f);
    }
   

public void CreateGamePieces(float gapThickness)
    {
        float width = 1 / (float)size;
        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                Transform piece = Instantiate(piecePrefab, gameTransform);
                pieces.Add(piece);

                piece.localPosition = new Vector3(-1 + (2 * width * col) + width,
                                                  +1 - (2 * width * row) - width, 0);

                piece.localScale = ((2 * width) - gapThickness) * Vector3.one;
                piece.name = $"{(row * size) + col}";

                if ((row == size - 1) && (col == size - 1))
                {
                    emptyLocation = (size * size) - 1;
                    piece.gameObject.SetActive(false);
                }
                else
                {
                    float gap = gapThickness / 2;
                    Mesh mesh = piece.GetComponent<MeshFilter>().mesh;
                    Vector2[] uv = new Vector2[4];

                    uv[0] = new Vector2((width * col) + gap, 1 - ((width * (row + 1)) - gap));
                    uv[1] = new Vector2((width * (col + 1)) - gap, 1 - ((width * (row + 1)) - gap));
                    uv[2] = new Vector2((width * col) + gap, 1 - ((width * row) + gap));
                    uv[3] = new Vector2((width * (col + 1)) - gap, 1 - ((width * row) + gap));

                    mesh.uv = uv;
                }
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

        // check for completion
        if(!shuffling && CheckCompletion())
        {
            shuffling = true;
            StartCoroutine(WaitShuffle(0.5f));
        }
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(OI.zoomCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            //Debug.Log(hit.collider.name);

            if (hit)
            {
                for (int i = 0; i < pieces.Count; i++)
                {
                    if (pieces[i] == hit.transform)
                    {
                        selectedPiece = pieces[i];
                         if(SwapIfValid(i, -size, size )) { break; }
                         if(SwapIfValid(i, +size, size )) { break; }
                         if(SwapIfValid(i,-1, 0 )) { break; }
                         if(SwapIfValid(i, +1, size -1 )) { break; }
                    }
                }
            }
        }
    }
    private bool SwapIfValid(int i, int offset, int colCheck)
    {
        if (((i % size) != colCheck) && ((i + offset) == emptyLocation))
        {
            (pieces[i], pieces[i + offset]) = (pieces[i + offset], pieces[i]);
            (pieces[i].localPosition, pieces[i + offset].localPosition) = ((pieces[i + offset].localPosition, pieces[i].localPosition));
            emptyLocation = i;
            return true;  
        }
        return false;
    }

    private bool CheckCompletion()
    {
        for (int i = 0; i < pieces.Count;i++)
        {
            if (pieces[i].name != $"{i}")
            {
                return false;
            } 
        }
        return true;
    }

    private IEnumerator WaitShuffle(float duration)
    {
      yield return new WaitForSeconds(duration);
        Shuffle();
        shuffling = false;

        Destroy(gameObject);
    }
    private void Shuffle()
    {
        int count = 0;
        int last = 0;
        while (count < (size * size * size))
        {
            int rand = Random.Range(0, size * size);

            if (rand == last) { continue; }

            last = emptyLocation;

            if (SwapIfValid(rand, -1, 0))
            {
                count++;
            }
            else if (SwapIfValid(rand, +size, size))
            {
                count++;
            }
            else if (SwapIfValid(rand, -1, 0))
            {
                count++;
            }
            else if (SwapIfValid(rand, +1, size - 1))
            {
                count++;
            }
        }
    }
}
