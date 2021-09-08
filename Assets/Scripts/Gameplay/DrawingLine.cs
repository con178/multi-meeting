using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DrawingLine : MonoBehaviourPun
{
    [SerializeField] private PlayerMovement playerMovement;

    [SerializeField] private GameObject currentLine;
    [SerializeField] private LineRenderer lineRenderer;

    [SerializeField] private List<Vector2> fingerPositions;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (!GetComponent<PhotonView>().IsMine) return;

        DrawingInput();
    }

    void DrawingInput()
    {
        if(Input.GetMouseButtonDown(0) && playerMovement.pointerRenderer.gameObject.activeSelf)
        {
            if(playerMovement.isLookingAtWhiteBoard)
            {
                photonView.RPC("CreateLine", RpcTarget.All);
            }
                
        }
        if(Input.GetMouseButton(0) && playerMovement.pointerRenderer.gameObject.activeSelf)
        {
            if(playerMovement.isLookingAtWhiteBoard)
            {
                Vector3 tempFingerPosition = playerMovement.pointerEndPoint;
                if(Vector3.Distance(tempFingerPosition, fingerPositions[fingerPositions.Count - 1]) > 0.00001f)
                {
                    photonView.RPC("UpdateLine", RpcTarget.All, tempFingerPosition);
                }
            }
            
        }
    }

    [PunRPC]
    void CreateLine()
    {
        Transform whiteBoard = GameObject.FindGameObjectWithTag("WhiteBoard").transform;

        currentLine = PhotonNetwork.Instantiate("BoardLine", playerMovement.pointerEndPoint, Quaternion.FromToRotation(Vector3.forward, playerMovement.surfaceHitPoint));
        currentLine.transform.SetParent(whiteBoard);
        //currentLine.transform.position = playerMovement.pointerEndPoint;
        lineRenderer = currentLine.GetComponent<LineRenderer>();
        fingerPositions.Clear();
        fingerPositions.Add(playerMovement.pointerEndPoint);
        fingerPositions.Add(playerMovement.pointerEndPoint);
        lineRenderer.SetPosition(0, fingerPositions[0]);
        lineRenderer.SetPosition(1, fingerPositions[1]);
    } 

    [PunRPC]
    void UpdateLine(Vector3 newFingerPosition)
    {
        fingerPositions.Add(newFingerPosition);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, newFingerPosition);
    }
}
