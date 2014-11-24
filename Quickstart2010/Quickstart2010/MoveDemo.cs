﻿using System;
using System.Collections.Generic;
using System.Text;
using Mogre;
using MogreFramework;

using Quickstart2010.Modules;
using Quickstart2010.States;

namespace Quickstart2010
{

    public class MoveDemo : OgreWindow
    {
        public AnimationState mAnimationState = null; //The AnimationState the moving object
        float mDistance = 0.0f;              //The distance the object has left to travel
        Vector3 mDirection = Vector3.ZERO;   // The direction the object is moving
        Vector3 mDestination = Vector3.ZERO; // The destination the object is moving towards
        LinkedList<Vector3> mWalkList = null; // A doubly linked containing the waypoints
        float mWalkSpeed = 50.0f;  // The speed at which the object is moving


        public override void CreateSceneManager()
        {
            //ROBOT
            Entity ent = Program.mEngine.SceneMgr.CreateEntity("Robot", "robot.mesh");
            SceneNode node = Program.mEngine.SceneMgr.RootSceneNode.CreateChildSceneNode("HeadNode");
            node.AttachObject(ent);

            // Create knot objects so we can see movement
            ent = Program.mEngine.SceneMgr.CreateEntity("Knot1", "knot.mesh");
            node = Program.mEngine.SceneMgr.RootSceneNode.CreateChildSceneNode("Knot1Node",
                new Vector3(0.0f, -10.0f, 25.0f));
            node.AttachObject(ent);
            node.Scale(0.1f, 0.1f, 0.1f);
            //
            ent = Program.mEngine.SceneMgr.CreateEntity("Knot2", "knot.mesh");
            node = Program.mEngine.SceneMgr.RootSceneNode.CreateChildSceneNode("Knot2Node",
                new Vector3(550.0f, -10.0f, 50.0f));
            node.AttachObject(ent);
            node.Scale(0.1f, 0.1f, 0.1f);
            //
            ent = Program.mEngine.SceneMgr.CreateEntity("Knot3", "knot.mesh");
            node = Program.mEngine.SceneMgr.RootSceneNode.CreateChildSceneNode("Knot3Node",
                new Vector3(-100.0f, -10.0f, -200.0f));
            node.AttachObject(ent);
            node.Scale(0.1f, 0.1f, 0.1f);

            // Create the walking list
            mWalkList = new LinkedList<Vector3>();
            mWalkList.AddLast(new Vector3(550.0f, 0.0f, 50.0f));
            mWalkList.AddLast(new Vector3(-100.0f, 0.0f, -200.0f));
            mWalkList.AddLast(new Vector3(0.0f, 0.0f, 25.0f));

            // Set idle animation
            mAnimationState = Program.mEngine.SceneMgr.GetEntity("Robot").GetAnimationState("Walk");
            mAnimationState.Loop = true;
            mAnimationState.Enabled = true;
        }

        protected override void CreateInputHandler()
        {
            base.CreateInputHandler();
            this.Root.FrameStarted += new FrameListener.FrameStartedHandler(FrameStarted);
        }

        protected bool nextLocation()
        {
            return true;
        }

        bool FrameStarted(FrameEvent evt)
        {
            if (!mWalking)
            //either we've not started walking or reached a way point
            {
                //check if there are places to go
                if (nextLocation())
                {
                    LinkedListNode<Vector3> tmp;

                    //Start the walk animation
                    mAnimationState = mEntity.GetAnimationState("Walk");
                    mAnimationState.SetLoop(true);
                    mAnimationState.SetEnabled(true);
                    mWalking = true;

                    //Update the destination using the walklist.

                }//if(nextLocation())
            }
            return true;
        }


    }
}