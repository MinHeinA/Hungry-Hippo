using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class EnemyMovement : MonoBehaviour
{
    // public variables
    public float movementRate = 0.01f;
    public int xmin = 0, xmax = 15, ymin = 0, ymax = 6;
    public int minSquare = 6;
    public int maxSquare = 9;
    public float stunTime = 1f;
    public AudioSource footStepsSrc;
    public AudioSource alertSrc;
    public AudioSource bruhSrc;
    public AudioClip[] audioClips;
    public int hippostate = 0;

    // private variables
    Transform player;
    bool stillmoving = false;
    float xpos = 0, ypos = 0;
    string[] obstacleCoords;
    bool[] isObstacle;
    Animator myAnim;
    // 0 - Unalerted, 1 - Chase Crystal, 2 - Chase Player, 3 - Detected, 4 - Crazy
    int targetxpos = 0, targetypos = 0;
    float countdown = 0f;
    SpriteRenderer spriteRenderer;
    Color hippoTint;
    bool alertedAudioPlayed = false;

    [SerializeField]
    Tilemap secondLayerTilemap;
    [SerializeField]
    Tilemap groundTilemap;


    private void Start()
    {
        isObstacle = new bool[(xmax + 1) * (ymax + 1)];
        player = FindObjectOfType<PlayerAction>().transform;
        myAnim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        groundTilemap = GameObject.Find("Ground").GetComponent<Tilemap>();
        secondLayerTilemap = GameObject.Find("2ndLayer").GetComponent<Tilemap>();
        hippostate = 0;
        xpos = Mathf.Round(transform.position.x);
        ypos = Mathf.Round(transform.position.y);
        obstacleCoords = GetObstacles(secondLayerTilemap).ToArray();
        for (int i = 0; i < obstacleCoords.Length; i++)
        {
            int x = Int32.Parse(obstacleCoords[i].Split(';')[0]);
            int y = Int32.Parse(obstacleCoords[i].Split(';')[1]);
            isObstacle[x + y * (xmax + 1)] = true;
        }
    }

    private Color HexToColor(String hexCode)
    {
        if (ColorUtility.TryParseHtmlString(hexCode, out hippoTint))
        {
            return hippoTint;
        }
        return hippoTint;
    }

    // when triggered by flashlight collider, then change state to detected
    void OnTriggerStay2D(Collider2D other)
    {
        // crazy state is not affected by flashlight
        if (hippostate == 3) return;

        if (!bruhSrc.isPlaying && !alertedAudioPlayed)
        {
            bruhSrc.Play();
            alertedAudioPlayed = true;
        }

        // stun hippo, then make hippo return to unalerted
        countdown = stunTime;
        myAnim.speed = 0;
        hippostate = 0;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        alertedAudioPlayed = false;
    }

    public void Crazy()
    {
        hippostate = 3;
        spriteRenderer.color = HexToColor("#FF1B00");
    }

    public void ChaseCrystal(int x, int y)
    {
        targetxpos = x;
        targetypos = y;
        hippostate = 1;
    }

    List<string> GetObstacles(Tilemap tilemap)
    {
        Vector3Int bottomLeft = new Vector3Int(xmin, ymin, 0); //coord x = 0, y = 0
        Vector3Int topRight = new Vector3Int(xmax, ymax, 0); // coord x = 15, y = 6

        Grid grid = tilemap.layoutGrid;

        Vector3Int bottomLeftCell = grid.WorldToCell(bottomLeft);
        Vector3Int topRightCell = grid.WorldToCell(topRight);

        Vector3Int min = Vector3Int.Min(bottomLeftCell, topRightCell);
        Vector3Int max = Vector3Int.Max(bottomLeftCell, topRightCell);

        Vector3Int size = max - min + Vector3Int.one;
        BoundsInt bounds = new BoundsInt(min, size);

        TileBase[] tileArray = tilemap.GetTilesBlock(bounds);

        List<string> coords = new List<string>();

        for (int x = 0; x < bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {
                TileBase tile = tileArray[x + y * bounds.size.x];
                if (tile != null && tile.name != "Tree 3" && tile.name != "safezone") //Tree 3 is the sprite that contains the shadow of the tree
                {
                    coords.Add(x.ToString() + ";" + y.ToString());
                }
            }
        }

        return coords;
    }

    // in-built function to move to a certain x and y coordinate
    void Move(float x, float y)
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(x, y, transform.position.z), movementRate);
    }

    private void FixedUpdate()
    {
        if (FindObjectOfType<GameOverScreen>().isGameOver())
        {
            footStepsSrc.Stop();
        }
        else
        {
            if (!footStepsSrc.isPlaying)
            {
                footStepsSrc.Play();
            }
        }
        if (countdown > 0)
        {
            spriteRenderer.color = HexToColor("#00FAFF");
            countdown -= Time.deltaTime;
            return;
        }

        // complete the movement first if the hippo is still moving
        if (stillmoving)
        {
            myAnim.speed = 1;
            Move(xpos, ypos);
            // check if the hippo has reached its destination
            if (xpos == transform.position.x && ypos == transform.position.y)
            {
                stillmoving = false;
            }
        }

        // if the hippo is not moving to a destination, choose the next destination
        else
        {
            float playerx = Mathf.Round(player.position.x), playery = Mathf.Round(player.position.y);
            float startpos = transform.position.x + transform.position.y * (xmax + 1);
            float endpos = 0;
            // change endpos to the corresponding target

            // if the hippo is at unalerted or chase crystal state, and within x squares from player,
            // then chase player

            if (hippostate <= 1 &&
            Mathf.Pow(playerx - transform.position.x, 2) + Mathf.Pow(playery - transform.position.y, 2) < minSquare * minSquare)
                hippostate = 2;
            if (hippostate == 2 &&
            Mathf.Pow(playerx - transform.position.x, 2) + Mathf.Pow(playery - transform.position.y, 2) > maxSquare * maxSquare)
                hippostate = 0;

            if (hippostate <= 2) spriteRenderer.color = HexToColor("#FFFFFF");

            // 0 - Unalerted, 1 - Chase Crystal, 2 - Chase Player, 3 - Crazy
            if (hippostate == 0)
            {
                int newxpos = (int)transform.position.x, newypos = (int)transform.position.y;
                do
                {
                    int newdir = UnityEngine.Random.Range(0, 4);
                    // up
                    if (newdir == 0 && newypos < ymax)
                    {
                        endpos = newxpos + (newypos + 1) * (xmax + 1);
                    }
                    // right
                    else if (newdir == 1 && newxpos < xmax)
                    {
                        endpos = (newxpos + 1) + newypos * (xmax + 1);
                    }
                    // down
                    else if (newdir == 2 && newypos > ymin)
                    {
                        endpos = newxpos + (newypos - 1) * (xmax + 1);
                    }
                    // left
                    else if (newdir == 3 && newxpos > xmin)
                    {
                        endpos = (newxpos - 1) + newypos * (xmax + 1);
                    }
                    else { endpos = -1; }
                } while (endpos < 0 || isObstacle[(int)endpos]);

            }
            else if (hippostate == 1)
            {
                // chase the crystal
                endpos = targetxpos + targetypos * (xmax + 1);
            }
            else if (hippostate == 2 || hippostate == 3)
            {
                // Chase Player chases to the player location
                endpos = playerx + playery * (xmax + 1);
                if (!alertSrc.isPlaying)
                {
                    alertSrc.Play();
                }
            }

            // if hippo is already at target position, then set idle animation and go back to unalerted
            if (startpos == endpos)
            {
                myAnim.SetTrigger("Idle");
                hippostate = 0;
            }

            int dir = AStar((int)startpos, (int)endpos);

            if (dir == 0) return;
            else
            {
                xpos = transform.position.x;
                ypos = transform.position.y;
                stillmoving = true;
                // up
                if (dir == 1) ypos++;
                // right
                else if (dir == 2) xpos++;
                // down
                else if (dir == 3) ypos--;
                // left
                else if (dir == 4) xpos--;

                // Set up the animations based on hippo direction
                // up
                if (dir == 1) myAnim.SetTrigger("WalkUp");
                // right
                else if (dir == 2) myAnim.SetTrigger("WalkRight");
                // down
                else if (dir == 3) myAnim.SetTrigger("WalkDown");
                // left
                else if (dir == 4) myAnim.SetTrigger("WalkLeft");
            }
        }
    }

    // Manhattan Distance
    int estimateddist(int startnode, int endnode)
    {
        int startx = startnode % (xmax + 1), starty = startnode / (xmax + 1);
        int endx = startnode % (xmax + 1), endy = startnode / (xmax + 1);

        return Mathf.Abs(startx - endx) + Mathf.Abs(starty - endy);
    }

    // A* Search
    int AStar(int startnode, int endnode)
    {
        // node position is given by y*width + x
        // (bool) array of size [width*height] - contains whether the node position is visited
        bool[] isVisited = new bool[(xmax + 1) * (ymax + 1)];
        // (int) direction array of size [width*height] - contains direction variable (0 - undef, 1 - up, 2 - right, 3 - down, 4 - left)
        int[] dir = new int[(xmax + 1) * (ymax + 1)];
        // (int) total distance to current node
        int[] dist = new int[(xmax + 1) * (ymax + 1)];
        // queue to determine the order of expanding nodes
        PQueue<int> node = new PQueue<int>(); // First start with empty queue

        // Set inaccessible positions as visited in visited list
        // Positions: (14, 1), (14, 2), (12, 3), (12, 4), (12, 5), (9, 2), (9, 3), (9, 4), (5, 2), (5, 3), (5, 4)
        // (2, 3), (2, 4), (2, 5)
        // int[] obstaclex = { 14, 14, 12, 12, 12, 9, 9, 9, 5, 5, 5, 2, 2, 2 };
        // int[] obstacley = { 1, 2, 3, 4, 5, 2, 3, 4, 2, 3, 4, 3, 4, 5 };
        for (int i = 0; i < obstacleCoords.Length; i++)
        {
            int x = Int32.Parse(obstacleCoords[i].Split(';')[0]);
            int y = Int32.Parse(obstacleCoords[i].Split(';')[1]);
            isVisited[x + y * (xmax + 1)] = true;
        }


        // Add in hippo position as initial node in queue
        node.Enqueue(startnode, 0);
        dist[startnode] = 0;

        // While queue is not empty
        while (node.Count > 0)
        {
            // Pop first node in queue
            // TODO: Pop node with highest priority
            int curnode = node.Dequeue();

            // If node is end position, return direction variable in that node
            if (curnode == endnode) return dir[curnode];

            // 1. Add in neighbours of element into queue if not in visited list
            // 2. Add in neighbours of element into visited list
            // 3. Set the neighbours' direction variable
            int curx = (int)(curnode % (xmax + 1));
            int cury = (int)(curnode / (xmax + 1));
            int upnode = curx + (cury + 1) * (xmax + 1), downnode = curx + (cury - 1) * (xmax + 1),
            rightnode = (curx + 1) + cury * (xmax + 1), leftnode = (curx - 1) + cury * (xmax + 1);

            // TODO: When storing the node, store the current distance to node + heuristic distance to end goal.
            // Heuristic distance used is Manhattan distance |x1-x2|+|y1-y2|

            // Up
            if (cury < ymax && !isVisited[upnode])
            {
                dist[upnode] = dist[curnode] + 1;
                node.Enqueue(upnode, dist[upnode] + estimateddist(upnode, endnode));
                isVisited[upnode] = true;
                if (dir[curnode] > 0) dir[upnode] = dir[curnode];
                else dir[upnode] = 1;
            }

            // Right
            if (curx < xmax && !isVisited[rightnode])
            {
                dist[rightnode] = dist[curnode] + 1;
                node.Enqueue(rightnode, dist[rightnode] + estimateddist(rightnode, endnode));
                isVisited[rightnode] = true;
                if (dir[curnode] > 0) dir[rightnode] = dir[curnode];
                else dir[rightnode] = 2;
            }

            // Down
            if (cury > ymin && !isVisited[downnode])
            {
                dist[downnode] = dist[curnode] + 1;
                node.Enqueue(downnode, dist[downnode] + estimateddist(downnode, endnode));
                isVisited[downnode] = true;
                if (dir[curnode] > 0) dir[downnode] = dir[curnode];
                else dir[downnode] = 3;
            }

            // Left
            if (curx > xmin && !isVisited[leftnode])
            {
                dist[leftnode] = dist[curnode] + 1;
                node.Enqueue(leftnode, dist[leftnode] + estimateddist(leftnode, endnode));
                isVisited[leftnode] = true;
                if (dir[curnode] > 0) dir[leftnode] = dir[curnode];
                else dir[leftnode] = 4;
            }
        }

        return 0;
    }
}