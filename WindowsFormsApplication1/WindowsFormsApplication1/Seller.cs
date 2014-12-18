using System;
using System.Collections.Generic;
using System.Text;
using Mogre;

namespace WindowsFormsApplication1
{
    class Seller
    {
        // Constantes
        const int initPrice = 10;
        const int initNbItems = 10;

        // Attributs
        int price;
        //int nbRemainingItems; //Non utilisé dans un premier temps.

        //Mogre Business
        Entity sellerEntity = null;
        SceneNode sellerNode = null;

        // Constructeur
        public Seller(String name, SceneManager mSceneMgr, float x, float y, float z, Random r)
        {
            price = initPrice + r.Next();
            //nbRemainingItems = initNbItems;

            sellerEntity = mSceneMgr.CreateEntity(name, "ninja.mesh");
            sellerEntity.CastShadows = true;
            sellerNode = mSceneMgr.RootSceneNode.CreateChildSceneNode(
                new Vector3(x, y, z));

            //The ninja is so damn big!
            sellerNode.Scale(0.4f, 0.4f, 0.4f);
            sellerNode.AttachObject(sellerEntity);
        }

    }
}
