using System;
using System.Collections.Generic;
using System.Text;
using Mogre;

namespace WindowsFormsApplication1
{
    class Buyer
    {
        //Fievre acheteuse Threshold
        const int BUY_NOW = 2000;

        //Animation, Movement
        public AnimationState mAnimationState = null; //The AnimationState the moving object
        public float _animationSpeed = 0.0f;
        public Vector3 mDirection = Vector3.ZERO;   // The direction the object is moving
        public Vector3 mDestination = Vector3.ZERO; // The destination the object is moving towards
        public LinkedList<Vector3> mWalkList = null; // A doubly linked containing the waypoints
        public SceneNode buyerNode;
        public Entity buyerEntity = null;
                float mWalkSpeed = 50.0f;

        public bool mWalking = false;
        float mDistance = 0.0f; //The distance the object has left to travel

        //Comportement
        int wantToBuy = 0;
        
        public Buyer(String name, SceneManager mSceneMgr, float x, float y, float z, Random r)
        {
            wantToBuy = r.Next(0, 500);

           buyerEntity = mSceneMgr.CreateEntity(name, "robot.mesh");
            buyerNode = mSceneMgr.RootSceneNode.CreateChildSceneNode(name, new Vector3(x, y, z));
            buyerNode.AttachObject(buyerEntity);

            _animationSpeed = (float)(r.NextDouble() + 0.5);
            // create the walking list
            mWalkList = new LinkedList<Vector3>();
            mWalkList.AddLast(new Vector3(550.0f + x, 0.0f, 50.0f + z));
            mWalkList.AddLast(new Vector3(-100.0f + x, 0.0f, -200.0f + z));
            mWalkList.AddLast(new Vector3(0.0f + x, 0.0f, 25.0f + z));

            // Set idle animation
            mAnimationState = buyerEntity.GetAnimationState("Walk");
            mAnimationState.Loop = true;
            mAnimationState.Enabled = true;
        }

          public bool NextLocation() 
        {
            if (mWalkList.Count == 0)
                return false;
            else
                return true;
        }

        public void Walk(SceneManager mSceneMgr, int i, float deltaTime)
        {
            wantToBuy++;

            if (!mWalking)
                // either we've not started walking or reached a way point
                {
                    // check if there are places to go
                    if (NextLocation() == true)
                    {
                        LinkedListNode<Vector3> tmp;

                        // start the walk animation
                        mAnimationState = buyerEntity.GetAnimationState("Walk");
                        mAnimationState.Loop = true;
                        mAnimationState.Enabled = true;
                        mWalking = true;

                        // update the destination using the walklist.
                        mDestination = mWalkList.First.Value;       // get the next destination.
                        tmp = mWalkList.First;                      // save the node that held it
                        mWalkList.RemoveFirst();                    // remove that node from the front of the list

                         Random rand1 = new Random(i);
                         Random rand2 = new Random();
                         mWalkList.AddLast(new Vector3(rand1.Next(-100, 100) + rand2.Next(-500, 500), 0, rand1.Next(-100, 100) + rand2.Next(-500, 500)));                     // add it to the back of the list.  


                        // update the direction and the distance
                        mDirection = mDestination - buyerNode.Position;
                        mDistance = mDirection.Normalise();

                    }
                    else // nowhere to go. set the idle animation. (or Die)
                    {
                        Entity buyer = mSceneMgr.GetEntity("buyer" + i);
                        mAnimationState = buyer.GetAnimationState("Idle");
                    }
                }

                else // we're in motion
                {
                    // determine how far to move this frame
                    mDistance -= mWalkSpeed * deltaTime;


                    // check to see if we've arrived at a waypoint
                    if (mDistance <= 0.0f)
                    {
                        // set our node to the destination we've just reached & reset direction to 0
                        buyerNode.Position = mDestination;
                        mDirection = Vector3.ZERO;
                        mWalking = false;
                    }
                    else
                    {
                        // movement code goes here
                        buyerNode.Translate(mDirection * mWalkSpeed * deltaTime);

                        // rotation code goes here
                        Vector3 src = buyerNode.Orientation * Vector3.UNIT_X;
                        if ((1.0f + src.DotProduct(mDirection)) < 0.0001f)
                        {
                            buyerNode.Yaw(180.0f);
                        }
                        else
                        {
                            Quaternion quat = src.GetRotationTo(mDirection);
                            buyerNode.Rotate(quat);
                        }

                    }
                }
                mAnimationState.AddTime(deltaTime);
            }
        }
    }
