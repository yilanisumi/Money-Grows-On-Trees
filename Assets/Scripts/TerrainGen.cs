using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimplexNoise;
public class TerrainGen
{
    //stone noise 
    float stoneBaseHeight = -24;
    float stoneBaseNoise = 0.05f;
    float stoneBaseNoiseHeight = 4;
    //lowest level stones can go
    float stoneMinHeight = -12;
    //mountain noise
    float stoneMountainHeight = 48;
    float stoneMountainFrequency = 0.008f;
    //dirt noise
    float dirtBaseHeight = 1;
    float dirtNoise = 0.04f;
    float dirtNoiseHeight = 3;
    //cave noise
    float caveFrequency = 0.02f;
    int caveSize = 4;
    //tree noise
    float brownTreeFrequency = 0.2f;
    int brownTreeDensity = 3;

    float whiteTreeFrequency = 0.3f;
    int whiteTreeDensity = 4;

    float roofBaseHeight = 20;
    float roofNoise = 0.04f;
    float roofNoiseHeight = 10;
    Vector3 origin;
    List<string> companyNames;
    World world;
    public void SetOrigin(Vector3 org)
    {
        origin = org;
    }
    public void SetWorld(World w)
    {
        world = w;
    }
        
    Block GetCompanyDirtBlock(string name)
    {
        switch (name)
        {
            case "Apple":
                BlockAppleDirt blockApple = new BlockAppleDirt();
                return blockApple;
            case "Alphabet":
                BlockAlphabetDirt blockAlphabet = new BlockAlphabetDirt();
                return blockAlphabet;
            case "Amazon":
                BlockAmazonDirt blockAmazon = new BlockAmazonDirt();
                return blockAmazon;
            case "Facebook":
                BlockFacebookDirt blockFacebook = new BlockFacebookDirt();
                return blockFacebook;
            case "IBM":
                BlockIBMDirt blockIBM = new BlockIBMDirt();
                return blockIBM;
            case "Intel":
                BlockIntelDirt blockIntel = new BlockIntelDirt();
                return blockIntel;
            case "Microsoft":
                BlockMicrosoftDirt blockMicrosoft = new BlockMicrosoftDirt();
                return blockMicrosoft;
            case "Netflix":
                BlockNetflixDirt blockNetflix = new BlockNetflixDirt();
                return blockNetflix;
            case "Twitter":
                BlockTwitterDirt blockTwitter = new BlockTwitterDirt();
                return blockTwitter;
            case "Yahoo":
                BlockYahooDirt blockYahoo = new BlockYahooDirt();
                return blockYahoo;
            case "Garmin":
                BlockGarminDirt blockGarmin = new BlockGarminDirt();
                return blockGarmin;
            case "Symantec":
                BlockSymantecDirt blockSymantec = new BlockSymantecDirt();
                return blockSymantec;
            case "Chipotle":
                BlockChipotleDirt blockChipotle = new BlockChipotleDirt();
                return blockChipotle;
            case "Costco":
                BlockCostcoDirt blockCostco = new BlockCostcoDirt();
                return blockCostco;
            case "Pepsi":
                BlockPepsiDirt blockPepsi = new BlockPepsiDirt();
                return blockPepsi;
            case "Walmart":
                BlockWalmartDirt blockWalmart = new BlockWalmartDirt();
                return blockWalmart;
            
        }

        return new BlockAir();
    }
    Block GetCompanyBlock(string name)
    {
       // Debug.Log("terrain" + name);
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
    public void SetCompanies(List<string> companies)
    {
        companyNames = companies;
    }
    void CreateGarden(Chunk chunk, int x, int z)
    {
        for (int y = chunk.pos.y - 8; y < chunk.pos.y + Chunk.chunkSize; y++)
        {             
            if(y  == -8 || y == -7)
            {

                if(x == origin.x-10 || x == origin.x+10||z==origin.z+10 || z == origin.z - 10)
                {
                    SetBlock(x, y, z, new BlockLava(), chunk);
                }
                else
                {
                    if (y == -8)
                    {
                        
                        if(x == origin.x && z == origin.z)
                        {
                            Block block = GetCompanyDirtBlock(companyNames[0]);
                            world.AddToCompanyGardenPositions(companyNames[0], new Vector3(origin.x, y, origin.z));
                            SetBlock(x, y, z, block, chunk);
                        } else if(x == origin.x + 5 && z == origin.z + 5)
                        {
                            Block block = GetCompanyDirtBlock(companyNames[1]);
                            world.AddToCompanyGardenPositions(companyNames[1], new Vector3(origin.x + 5, y, origin.z + 5));
                            SetBlock(x, y, z, block, chunk);
                        } else if(x == origin.x + 5 && z == origin.z - 5)
                        {
                            Block block = GetCompanyDirtBlock(companyNames[2]);
                           world.AddToCompanyGardenPositions(companyNames[2], new Vector3(origin.x + 5, y, origin.z - 5));
                            SetBlock(x, y, z, block, chunk);
                        } else if(x == origin.x - 5 && z == origin.z - 5)
                        {
                            Block block = GetCompanyDirtBlock(companyNames[3]);
                            world.AddToCompanyGardenPositions(companyNames[3], new Vector3(origin.x - 5, y, origin.z - 5));
                            SetBlock(x, y, z, block, chunk);
                        } else if(x == origin.x - 5 && z == origin.z + 5)
                        {
                            Block block = GetCompanyDirtBlock(companyNames[4]);
                            world.AddToCompanyGardenPositions(companyNames[4], new Vector3(origin.x - 5, y, origin.z + 5));
                            SetBlock(x, y, z, block, chunk);
                        }
                        else
                        {
                            SetBlock(x, y, z, new BlockGrass(), chunk);
                        }
                       
                    }
                    else
                    {
                        SetBlock(x, y, z, new BlockAir(), chunk);
                    }
                   
                }
                   
            }
            else if(y < -8)
            {
                SetBlock(x, y, z, new Block(), chunk);
            }
            else
            {
                SetBlock(x, y, z, new BlockAir(), chunk);
            }

        }
        

    }

    //takes a chunk, fill it, and return the filled chunk
    public Chunk ChunkGen(Chunk chunk)
    {
        // +3/-3 so we can catch the start of any tree that will have leaves in the chunk
        for (int x = chunk.pos.x - 6; x < chunk.pos.x + Chunk.chunkSize + 6; x++)
        {
            for (int z = chunk.pos.z - 6; z < chunk.pos.z + Chunk.chunkSize + 6; z++)
            {
                chunk = ChunkColumnGen(chunk, x, z);
            }
        }
        return chunk;
    }
    public Chunk ChunkColumnGen(Chunk chunk, int x, int z)
    {
        if(x >= origin.x-10 && x <= origin.x+10 && z <= origin.z +10 && z >= origin.z - 10)
        {
            CreateGarden(chunk, x, z);
            return chunk;
        }
      
        int stoneHeight = Mathf.FloorToInt(stoneBaseHeight);
        //add mountain noise
        stoneHeight += GetNoise(x, 0, z, stoneMountainFrequency, Mathf.FloorToInt(stoneMountainHeight));
        //check stone height does not go below min height
        if (stoneHeight < stoneMinHeight)
            stoneHeight = Mathf.FloorToInt(stoneMinHeight);
        //apply base noise
        stoneHeight += GetNoise(x, 0, z, stoneBaseNoise, Mathf.FloorToInt(stoneBaseNoiseHeight));

        int dirtHeight = stoneHeight + Mathf.FloorToInt(dirtBaseHeight);
        //add dirt noise
        dirtHeight += GetNoise(x, 100, z, dirtNoise, Mathf.FloorToInt(dirtNoiseHeight));

        int roofHeight = dirtHeight + Mathf.FloorToInt(roofBaseHeight);
        roofHeight += GetNoise(x, 100, z, roofNoise, Mathf.FloorToInt(roofNoiseHeight));
        //cycle through every chunk in the column
        //subtract 8 to not cut off trees which are height 8
        int count = 0;
        for (int y = chunk.pos.y - 16; y < chunk.pos.y + Chunk.chunkSize+8; y++)
        {
            count++;
            //Get a value to base cave generation on
            int caveChance = GetNoise(x, y, z, caveFrequency, 100);
            //set stones
            if (y <= stoneHeight && caveSize < caveChance)
            {
                SetBlock(x, y, z, new Block(), chunk);
            }
            //set dirt
            else if (y <= dirtHeight && caveSize < caveChance)
            {
                SetBlock(x, y, z, new BlockGrass(), chunk);
                if (y == dirtHeight && GetNoise(x, 0, z, brownTreeFrequency, 100) < brownTreeDensity)
                {
                    int rand = Random.Range(0, companyNames.Count);
                    string company = companyNames[rand];
                    StockInventory inven = GameObject.Find("StockDatabase").GetComponent<StockInventory>();
                    int height = 6;
                    if(inven != null)
                    {
                        Company c = (Company)inven.companyTranslation[company];

                        height = (int)(c.avg / 50) + 2;
                        height = Mathf.Min(height, 10);
                        Vector3 vec = new Vector3(x, y + 1, z);
                        KeyValuePair<Vector3, Chunk> pair = new KeyValuePair<Vector3, Chunk>(vec,chunk);
                        List<KeyValuePair<Vector3, Chunk>> list = (List<KeyValuePair<Vector3, Chunk>>)world.companyTrees[c];
                        if(list == null)
                        {
                            list = new List<KeyValuePair<Vector3, Chunk>>();
                        }
                        list.Add(pair);
                        world.companyTrees[c] = list;                       
                       
                    }
                    CreateTree(x, y + 1, z, chunk, false, company, height);

                    //company, list of pair<Vector3, chunk>

                }

            }
            //roof. didn't add cave chance
        //    else if (y <= roofHeight && y >= dirtHeight + 20)
         //   {
         //       SetBlock(x, y, z, new BlockLava(), chunk);
          //      if (y == dirtHeight + 20 && GetNoise(x, y, z, whiteTreeFrequency, 100) < whiteTreeDensity)
          //      {
           //         CreateTree(x, y - 1, z, chunk, true);
           //     }
           // }
            //set air
            else
            {
                SetBlock(x, y, z, new BlockAir(), chunk);
            }
        }
        return chunk;
    }
    //gets noise based on coordinates
    //smaller scale gives smooth noise suitable for mountains and long distances between peaks and values
    //larger scale value makes more frequent bumps and dips
    public static int GetNoise(int x, int y, int z, float scale, int max)
    {
        return Mathf.FloorToInt((Noise.Generate(x * scale, y * scale, z * scale) + 1f) * (max / 2f));
    }

    //uses local coordinatates of the block relative to the chunk
    public static void SetBlock(int x, int y, int z, Block block, Chunk chunk, bool replaceBlocks = false)
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


        }

    }

    //same tree every time
    public void CreateTree(int x, int y, int z, Chunk chunk, bool upsideDown, string company, int height)
    {
        StockInventory inven = GameObject.Find("StockDatabase").GetComponent<StockInventory>();

 
            Company c = (Company)inven.companyTranslation[company];
        int seas = GameObject.Find("Clock").GetComponent<Calendar>().month;
        List<double> list = (List<double>) inven.stocksValue[c];

        double curr = list[seas];
        double min = c.min;
        double inc = c.incrementer;
        double temp = (curr - min) / inc;
        int type = (int)temp;
        type = Mathf.Min(type, 4);
        type = Mathf.Max(type, 0);
            //create leaves
         if(height >= 3)
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
                                leavesBlock.SetCompany(company);
                            }
                            else if (type == 1)
                            {
                                leavesBlock = new BlockLeaves1();
                                leavesBlock.SetCompany(company);
                            }
                            else if (type == 2)
                            {
                                leavesBlock = new BlockLeaves2();
                                leavesBlock.SetCompany(company);
                            }
                            else if (type == 3)
                            {
                                leavesBlock = new BlockLeaves3();
                                leavesBlock.SetCompany(company);
                            }
                            else
                            {
                                leavesBlock = new BlockLeaves4();
                                leavesBlock.SetCompany(company);
                            }
                            //BlockGreenLeaves leavesBlock = new BlockGreenLeaves();
                            //leavesBlock.SetCompany(company);
                            SetBlock(x + xi, y + yi, z + zi, leavesBlock, chunk, true);

                        }
                        else {
                            SetBlock(x - xi, y - yi, z - zi, new BlockOrangeLeaves(), chunk, true);
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
            }
            else {
                SetBlock(x, y - yt, z, new BlockWhiteWood(), chunk, true);
            }
        }
    }
  
}
