using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public List<LevelBlock> legoBlocks = new List<LevelBlock>();
    List<LevelBlock> currentBlocks = new List<LevelBlock>();

    public Transform initialPoint;

    private static LevelGenerator _sharedInstance;

    public static LevelGenerator sharedInstance
    {
        get 
        {
            return _sharedInstance;
        }
    }
    
    public byte initialBlockNumber = 2;

    private void Awake()
    {
        _sharedInstance = this;     
    }

    public void createInitialBlocks() 
    { 
        if(currentBlocks.Count < 0)
        {
            return;
        }
        for (byte i = 0; i < initialBlockNumber; i++)
        {
            AddNewBlock(true);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddNewBlock(bool initialBlocks = false)
    {
        int randNumber = initialBlocks ? 0 : Random.Range(0, legoBlocks.Count);
        var block = Instantiate( legoBlocks[randNumber]);
        block.transform.SetParent(this.transform, false);
        Vector3 blockPosition = Vector3.zero;
        if(currentBlocks.Count == 0)
        {
            blockPosition = initialPoint.position;
        }
        else
        {
            int lastBlockPos = currentBlocks.Count - 1;
            blockPosition = currentBlocks[lastBlockPos].exitPoint.position;
        }
        block.transform.position = blockPosition;
        currentBlocks.Add(block);
    }

    public void RemoveOldBlock()
    {
        var oldBlock = currentBlocks[0];
        currentBlocks.Remove(oldBlock);
        Destroy(oldBlock.gameObject);
    }

    public void RemoveAllBlocks()
    {
        while (currentBlocks.Count > 0)
        {
            RemoveOldBlock();
        }
    }

}
