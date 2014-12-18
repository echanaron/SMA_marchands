using System;
using System.Collections.Generic;
using System.Text;
using Mogre;

namespace WindowsFormsApplication1
{
    class Simulation : WindowMaker
    {
        //Input
        protected MOIS.InputManager inputManager;
        protected MOIS.Keyboard inputKeyboard;
        protected MOIS.Mouse inputMouse;

        //Stuff
        protected bool shutDown = false;
        protected float camSpeed = 100f;
        protected Degree rotateSpeed = 36;
        protected float toggleDelay = 1.0f;

        //Numbers
        const int NUM_BUYERS = 10;
        const int NUM_SELLERS = 5;
        const int ROW_COUNT = 15;

        //Buyers
        Buyer[] buyer = new Buyer[NUM_BUYERS];
        float mWalkSpeed = 50.0f;

        //Sellers
        Seller[] seller = new Seller[NUM_SELLERS];

        public Simulation()
        {
        }

        // ici tu crées ta caméra
        protected override void CreateCamera()
        {
            mCamera = mSceneMgr.CreateCamera("PlayerCam");
            mCamera.Position = new Vector3(20, 50, 500);
            mCamera.LookAt(Vector3.ZERO);
            mCamera.NearClipDistance = 5;
            mCameraMan = new CameraMan(mCamera);
            base.CreateCamera();
        }

        protected override void CreateFrameListeners()
        {
            mRoot.FrameStarted += new FrameListener.FrameStartedHandler(FrameStarted);
            mRoot.FrameStarted += new FrameListener.FrameStartedHandler(Anime_FrameStarted);
            base.CreateFrameListeners();
        }

        bool Anime_FrameStarted(FrameEvent evt)
        {
            float deltaTime = evt.timeSinceLastFrame * mWalkSpeed / 20;
            for (int i = 0; i < NUM_BUYERS; ++i)
            {
                buyer[i].Walk(mSceneMgr, i, deltaTime);
            }
            return true;
        }

        protected override void CreateInputHandler()
        {
            LogManager.Singleton.LogMessage("*** Initializing OIS ***");
            MOIS.ParamList pl = new MOIS.ParamList();
            IntPtr windowHnd;
            mWindow.GetCustomAttribute("WINDOW", out windowHnd);
            pl.Insert("WINDOW", windowHnd.ToString());

            inputManager = MOIS.InputManager.CreateInputSystem(pl);

            //Create all devices (We only catch joystick exceptions here, as, most people have Key/Mouse)
            inputKeyboard = (MOIS.Keyboard)inputManager.CreateInputObject(MOIS.Type.OISKeyboard, UseBufferedInput);
            inputMouse = (MOIS.Mouse)inputManager.CreateInputObject(MOIS.Type.OISMouse, UseBufferedInput);
            base.CreateInputHandler();
        }

        public virtual bool UseBufferedInput
        {
            get { return false; }
        }

        protected override void ChooseSceneManager()
        {
            base.ChooseSceneManager();
        }

        protected override void CreateSceneManager()
        {

            mSceneMgr = Root.CreateSceneManager(SceneType.ST_EXTERIOR_CLOSE);

            //Sky
            mSceneMgr.SetSkyDome(true, "Examples/CloudySky", 5, 8);
            mSceneMgr.ShadowTechnique = ShadowTechnique.SHADOWTYPE_STENCIL_ADDITIVE;

            //Setting a ground
            Plane plane = new Plane(Vector3.UNIT_Y, 0);
            MeshManager.Singleton.CreatePlane("ground",
                ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME, plane,
                2500, 2500, 20, 20, true, 1, 5, 5, Vector3.UNIT_Z);
            Entity groundEnt = mSceneMgr.CreateEntity("GroundEntity", "ground");
            mSceneMgr.RootSceneNode.CreateChildSceneNode().AttachObject(groundEnt);
            groundEnt.SetMaterialName("Examples/Rockwall");
            groundEnt.CastShadows = false;


            //Buyers
            int row = 0;
            int column = 0;
            Random rnd = new Random();
            for (int i = 0; i < NUM_BUYERS; ++i, ++column)
            {
                if (column > ROW_COUNT)
                {
                    ++row;
                    column = 0;
                }

                buyer[i] = new Buyer("buyer" + i, mSceneMgr, -(row * 100), 0, (column * 50), rnd);
            }

            //Sellers
            for (int i = 0; i < NUM_SELLERS; ++i, ++column)
            {
                if (column > ROW_COUNT)
                {
                    ++row;
                    column = 0;
                }

                seller[i] = new Seller("Seller" + i, mSceneMgr, column * 200 - 2000, 0, 1000.0f, new Random());
            }

            //Lights
            Light pointLight = mSceneMgr.CreateLight("pointLight");
            pointLight.Type = Light.LightTypes.LT_POINT;
            pointLight.Position = new Vector3(0, 150, 250);
            pointLight.DiffuseColour = ColourValue.Red;
            pointLight.SpecularColour = ColourValue.Red;

            Light directionalLight = mSceneMgr.CreateLight("directionalLight");
            directionalLight.Type = Light.LightTypes.LT_DIRECTIONAL;
            directionalLight.DiffuseColour = new ColourValue(.25f, .25f, 0);
            directionalLight.SpecularColour = new ColourValue(.25f, .25f, 0);
            directionalLight.Direction = new Vector3(0, -1, 1);

            Light spotLight = mSceneMgr.CreateLight("spotLight");
            spotLight.Type = Light.LightTypes.LT_SPOTLIGHT;
            spotLight.DiffuseColour = ColourValue.Blue;
            spotLight.SpecularColour = ColourValue.Blue;
            spotLight.Direction = new Vector3(-1, -1, 0);
            spotLight.Position = new Vector3(300, 300, 0);
            spotLight.SetSpotlightRange(new Degree(35), new Degree(50));
        }

        protected override void CreateRenderWindow(IntPtr handle)
        {
            base.CreateRenderWindow(handle);
        }

        protected override void CreateViewport()
        {
            Viewport viewport = mWindow.AddViewport(mCamera);
            viewport.BackgroundColour = ColourValue.Black;
            mCamera.AspectRatio = (float)viewport.ActualWidth / viewport.ActualHeight;

            base.CreateViewport();
        }

        protected override void CreateOverlay()
        {
            base.CreateOverlay();
        }

        bool FrameStarted(FrameEvent evt)
        {
            if (mWindow.IsClosed)
                return false;

            HandleInput(evt);

            return !shutDown;
        }

        protected virtual void HandleInput(FrameEvent evt)
        {
            // Move about 100 units per second,
            float moveScale = camSpeed * evt.timeSinceLastFrame;
            // Take about 10 seconds for full rotation
            Degree rotScale = rotateSpeed * evt.timeSinceLastFrame;

            Vector3 translateVector = Vector3.ZERO;


            // set the scaling of camera motion
            Degree scaleRotate = rotateSpeed * evt.timeSinceLastFrame;

            Vector3 camVelocity = Vector3.ZERO;

            inputKeyboard.Capture();

            if (inputKeyboard.IsKeyDown(MOIS.KeyCode.KC_ESCAPE))
            {
                // stop rendering loop
                shutDown = true;
            }

            if (inputKeyboard.IsKeyDown(MOIS.KeyCode.KC_A))
            {
                translateVector.x = -moveScale;
            }

            if (inputKeyboard.IsKeyDown(MOIS.KeyCode.KC_D))
            {
                translateVector.x = moveScale;
            }

            if (inputKeyboard.IsKeyDown(MOIS.KeyCode.KC_W))
            {
                translateVector.z = -moveScale;
            }

            if (inputKeyboard.IsKeyDown(MOIS.KeyCode.KC_S))
            {
                translateVector.z = moveScale;
            }

            if (inputKeyboard.IsKeyDown(MOIS.KeyCode.KC_LEFT))
            {
                mCamera.Yaw(scaleRotate);
            }

            if (inputKeyboard.IsKeyDown(MOIS.KeyCode.KC_RIGHT))
            {
                mCamera.Yaw(-scaleRotate);
            }

            if (inputKeyboard.IsKeyDown(MOIS.KeyCode.KC_UP))
            {
                mCamera.Pitch(scaleRotate);
            }

            if (inputKeyboard.IsKeyDown(MOIS.KeyCode.KC_DOWN))
            {
                mCamera.Pitch(-scaleRotate);
            }

            // subtract the time since last frame to delay specific key presses
            toggleDelay -= evt.timeSinceLastFrame;

            // toggle rendering mode
            if (inputKeyboard.IsKeyDown(MOIS.KeyCode.KC_R) && toggleDelay < 0)
            {
                if (mCamera.PolygonMode == PolygonMode.PM_POINTS)
                {
                    mCamera.PolygonMode = PolygonMode.PM_SOLID;
                }
                else if (mCamera.PolygonMode == PolygonMode.PM_SOLID)
                {
                    mCamera.PolygonMode = PolygonMode.PM_WIREFRAME;
                }
                else
                {
                    mCamera.PolygonMode = PolygonMode.PM_POINTS;
                }

                Console.WriteLine("Rendering mode changed to '{0}'.", mCamera.PolygonMode);

                toggleDelay = 1;


                inputMouse.Capture();
                MOIS.MouseState_NativePtr mouseState = inputMouse.MouseState;

                if (!mouseState.ButtonDown(MOIS.MouseButtonID.MB_Left))
                {
                    Degree cameraYaw = -mouseState.X.rel * .13f;
                    Degree cameraPitch = -mouseState.Y.rel * .13f;

                    mCamera.Yaw(cameraYaw);
                    mCamera.Pitch(cameraPitch);
                }
                else
                {
                    translateVector.x += mouseState.X.rel * 0.13f;
                }


                // move the camera based on the accumulated movement vector
                mCamera.MoveRelative(translateVector);
            }
        }
    }
}
