using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class DungeonGenerator : MonoBehaviour
{

    public class Cell
    {
        public bool visited = false;
        public bool[] status = new bool[4];
    }

    [System.Serializable]
    public class Rule
    {
       /* public GameObject room;
        public Vector2Int minPos;
        public Vector2Int maxPos;

        public bool obligatory;

        public int ProbabilityofSpawning(int x, int y)
        {
            // 0 - cannot spawn 1 - can spawn 2 - HAS to spawn

            if(x>= minPos.x && x<= maxPos.x && y>= minPos.y && y<= maxPos.y)
            {
                return obligatory ? 2 : 1;
            }

            return 0;
        }*/

    }
    public NavMeshSurface navMeshSurface;

    public GameObject playerPrefab;
    Vector2Int firstCellPosition = new Vector2Int(0, 0);
    public Vector2Int size;
    public int startPos = 0;
    public GameObject[] rooms;
    //public Rule[] rooms;
    public Vector2 offset = new Vector2(0, 0);
    List<Cell> board;
    // Start is called before the first frame update
    void Start()
    {
        MazeGenerator();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateDungeon()
    {

        
    
        for (int i =0; i<size.x; i++)
        {
            for (int j =0; j<size.y; j++)
            {
                Cell currentCell = board[(i+j*size.x)];
                if(currentCell.visited)
                 {
                   /* int randomRoom = -1;

                    List<int> availableRooms = new List<int>();

                    for(int k=0; k< rooms.Length; k++)
                    {
                        int p = rooms[k].ProbabilityofSpawning(i, j);

                        if(p == 2)
                        {
                            randomRoom = k;
                            break;
                        }
                        else if(p == 1)
                        {
                            availableRooms.Add(k);
                        }
                    }

                    if(randomRoom == -1)
                    {
                        if(availableRooms.Count > 0)
                        {
                            randomRoom = availableRooms[Random.Range(0,             availableRooms.Count)];
                        }
                        else
                    {
                        randomRoom = 0;
                    }
                    }*/
                    
                     
              int randomRoom = Random.Range(0, rooms.Length);
              var newRoom = Instantiate(rooms[randomRoom]/*.room*/, new Vector3(i*offset.x,0,-j*offset.y), Quaternion.identity, transform).GetComponent<RoomBehaviour>();
              newRoom.UpdateRoom(currentCell.status);

              newRoom.name += " " + "" + j;
                }

                
            }
        }

           
       
        
        // if (board.Count > 0)
        // {
        //     Cell firstCell = board[0];
        //     Vector3 playerPosition = new Vector3(3, 0, 3);
        //     Instantiate(playerPrefab, playerPosition + new Vector3(offset.x * firstCellPosition.x, 0, -offset.y * firstCellPosition.y), Quaternion.identity, transform);
        // }

       

navMeshSurface.BuildNavMesh();

    }









    void MazeGenerator() 
    {
        board = new List<Cell>();
        for (int i =0; i<size.x; i++)
        {
            for (int j =0; j<size.y; j++)
            {
                board.Add(new Cell());
            }
        }

        int CurrentCell = startPos;

        Stack<int> path = new Stack<int>();

        int k = 0;
        while (k<1000)
        {
            k++;
            board[CurrentCell].visited= true;

            if(CurrentCell == board.Count - 1)
            {
                break;
            }

            //check neighbors

            List<int> neighbors = CheckNeighbors(CurrentCell);

            if(neighbors.Count == 0)
            {
                if(path.Count == 0)
                {
                    break;
                }
                else
                {
                    CurrentCell =path.Pop();
                }
            }
            else
            {
                path.Push(CurrentCell);
                int newCell = neighbors[Random.Range(0, neighbors.Count)];

                if(newCell > CurrentCell)
                {
                    //going down or right
                    if(newCell-1 == CurrentCell)
                    {
                        board[CurrentCell].status[2] = true;
                        CurrentCell = newCell;
                        board[CurrentCell].status[3] = true;
                    }
                    else
                    {
                        board[CurrentCell].status[1] = true;
                        CurrentCell = newCell;
                        board[CurrentCell].status[0] = true;
                    }
                }
                else
                {
                    //going up or left
                    if(newCell+1 == CurrentCell)
                    {
                        board[CurrentCell].status[3] = true;
                        CurrentCell = newCell;
                        board[CurrentCell].status[2] = true;
                    }
                    else
                    {
                        board[CurrentCell].status[0] = true;
                        CurrentCell = newCell;
                        board[CurrentCell].status[1] = true;
                    }
                }
            }

        }
        GenerateDungeon();
    }

    List<int> CheckNeighbors(int cell)
    {
        List<int> neighbors = new List<int>();

        //check up neighbor

        if(cell-size.x>=0 && !board[(cell-size.x)].visited)
        {
            neighbors.Add((cell-size.x));
        }

        //check down neighbor

        
        if(cell+size.x < board.Count && !board[(cell+size.x)].visited)
        {
            neighbors.Add((cell+size.x));
        }

        //check right neighbor

        if((cell+1) % size.x != 0 && !board[(cell+1)].visited)
        {
            neighbors.Add((cell+1));
        }

        //check left neighbor

        if(cell % size.x != 0 && !board[(cell-1)].visited)
        {
            neighbors.Add((cell-1));
        }

        return neighbors;
    }
}
