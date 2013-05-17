namespace _3DScanner.Client.Rendering
{
    public class Camera
    {
        public float X;
        public float Y;
        public float Z;
        public float Pan;
        public float Yaw;
        public float Roll;

        public Camera()
        {
            X = 0;
            Y = 0;
            Z = 0;
            Pan = 0;
            Yaw = 0;
            Roll = 0;
        }

        public void Update(Camera camera, float delta)
        {
            X +=  camera.X * delta;
            Y += camera.Y * delta; 
            Z += camera.Z * delta;
            Pan += camera.Pan * delta;
            Yaw += camera.Yaw * delta; 
            Roll += camera.Roll * delta;
        } 
    }
}
