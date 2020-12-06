using DataLayer;
using System;

namespace BusinessLayer
{
    public class Cameras
    {
        private CameraRepository cameraRepository;
        private FileRepository fileRepository;

        public Cameras(CameraRepository cameraRepository, FileRepository fileRepository)
        {
            this.cameraRepository = cameraRepository ?? throw new ArgumentNullException(nameof(cameraRepository));
            this.fileRepository = fileRepository ?? throw new ArgumentNullException(nameof(fileRepository));
        }

        public Camera Get(File file)
        {
            try
            {
                return new Camera();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
