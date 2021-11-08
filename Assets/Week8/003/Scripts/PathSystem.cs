using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PathSystem : MonoBehaviour {

    public enum SeedType { RANDOM, CUSTOM }
    [Header("Random Related Stuff")]
    public SeedType seedType = SeedType.RANDOM;
    System.Random random;
    public int seed = 0;

    [Space]
    public bool animatedPath;
    public List<MyGridCell> gridCellList = new List<MyGridCell>();

    public List<MyGridCell> gridCellList2 = new List<MyGridCell>(); // create second grid cell list for second path wall

    public List<MyGridCell> gridCellList3 = new List<MyGridCell>(); // create second grid cell list for second path wall

    public List<float> colors; // create an array list of random numbers used to determin color for gizmos
    public int pathLength = 10;
    [Range(0.9f, 10.0f)]
    public float cellSize = 1.0f;

    public Transform startLocation;

    public Vector2 currentPositionOffset = new Vector2(2, 2); // add offset of 2 spaces to right

    public Vector2 currentPositionOffset2 = new Vector2(2, 2); // add offset of 2 spaces to right

    public Vector2 start = new Vector2(-4, 2); // add offset to close the corners of the grid formation
    public Vector2 start2 = new Vector2(2, -4); // add offset to close the corners of the grid formation

    public Vector2 start3 = new Vector2(-6, 2); // add offset to close the corners of the grid formation
    public Vector2 start4 = new Vector2(1, 0); // add offset to close the corners of the grid formation
    public Vector2 start5 = new Vector2(0, 1); // add offset to close the corners of the grid formation
    public Vector2 start6 = new Vector2(2, 0); // add offset to close the corners of the grid formation
    public GameObject PathCollider;

    public GameObject Loot0;
    public GameObject Loot1;
    public GameObject Loot2;
    public GameObject Loot3;

    public GameObject Reset;

    public GameObject Player; 

    public PlayerMove Resetting; 

    public Collider2D FirstCollider; // for player
    public Collider2D SecondCollider; // for last green box of dungeon


    // Start is called before the first frame update
    void Start() 
    {
        Player.transform.position = new Vector3(-2.157746f, -3.134951f, 1f); // starting position in each formation
        Resetting = GameObject.Find("Player").GetComponent<PlayerMove>(); // Gets player move script

         SetSeed();

            if (animatedPath)
                StartCoroutine(CreatePathRoutine());
            else
                CreatePath();
    }

    void SetSeed() 
    {
        if (seedType == SeedType.RANDOM) 
        {
            random = new System.Random();
        }
        else if (seedType == SeedType.CUSTOM) 
        {
            random = new System.Random(seed);
        }
    }

    void AddGridCell(Vector2 position)
    {
        var offset = new MyGridCell(position + currentPositionOffset);
        var offset2 = new MyGridCell(position + currentPositionOffset2);
        
        gridCellList.Add(new MyGridCell(position));
        gridCellList2.Add(offset); // add second current position for second grid cell list
        gridCellList3.Add(offset2); // add second current position for second grid cell list

        Instantiate(PathCollider, position + currentPositionOffset, Quaternion.identity);
        Instantiate(PathCollider, position + currentPositionOffset2, Quaternion.identity);

        colors.Add(Random.Range(0.2f, 0.8f));
        colors.Add(Random.Range(0.2f, 0.8f));
        colors.Add(Random.Range(0.2f, 0.8f));
        colors.Add(Random.Range(0.2f, 0.8f));
        colors.Add(Random.Range(0.2f, 0.8f));
        colors.Add(Random.Range(0.2f, 0.8f));
    }
    

    void ClearGridCells()
    {
        gridCellList.Clear();
        gridCellList2.Clear(); // clear second grid cell list
        gridCellList3.Clear(); // clear second grid cell list
        colors.Clear(); // clear random color list
        
        GameObject[] PathColliders = GameObject.FindGameObjectsWithTag("Collider");

        for (int i = 0; i < PathColliders.Length; i++)
        {
            Destroy(PathColliders[i]); //destroy all colliders by going through collider list
        }

        GameObject[] Loot = GameObject.FindGameObjectsWithTag("Loot");

        for (int i = 0; i < Loot.Length; i++)
        {
            Destroy(Loot[i]); //destroy all colliders by going through collider list
        }

        Player.transform.position = new Vector3(-2.157746f, -3.134951f, 1f); // starting position in each formation

        Resetting.touching = false;
    }

    void CreatePath() 
    {

        //gridCellList.Clear();
        //gridCellList2.Clear(); // clear second grid cell list
        //gridCellList3.Clear(); // clear second grid cell list
        ClearGridCells(); // calling function to carry out task on lines above, more convenient

        Vector2 currentPosition = startLocation.transform.position;

        //gridCellList.Add(new MyGridCell(currentPosition));
        //gridCellList2.Add(new MyGridCell(currentPosition + currentPositionOffset));
        //gridCellList3.Add(new MyGridCell(currentPosition + currentPositionOffset2));
        AddGridCell(currentPosition); // calling function to carry out task on lines above, more convenient

        var offset3 = new MyGridCell(currentPosition + start);
        var offset4 = new MyGridCell(currentPosition + start2);
        var offset5 = new MyGridCell(currentPosition + start3);

        gridCellList2.Add(offset3); // add second current position for second grid cell list
        gridCellList3.Add(offset4); // add second current position for second grid cell list
        gridCellList2.Add(offset5); // add second current position for second grid cell list

        Instantiate(PathCollider, currentPosition + start, Quaternion.identity);
        Instantiate(PathCollider, currentPosition + start2, Quaternion.identity);
        Instantiate(PathCollider, currentPosition + start3, Quaternion.identity);

        for (int i = 0; i < pathLength; i++) 
        {

            int n = random.Next(100);

            if (n.IsBetween(0, 24)) 
            {
                currentPosition = new Vector2(currentPosition.x + cellSize, currentPosition.y);

                if (n.IsBetween(10, 15))
                {
                    Instantiate(Loot0, currentPosition, Quaternion.identity);
                }
            }

            else if(n.IsBetween(25, 49))
            {
                currentPosition = new Vector2(currentPosition.x, currentPosition.y + cellSize);

                if (n.IsBetween(40, 45))
                {
                    Instantiate(Loot1, currentPosition, Quaternion.identity);
                }
            }

            else if (n.IsBetween(50, 75)) 
            {
                currentPosition = new Vector2(currentPosition.x + cellSize, currentPosition.y);
                
                if (n.IsBetween(60, 65))
                {
                    Instantiate(Loot2, currentPosition, Quaternion.identity);
                }
            }

            else
            {
                currentPosition = new Vector2(currentPosition.x, currentPosition.y + cellSize);

                if (n.IsBetween(85, 90))
                {
                    Instantiate(Loot3, currentPosition, Quaternion.identity);
                }
            }
   

            //gridCellList.Add(new MyGridCell(currentPosition));
            //gridCellList2.Add(new MyGridCell(currentPosition + currentPositionOffset)); // add second current position for second grid cell list
            //gridCellList3.Add(new MyGridCell(currentPosition + currentPositionOffset2)); // add second current position for second grid cell list
            AddGridCell(currentPosition); // calling function to carry out task one lines above, more convenient

            
            if (i == pathLength - 1)    
            {
                Instantiate(Reset, currentPosition, Quaternion.identity);
            }

        }

        var offset6 = new MyGridCell(currentPosition + start4);
        var offset7 = new MyGridCell(currentPosition + start5);
        var offset8 = new MyGridCell(currentPosition + start6);


        gridCellList2.Add(offset6); // add second current position for second grid cell list
        gridCellList3.Add(offset7); // add second current position for second grid cell list
        gridCellList3.Add(offset8); // add second current position for second grid cell list

        Instantiate(PathCollider, currentPosition + start4, Quaternion.identity);
        Instantiate(PathCollider, currentPosition + start5, Quaternion.identity);
        Instantiate(PathCollider, currentPosition + start6, Quaternion.identity);
    }

    IEnumerator CreatePathRoutine() 
    {

        //gridCellList.Clear();
        //gridCellList2.Clear(); // clear second grid cell list
        //gridCellList3.Clear(); // clear second grid cell list
        ClearGridCells(); // calling function to carry out task on lines above, more convenient

        Vector2 currentPosition = startLocation.transform.position;

        //gridCellList.Add(new MyGridCell(currentPosition));
        //gridCellList2.Add(new MyGridCell(currentPosition + currentPositionOffset)); // add second current position for second grid cell list
        //gridCellList2.Add(new MyGridCell(currentPosition + currentPositionOffset2)); // add second current position for second grid cell list
        AddGridCell(currentPosition); // calling function to carry out task one lines above, more convenient

        for (int i = 0; i < pathLength; i++) 
        {

            int n = random.Next(100);

            if (n.IsBetween(0, 49)) 
            {
                currentPosition = new Vector2(currentPosition.x + cellSize, currentPosition.y);
            }
            else 
            {
                currentPosition = new Vector2(currentPosition.x, currentPosition.y + cellSize);
            }

            //gridCellList.Add(new MyGridCell(currentPosition));
            //gridCellList2.Add(new MyGridCell(currentPosition + currentPositionOffset)); // add second current position for second grid cell list
            //gridCellList3.Add(new MyGridCell(currentPosition + currentPositionOffset2)); // add second current position for second grid cell list
            AddGridCell(currentPosition); // calling function to carry out task one lines above, more convenient
            yield return null;
        }
    }



    private void OnDrawGizmos() 
    {
        for (int i = 0; i < gridCellList.Count; i++) 
        {
            if (i < gridCellList.Count - 1)
            {
                Gizmos.color = Color.white;
                Gizmos.DrawWireCube(gridCellList[i].location, Vector3.one * cellSize);
                Gizmos.color = new Color(0.764151f, 0.764151f, 0.764151f, 0.7f);
                Gizmos.DrawCube(gridCellList[i].location, Vector3.one * cellSize);
            }

            else if (i == gridCellList.Count - 1)
            {
                Gizmos.color = Color.white;
                Gizmos.DrawWireCube(gridCellList[i].location, Vector3.one * cellSize);
                Gizmos.color = new Color(0.2093606f, 0.9716981f, 0.196173f, 0.8f); // light alpha transparent green for end of dungeon marker
                //Gizmos.color = new Color(0.764151f, 0.764151f, 0.764151f, 0.5f);
                Gizmos.DrawCube(gridCellList[i].location, Vector3.one * cellSize);
            }
        }

        for (int i = 0; i < gridCellList2.Count; i++) 
        {
            //Gizmos.color = Color.red;
            Gizmos.color = new Color(colors[i], colors[i + 1], colors[i + 2], 0.5f);
            Gizmos.DrawWireCube(gridCellList2[i].location, Vector3.one * cellSize); // draw second grid cell list
            Gizmos.color = new Color(colors[i + 3], colors[i + 4], colors[i + 5], 0.5f);
            //Gizmos.color = new Color(0.490566f, 0.02739405f, 0.02739405f, 0.5f);
            Gizmos.DrawCube(gridCellList2[i].location, Vector3.one * cellSize); // draw second grid cell list
        }

        for (int i = 0; i < gridCellList3.Count; i++) 
        {
            //Gizmos.color = Color.red;
            Gizmos.color = new Color(colors[i], colors[i + 1], colors[i + 2], 0.5f);
            Gizmos.DrawWireCube(gridCellList3[i].location, Vector3.one * cellSize); // draw third grid cell list

            var color4 = Random.Range(0f, 1f);
            var color5 = Random.Range(0f, 1f);
            var color6 = Random.Range(0f, 1f);
            Gizmos.color = new Color(colors[i + +3], colors[i + 4], colors[i + 5], 0.5f);
            //Gizmos.color = new Color(0.490566f, 0.02739405f, 0.02739405f, 0.5f);
            Gizmos.DrawCube(gridCellList3[i].location, Vector3.one * cellSize); // draw third grid cell list
        }
        
    }

    // Update is called once per frame
    void Update() 
    {
        /*
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            SetSeed();

            if (animatedPath)
                StartCoroutine(CreatePathRoutine());
            else
                CreatePath();
        }
        */

        if (Resetting.touching == true)
        {
            SetSeed();

            if (animatedPath)
                StartCoroutine(CreatePathRoutine());
            else
                CreatePath();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            seedType = SeedType.CUSTOM; //same levels
            seed = 1;

            SetSeed();
            

            if (animatedPath)
                StartCoroutine(CreatePathRoutine());
            else
                CreatePath();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            seedType = SeedType.RANDOM; // randomize levels
            seed = 0;
            
            SetSeed();

            if (animatedPath)
                StartCoroutine(CreatePathRoutine());
            else
                CreatePath();
        }
     
    }
}
