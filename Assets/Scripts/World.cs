using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    //name for serialization
    public string worldName = "world";
    //maps positions to chunks
    public Dictionary<WorldPos, Chunk> chunks = new Dictionary<WorldPos, Chunk>();
    public GameObject chunkPrefab;
    Vector3 origin;
    Hashtable companyGardenPositions;
    List<string> companies;
    public Hashtable companyTrees;
    void Start()
    {
        origin = new Vector3(-7, 0, -26);
        companyGardenPositions = new Hashtable();
        companies = GameObject.Find("StockDatabase").GetComponent<StockInventory>().getCompaniesList();
        companyTrees = new Hashtable();
        for(int i = 0; i< companies.Count; i++)
        {
            List<KeyValuePair<Vector3, Chunk>> list = new List<KeyValuePair<Vector3, Chunk>>();
            Company c = (Company)GameObject.Find("StockDatabase").GetComponent<StockInventory>().companyTranslation[companies[i]];
            if (!companyTrees.Contains(c))
            {
                companyTrees.Add(c, list);
            }
            
        }

    }
    public void RejevunateTrees()
    {
        //TerrainGen terrainGen = new TerrainGen();
        foreach (DictionaryEntry pair in companyTrees)
        {
            
            Company c = (Company) pair.Key;
            List<KeyValuePair<Vector3, Chunk>> list = (List < KeyValuePair < Vector3, Chunk>> )pair.Value;
            foreach (KeyValuePair<Vector3, Chunk> pair2 in list)
            {

                int height = 6;
                height = (int)(c.avg / 50) + 2;
                height = Mathf.Min(height, 10);
                Vector3 vec = (Vector3) pair2.Key;
                Chunk chunk = (Chunk)pair2.Value;
                CreateTree((int)vec.x, (int)vec.y,(int) vec.z, chunk,false, c.name, height);
            }
                
        }
    }
    public void AddToGarden(string name, int numStock)
    {
        Debug.Log(name);
        Debug.Log(numStock);
        Debug.Log(companyGardenPositions);
        if (companyGardenPositions.Contains(name))
        {
            Vector3 pos = (Vector3)companyGardenPositions[name];
            Block block = CreateBlock(name);
            block.selling = true;
            SetBlock(EditTerrain.GetBlockPos(pos).x, EditTerrain.GetBlockPos(pos).y + numStock, EditTerrain.GetBlockPos(pos).z, block);

        }
      
    }
    public void AddToCompanyGardenPositions(string company, Vector3 pos)
    {
        if(companyGardenPositions != null)
        {
           // Debug.Log(company);
           // Debug.Log(pos);
            if (!companyGardenPositions.Contains(company))
            {
                companyGardenPositions.Add(company, pos);
            }
          //  companyGardenPositions.Add(company, pos);
        }
        else
        {
            Debug.Log("NULL");
        }
      
    }
    Block CreateBlock(string name)
    {
        Block block;

        switch (name)
        {           
            case "Apple":
                block = new BlockApple();
                break;
            case "Alphabet":
                block = new BlockAlphabet();
                break;
            case "Amazon":
                block = new BlockAmazon();
                break;
            case "Facebook":
                block = new BlockFacebook();
                break;
            case "IBM":
                block = new BlockIBM();
                break;
            case "Intel":
                block = new BlockIntel();
                break;
            case "Microsoft":
                block = new BlockMicrosoft();
                break;
            case "Netflix":
                block = new BlockNetflix();
                break;
            case "Twitter":
                block = new BlockTwitter();
                break;
            case "Yahoo":
                block = new BlockYahoo();
                break;
            case "Walmart":
                block = new BlockWalmart();
                break;
            case "Chipotle":
                block = new BlockChipotle();
                break;
            case "Costco":
                block = new BlockCostco();
                break;
            case "Pepsi":
                block = new BlockPepsi();
                break;
            case "Symantec":
                block = new BlockSymantec();
                break;
            case "Garmin":
                block = new BlockGarmin();
                break;
            default:
                block = new BlockAir();
                break;
        }
        return block;
    }
    public void CreateChunk(int x, int y, int z)
    {
        WorldPos worldPos = new WorldPos(x, y, z);

        //Instantiate the at the coordiantes using the prefab
        GameObject newChunkObject = Instantiate(chunkPrefab, new Vector3(x, y, z), Quaternion.Euler(Vector3.zero)) as GameObject;
        Chunk newChunk = newChunkObject.GetComponent<Chunk>();

        newChunk.pos = worldPos;
        newChunk.world = this;

        //Add it to the chunks dictioanry with the position as key
        chunks.Add(worldPos, newChunk);

        TerrainGen terrainGen = new TerrainGen();
        terrainGen.SetOrigin(origin);
        terrainGen.SetWorld(this);
        //List<string> companies = new List<string>();
        ////TODO Give company names to terrain gen
        //companies.Add("Apple");
        //companies.Add("Alphabet");
        //companies.Add("Amazon");
        //companies.Add("Yahoo");
        //companies.Add("Twitter");
        terrainGen.SetCompanies(companies);
       


        newChunk = terrainGen.ChunkGen(newChunk);
        newChunk.SetBlocksUnmodified();
        Serialization.Load(newChunk);
    }
    public Chunk GetChunk(int x, int y, int z)
    {
        WorldPos pos = new WorldPos();
        //change to float? trouble if dividing two integers if they are negative?
        float multiple = Chunk.chunkSize;
        //calculate world Pos
        pos.x = Mathf.FloorToInt(x / multiple) * Chunk.chunkSize;
        pos.y = Mathf.FloorToInt(y / multiple) * Chunk.chunkSize;
        pos.z = Mathf.FloorToInt(z / multiple) * Chunk.chunkSize;
        Chunk containerChunk = null;
        //get value from dictionary
        chunks.TryGetValue(pos, out containerChunk);

        return containerChunk;
    }

    public Block GetBlock(int x, int y, int z)
    {
        Chunk containerChunk = GetChunk(x, y, z);
        if (containerChunk != null)
        {
            //convert to local position
            Block block = containerChunk.GetBlock(x - containerChunk.pos.x, y - containerChunk.pos.y, z - containerChunk.pos.z);
            return block;
        }
        else
        {
            //place holder if null
            return new BlockAir();
        }
    }

    public void SetBlock(int x, int y, int z, Block block)
    {
        Chunk chunk = GetChunk(x, y, z);
        if (chunk != null)
        {
            chunk.SetBlock(x - chunk.pos.x, y - chunk.pos.y, z - chunk.pos.z, block);
            chunk.update = true;

            //update neighbors esp when neighbor is in another chunk
            UpdateIfEqual(x - chunk.pos.x, 0, new WorldPos(x - 1, y, z));
            UpdateIfEqual(x - chunk.pos.x, Chunk.chunkSize - 1, new WorldPos(x + 1, y, z));
            UpdateIfEqual(y - chunk.pos.y, 0, new WorldPos(x, y - 1, z));
            UpdateIfEqual(y - chunk.pos.y, Chunk.chunkSize - 1, new WorldPos(x, y + 1, z));
            UpdateIfEqual(z - chunk.pos.z, 0, new WorldPos(x, y, z - 1));
            UpdateIfEqual(z - chunk.pos.z, Chunk.chunkSize - 1, new WorldPos(x, y, z + 1));
        }
    }

    public void DestroyChunk(int x, int y, int z)
    {
        Chunk chunk = null;
        if (chunks.TryGetValue(new WorldPos(x, y, z), out chunk))
        {
            //save the before its destroyed
          //  Serialization.SaveChunk(chunk);
            Object.Destroy(chunk.gameObject);
            chunks.Remove(new WorldPos(x, y, z));
        }
    }

    void UpdateIfEqual(int value1, int value2, WorldPos pos)
    {
        if (value1 == value2)
        {
            Chunk chunk = GetChunk(pos.x, pos.y, pos.z);
            if (chunk != null)
            {
                chunk.update = true;
            }
        }
    }
    //same tree every time
    public void CreateTree(int x, int y, int z, Chunk chunk, bool upsideDown, string company, int height)
    {
        StockInventory inven = GameObject.Find("StockDatabase").GetComponent<StockInventory>();


        Company c = (Company)inven.companyTranslation[company];
        int seas = GameObject.Find("Clock").GetComponent<Calendar>().month;
        List<double> list = (List<double>)inven.stocksValue[c];

        double curr = list[seas];
        double min = c.min;
        double inc = c.incrementer;
        double temp = (curr - min) / inc;
        int type = (int)temp;
        type = Mathf.Min(type, 4);
        type = Mathf.Max(type, 0);
        //create leaves
        if (height >= 3)
        {
            for (int xi = -2; xi <= 2; xi++)
            {
                for (int yi = height; yi <= height + 4; yi++)
                {
                    for (int zi = -2; zi <= 2; zi++)
                    {
                        if (!upsideDown)
                        {
                            Block leavesBlock;
                            //TODO change leaf color
                            if (type == 0)
                            {
                                leavesBlock = new BlockLeaves0();
                                //leavesBlock.SetCompany(company);
                            }
                            else if (type == 1)
                            {
                                leavesBlock = new BlockLeaves1();
                            }
                            else if (type == 2)
                            {
                                leavesBlock = new BlockLeaves2();
                            }
                            else if (type == 3)
                            {
                                leavesBlock = new BlockLeaves3();
                            }
                            else
                            {
                                leavesBlock = new BlockLeaves4();
                            }
                            //BlockGreenLeaves leavesBlock = new BlockGreenLeaves();
                            leavesBlock.SetCompany(company);
                            //TerrainGen.SetBlock(x + xi, y + yi, z + zi, leavesBlock, chunk, true);
                            SetBlock(x + xi, y + yi, z + zi, leavesBlock);

                        }
                        else {
                            TerrainGen.SetBlock(x - xi, y - yi, z - zi, new BlockOrangeLeaves(), chunk, true);
                        }
                    }
                }
            }
        }

        //create trunk
        //replace height avg
        //   int height = 6;
        for (int yt = 0; yt < height; yt++)
        {
            if (!upsideDown)
            {
                Block block = GetCompanyBlock(company);

                SetBlock(x, y + yt, z, block, chunk, true);
                SetBlock(x, y + yt, z ,block);
            }
            else {
                SetBlock(x, y - yt, z, new BlockWhiteWood(), chunk, true);
            }
        }
    }
    void SetBlock(int x, int y, int z, Block block, Chunk chunk, bool replaceBlocks = false)
    {
        x -= chunk.pos.x;
        y -= chunk.pos.y;
        z -= chunk.pos.z;
        if (Chunk.InRange(x) && Chunk.InRange(y) && Chunk.InRange(z))
        {
            if (replaceBlocks || chunk.blocks[x, y, z] == null)
            {
                chunk.SetBlock(x, y, z, block);
            }
            else
            {
                Debug.Log("Failed2");
            }

        }
        else
        {
            Debug.Log("Failed1");
        }
    }
    Block GetCompanyBlock(string name)
    {
        Debug.Log("terrain" + name);
        switch (name)
        {
            case "Apple":
                BlockApple blockApple = new BlockApple();
                return blockApple;
            case "Alphabet":
                BlockAlphabet blockAlphabet = new BlockAlphabet();
                return blockAlphabet;
            case "Amazon":
                BlockAmazon blockAmazon = new BlockAmazon();
                return blockAmazon;
            case "Facebook":
                BlockFacebook blockFacebook = new BlockFacebook();
                return blockFacebook;
            case "IBM":
                BlockIBM blockIBM = new BlockIBM();
                return blockIBM;
            case "Intel":
                BlockIntel blockIntel = new BlockIntel();
                return blockIntel;
            case "Microsoft":
                BlockMicrosoft blockMicrosoft = new BlockMicrosoft();
                return blockMicrosoft;
            case "Netflix":
                BlockNetflix blockNetflix = new BlockNetflix();
                return blockNetflix;
            case "Twitter":
                BlockTwitter blockTwitter = new BlockTwitter();
                return blockTwitter;
            case "Yahoo":
                BlockYahoo blockYahoo = new BlockYahoo();
                return blockYahoo;
            case "Chipotle":
                BlockChipotle blockChipotle = new BlockChipotle();
                return blockChipotle;
            case "Walmart":
                BlockWalmart blockWalmart = new BlockWalmart();
                return blockWalmart;
            case "Costco":
                BlockCostco blockCostco = new BlockCostco();
                return blockCostco;
            case "Pepsi":
                BlockPepsi blockPepsi = new BlockPepsi();
                return blockPepsi;
            case "Symantec":
                BlockSymantec blockSymantec = new BlockSymantec();
                return blockSymantec;
            case "Garmin":
                BlockGarmin blockGarmin = new BlockGarmin();
                return blockGarmin;
        }
        return new BlockAir();
    }
}
